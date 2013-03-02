using System.Collections.Generic;
using System.Web;

namespace CI3540.UI.Areas.Admin.Models
{
    public class NewProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public HttpPostedFileBase[] Files { get; set; }
        public IList<int> CategoryIds { get; set; }  
    }
}