using CI3540.Core.Entities;
using FluentValidation;

namespace CI3540.UI.Validation
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(order => order.Id).NotNull();
        }
    }
}