using CI3540.Core.Entities;
using CI3540.UI.Areas.Admin.Models;
using CI3540.UI.Areas.Store.Models;
using FluentValidation;

namespace CI3540.UI.Validation
{
    public class ProductValidator : AbstractValidator<ProductViewModel>
    {
        public ProductValidator()
        {
            RuleFor(product => product.Name).NotEmpty().Length(5, 50);
            RuleFor(product => product.Description).NotEmpty();
            RuleFor(product => product.Stock).GreaterThanOrEqualTo(0);
            RuleFor(product => product.Price).GreaterThan(0.0m);
        }
    }
}