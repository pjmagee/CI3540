using System.Collections.Generic;
using CI3540.UI.Areas.Admin.Models;

namespace CI3540.UI.Areas.Store.Models
{
    public class ListingViewModel
    {
        public IEnumerable<ProductViewModel> ProductViewModels { get; set; }
        public IEnumerable<CategoryViewModel> CategoryViewModels { get; set; }
        public CartViewModel CartViewModel { get; set; } 
    }
}