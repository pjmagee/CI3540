using System;
using CI3540.UI.Areas.Store.Models;
using CI3540.UI.Models;
using FluentValidation;
using FluentValidation.Validators;

namespace CI3540.UI.Validation
{
    public class DeliveryViewModelValidator : AbstractValidator<DeliveryViewModel>
    {
        private const string PostCodeRegex = "^([A-PR-UWYZ0-9][A-HK-Y0-9][AEHMNPRTVXY0-9]?[ABEHMNPRVWXY0-9]? {1,2}[0-9][ABD-HJLN-UW-Z]{2}|GIR 0AA)$";

        public DeliveryViewModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(model => model.DeliveryAddressId).NotNull().WithMessage("Please select a delivery address.");
            RuleFor(model => model.InvoiceAddressId).NotNull().WithMessage("Please select a invoice address.");
        }

    }
}