using CI3540.Core.Entities;
using FluentValidation;

namespace CI3540.UI.Validation
{
    // http://fluentvalidation.codeplex.com/wikipage?title=Validators&referringTitle=Documentation

    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.Id).NotNull();
            RuleFor(customer => customer.PhoneNumber).NotEmpty();
            RuleFor(customer => customer.Email).EmailAddress();
        }
    }
}