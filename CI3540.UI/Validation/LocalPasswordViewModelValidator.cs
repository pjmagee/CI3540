using CI3540.UI.Models;
using FluentValidation;

namespace CI3540.UI.Validation
{
    public class LocalPasswordViewModelValidator : AbstractValidator<LocalPasswordViewModel>
    {
        public LocalPasswordViewModelValidator()
        {
            RuleFor(m => m.OldPassword).NotEmpty();
            RuleFor(m => m.NewPassword).NotEmpty();
            RuleFor(m => m.ConfirmPassword).NotEmpty();
            RuleFor(m => m.ConfirmPassword).Equal(m => m.NewPassword);
        }
    }
}