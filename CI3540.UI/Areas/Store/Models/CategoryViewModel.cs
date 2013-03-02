using System.Collections.Generic;

namespace CI3540.UI.Areas.Store.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ParentName { get; set; }
        public int ParentId { get; set; }
        public int NumberOfProducts { get; set; }
        public int NumberOfCategories { get; set; }
        public IEnumerable<CategoryViewModel> Children { get; set; }
        public bool IsParentCategory { get; set; }
        public bool HasChildCategories { get; set; }
    }
}
