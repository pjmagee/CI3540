using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Web;
using AutoMapper;
using CI3540.Core.Entities;
using CI3540.Infrastructure.EntityFramework;
using CI3540.UI.Areas.Admin.Models;
using CI3540.UI.Areas.Store.Models;
using Ninject;

namespace CI3540.UI.Services.Impl
{
    public class ProductService : IProductService
    {
        private readonly StoreContext context;

        [Inject]
        public ProductService(StoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<ProductViewModel> GetProducts()
        {
            var products = context.Products.ToList();
            return Mapper.Map<List<ProductViewModel>>(products);
        }

        public IEnumerable<ProductViewModel> GetProductsByCategoryId(int? id)
        {
            List<Product> products = context.Products.ToList();

            if (id != null)
            {
                products = products.Where(p => p.Categories.Any(c => c.Id == id || c.ParentId == id)).ToList();
            }

            return Mapper.Map<List<Product>, List<ProductViewModel>>(products);
        }

        public IEnumerable<ProductViewModel> GetProductsByName(string name)
        {
            var products = context.Products.Where(product => product.Name.Contains(name)).ToList();
            return Mapper.Map<List<Product>, List<ProductViewModel>>(products);
        }

        public ProductViewModel GetProductById(int id)
        {
            var product = context.Products.Find(id);
            return Mapper.Map<Product, ProductViewModel>(product);
        }

        public IEnumerable<ProductViewModel> GetMostPurchasedProducts(int amount = 1)
        {
            IQueryable<int> products = (from orderLine in context.OrderLines
                                        group orderLine by orderLine.Product.Id
                                        into collection
                                        orderby collection.Count() 
                                        descending
                                        select collection.Key);

            return Mapper.Map<List<ProductViewModel>>(products.Take(amount).Select(i => context.Products.Find(i)));
        }

        public IEnumerable<ProductViewModel> GetProducts(int? categoryId, string filterByName)
        {
            var products = categoryId != null ? context.Products.Where(p => p.Categories.Any(c => c.Id == categoryId || c.ParentId == categoryId)).ToList() : context.Products.ToList();

            if (!string.IsNullOrEmpty(filterByName))
            {
                products = products.Where(product => product.Name.Contains(filterByName)).ToList();
            }
            return Mapper.Map<List<ProductViewModel>>(products);
        }

        public IEnumerable<ProductViewModel> GetProductsUnderCategories(int[] categoryIds)
        {
            var products = from id in categoryIds
                           from product in context.Products
                           from category in product.Categories
                           where category.Id == id || category.ParentId == id
                           orderby product.Name
                           select product;

            return Mapper.Map<List<ProductViewModel>>(products);
        }

        public ProductViewModel CreateNewProduct(NewProductViewModel model)
        {
            var product = Mapper.Map<NewProductViewModel, Product>(model);

            #region Categories

            if (model.CategoryIds != null)
            {
                var foundCategories = (from category in context.Categories
                                       from categoryId in model.CategoryIds
                                       where category.Id == categoryId
                                       select category).ToList();

                product.Categories = foundCategories;
            }

            #endregion

            context.Products.Add(product);
            context.SaveChanges();

            #region Images

            if (model.Files != null)
            {
                var httpPostedFileBases = model.Files;

                AddImages(httpPostedFileBases, product);

                if (!product.ProductImages.Any(image => image.Primary))
                {
                    product.ProductImages.First().Primary = true;
                }
            }

            #endregion

            context.SaveChanges();

            return Mapper.Map<ProductViewModel>(product);
        }

        public EditProductViewModel GetEditProductById(int id)
        {
            var product = context.Products.Find(id);
            return Mapper.Map<EditProductViewModel>(product);
        }

        public ProductViewModel DeleteProductById(int id)
        {
            var product = context.Products.Find(id);
            context.Products.Remove(product);
            context.SaveChanges();
            return Mapper.Map<ProductViewModel>(product);
        }

        public ProductViewModel EditProduct(int id, EditProductViewModel model)
        {
            var product = context.Products.Find(id);

            Mapper.Map(model, product);

            #region Category

            if (model.CategoryIds != null)
            {
                foreach (int categoryId in model.CategoryIds)
                {
                    Category category = context.Categories.Find(categoryId);

                    if (category == null)
                        continue;

                    if (product.Categories.Any(c => c.Id == categoryId))
                        continue;

                    product.Categories.Add(category);
                }

                foreach (Category category in product.Categories)
                {
                    if (model.CategoryIds.All(i => i != category.Id))
                    {
                        product.Categories.Remove(category);
                    }
                }
            }

            #endregion

            #region Images

            if (model.Files != null)
            {
                var httpPostedFileBases = model.Files;
                AddImages(httpPostedFileBases, product);
            }

            var images = (from image in model.Images
                          from productImage in product.ProductImages
                          where productImage.Id == image.Id && image.Delete
                          select productImage).ToList();

            foreach (var productImage in images)
            {
                try
                {
                    product.ProductImages.Remove(productImage);
                    File.Delete(productImage.Path);
                }
                catch (DirectoryNotFoundException e)
                {

                }
            }

            product.ProductImages.Single(image => image.Id == model.SelectedDefaultImage).Primary = true;

            #endregion

            context.SaveChanges();

            return Mapper.Map<ProductViewModel>(product);
        }

        private void AddImages(IEnumerable<HttpPostedFileBase> httpPostedFileBases, Product product)
        {
            var images = new Collection<ProductImage>();

            foreach (HttpPostedFileBase file in httpPostedFileBases)
            {
                if (file != null && file.FileName != null)
                {
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(string.Format("~/Uploads/Product_{0}", product.Id))))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(string.Format("~/Uploads/Product_{0}", product.Id)));
                    }

                    string path = Path.Combine(HttpContext.Current.Server.MapPath(string.Format("~/Uploads/Product_{0}", product.Id)), Path.GetFileName(file.FileName));
                    file.SaveAs(path);

                    ProductImage image = new ProductImage();
                    image.Path = path;
                    image.Product = product;
                    image.ProductId = product.Id;
                    image.Primary = false;

                    images.Add(image);
                }
            }

            product.ProductImages = images;
        }
    }
}