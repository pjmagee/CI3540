using System.Collections.Generic;
using System.Web;
using CI3540.UI.Areas.Store.Models;

namespace CI3540.UI.Areas.Admin.Models
{
    public class EditProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public IList<ProductImageViewModel> Images { get; set; }
        public IList<CategoryTagViewModel> Tags { get; set; }
        public int SelectedDefaultImage { get; set; }
        public HttpPostedFileBase[] Files { get; set; }
        public IList<int> CategoryIds { get; set; }
    }
}