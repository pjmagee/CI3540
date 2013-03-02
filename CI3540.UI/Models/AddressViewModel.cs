using System.ComponentModel.DataAnnotations;

namespace CI3540.UI.Models
{
    public class AddressViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        [Display(Name = "Post Code")]
        public string PostCode { get; set; }
    }
}