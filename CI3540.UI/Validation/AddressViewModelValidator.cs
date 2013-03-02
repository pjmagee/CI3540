using CI3540.UI.Models;
using FluentValidation;

namespace CI3540.UI.Validation
{
    public class AddressViewModelValidator : AbstractValidator<AddressViewModel>
    {
        public AddressViewModelValidator()
        {
            RuleFor(m => m.AddressLine1).NotEmpty();
            RuleFor(m => m.AddressLine2);
            RuleFor(m => m.City).NotEmpty();
            RuleFor(m => m.County).NotEmpty();
            RuleFor(m => m.PostCode).Matches("^([A-PR-UWYZ0-9][A-HK-Y0-9][AEHMNPRTVXY0-9]?[ABEHMNPRVWXY0-9]? {1,2}[0-9][ABD-HJLN-UW-Z]{2}|GIR 0AA)$");
        }
    }
}