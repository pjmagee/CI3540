using System;
using System.Linq;
using CI3540.Infrastructure.EntityFramework;
using CI3540.UI.Models;
using CI3540.UI.Validation.Validator;
using FluentValidation;

namespace CI3540.UI.Validation
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(m => m.Email).EmailAddress().SetValidator(new RemoteValidator("That email has already been taken.", "ValidateEmail", "Account"));
            RuleFor(m => m.Password).NotEmpty();
            RuleFor(m => m.ConfirmPassword).Equal(m => m.Password).WithMessage("Confirmation password should match password");

            RuleFor(m => m.Forename).NotEmpty();
            RuleFor(m => m.Surname).NotEqual(m => m.Forename);

            // Regex was taken from this source
            // http://aa-asterisk.org.uk/index.php/Regular_Expressions_for_Validating_and_Formatting_GB_Telephone_Numbers
            // Match GB telephone number in any format
            RuleFor(m => m.PhoneNumber).Matches(@"^\(?(?:(?:0(?:0|11)\)?[\s-]?\(?|\+)44\)?[\s-]?\(?(?:0\)?[\s-]?\(?)?|0)(?:\d{2}\)?[\s-]?\d{4}[\s-]?\d{4}|\d{3}\)?[\s-]?\d{3}[\s-]?\d{3,4}|\d{4}\)?[\s-]?(?:\d{5}|\d{3}[\s-]?\d{3})|\d{5}\)?[\s-]?\d{4,5}|8(?:00[\s-]?11[\s-]?11|45[\s-]?46[\s-]?4\d))(?:(?:[\s-]?(?:x|ext\.?\s?|\#)\d+)?)$")
                .WithMessage("Must be a valid contact number");

            RuleFor(m => m.Address).SetValidator(new AddressViewModelValidator());
        }

        // Using remote validation instead
        private bool Predicate(string email)
        {
            using (var context = new StoreContext())
            {
                if (context.Users.Any(user => user.Email == email))
                    return false;

                return true;
            }

        }
    }
}