using CI3540.UI.Models;
using FluentValidation;

namespace CI3540.UI.Validation
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(m => m.Email).NotEmpty();
            RuleFor(m => m.Password).NotEmpty();
        }
    }
}