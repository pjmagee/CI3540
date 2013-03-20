using System.Collections.Generic;
using CI3540.UI.Areas.Admin.Models;
using CI3540.UI.Areas.Store.Models;

namespace CI3540.UI.Services
{
    public interface IProductService
    {
        ProductViewModel GetProductById(int id);
        ProductViewModel CreateNewProduct(NewProductViewModel model);
        ProductViewModel DeleteProductById(int id);
        ProductViewModel EditProduct(int id, EditProductViewModel model);
        EditProductViewModel GetEditProductById(int id);
        IEnumerable<ProductViewModel> GetProducts();
        IEnumerable<ProductViewModel> GetProductsByCategoryId(int? id);
        IEnumerable<ProductViewModel> GetProductsByName(string name);
        IEnumerable<ProductViewModel> GetMostPurchasedProducts(int amount);
        IEnumerable<ProductViewModel> GetProducts(int? categoryId, string name);
        IEnumerable<ProductViewModel> GetProductsUnderCategories(int[] categoryIds);
    }
}