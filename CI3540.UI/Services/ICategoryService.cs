using System.Collections.Generic;
using CI3540.UI.Areas.Admin.Models;
using CI3540.UI.Areas.Store.Models;

namespace CI3540.UI.Services
{
    public interface ICategoryService
    {
        CategoryViewModel GetCategoryById(int id);
        CategoryViewModel GetCategoryByName(string name);
        CategoryViewModel CreateCategory(NewCategoryViewModel model);
        CategoryViewModel UpdateCategory(CategoryViewModel model);
        IEnumerable<CategoryViewModel> GetCategories();
        IEnumerable<CategoryViewModel> GetParentCategories();
        IEnumerable<CategoryViewModel> GetCategoryByParentCategory(int? parentId);
        IEnumerable<CategoryViewModel> GetCategoriesByName(string name);
        IEnumerable<CategoryViewModel> GetCategories(int? parentId, string categoryName);
        IEnumerable<CategoryTagViewModel> GetCategoryTagsFromCategoryIds(int[] categoryIds);
        bool CategoryExists(string name);
    }
}