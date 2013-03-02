using System.ComponentModel.DataAnnotations;

namespace CI3540.UI.Models
{
    public class LocalPasswordViewModel
    {
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm new password")]
        public string ConfirmPassword { get; set; }
    }
}