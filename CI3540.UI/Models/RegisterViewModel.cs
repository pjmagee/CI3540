using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CI3540.UI.Models
{
    public class RegisterViewModel
    {
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public AddressViewModel Address { get; set; }
    }
}