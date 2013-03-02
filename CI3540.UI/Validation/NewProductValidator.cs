using CI3540.UI.Areas.Admin.Models;
using FluentValidation;

namespace CI3540.UI.Validation
{
    public class NewProductValidator : AbstractValidator<NewProductViewModel>
    {
        public NewProductValidator()
        {
            // http://fluentvalidation.codeplex.com/wikipage?title=Validators&referringTitle=Documentation&ANCHOR#Regex

            RuleFor(model => model.Name).NotEmpty().Matches("^[a-zA-Z0-9 _]+$").WithMessage("Letters and numbers only"); // Letters, numbers, spaces and underscore only
            RuleFor(model => model.Description).NotEmpty();
            RuleFor(model => model.Stock).NotEmpty();
            RuleFor(model => model.Price).NotEmpty();
            RuleFor(model => model.Files);
        }
    }
}