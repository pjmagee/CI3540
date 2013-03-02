using System;
using System.Linq;
using System.Web.Mvc;
using CI3540.UI.Areas.Admin.Models;
using CI3540.UI.Binders;
using CI3540.UI.BootstrapSupport.HtmlHelpers.Paging;
using CI3540.UI.Controllers;
using CI3540.UI.Services;
using Ninject;

namespace CI3540.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : BootstrapBaseController
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        [Inject]
        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            if (productService == null)
            {
                throw new ArgumentNullException("productService");
            }

            if (categoryService == null)
            {
                throw new ArgumentNullException("categoryService");
            }

            this.categoryService = categoryService;
            this.productService = productService;
        }

        public ActionResult Index(int? page, int? categoryId, string name, [ModelBinder(typeof(CommaSeparatedModelBinder))] int[] categoryIds, int pageSize = 10)
        {
            var pageNumber = (page ?? 1);

            var products = productService.GetProductsByCategoryId(categoryId);

            if (categoryIds != null)
            {
                ViewBag.CategoryIds = categoryService.GetCategoryTagsFromCategoryIds(categoryIds);
                products = productService.GetProductsUnderCategories(categoryIds);
            }

            if (!string.IsNullOrEmpty(name))
            {
                products = products.Where(p => p.Name.Contains(name)).ToList();
            }

            ViewBag.Categories = new SelectList(categoryService.GetParentCategories(), "Id", "Name");

            return View(products.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int id = 0)
        {
            var product = productService.GetProductById(id);
            return View(product);
        }

        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// http://www.webtrendset.com/2011/06/22/complete-code-example-for-using-blueimp-jquery-file-upload-control-in-asp-net/
        /// http://stackoverflow.com/questions/7983023/the-parameter-conversion-from-type-system-string-to-type-x-failed-because-n
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(NewProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var product = productService.CreateNewProduct(model);
                    Success(string.Format("Product '{0}' was added.", product.Name));
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    Error(e.Message);
                    return View(model);
                }
            }

            return View(model);
        }

        public ActionResult Edit(int id = 0)
        {
            var product = productService.GetEditProductById(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(int id, EditProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var product = productService.EditProduct(id, model);
                    Success(string.Format("Product '{0}' was successfully updated.", product.Name));
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    Error(e.Message);
                    return View(model);
                }
            }
            return View(model);
        }

        public ActionResult Delete(int id = 0)
        {
            var product = productService.GetProductById(id);
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            productService.DeleteProductById(id);
            return RedirectToAction("Index");
        }
    }
}