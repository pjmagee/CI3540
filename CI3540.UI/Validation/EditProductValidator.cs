using System;
using System.Linq;
using CI3540.UI.Areas.Admin.Models;
using FluentValidation;
using FluentValidation.Validators;

namespace CI3540.UI.Validation
{
    public class EditProductValidator : AbstractValidator<EditProductViewModel>
    {
        public EditProductValidator()
        {
            RuleFor(model => model.Name).NotEmpty().Matches("^[a-zA-Z0-9 _]+$").WithMessage("Letters and numbers only"); // Letters, numbers, spaces and underscore only
            RuleFor(model => model.Description).NotEmpty();
            RuleFor(model => model.Stock).GreaterThanOrEqualTo(0);
            RuleFor(model => model.Price).GreaterThan(0.0m);
            RuleFor(model => model.SelectedDefaultImage).Must(NotBeMarkedForDelete).WithMessage("You can not delete an image marked as default");
        }

        private bool NotBeMarkedForDelete(EditProductViewModel editProductViewModel, int i, PropertyValidatorContext arg3)
        {
            if (editProductViewModel.Images.Any(model => model.Delete && model.Id == editProductViewModel.SelectedDefaultImage))
                return false;

            return true;
        }
    }
}