using System.Collections.Generic;

namespace CI3540.UI.Areas.Store.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string DefaultImageUrl { get; set; }
        public IEnumerable<ProductImageViewModel> Images { get; set; }
        public IEnumerable<CategoryTagViewModel> Tags { get; set; }
        public IEnumerable<ReviewViewModel> Reviews { get; set; } 
    }
}