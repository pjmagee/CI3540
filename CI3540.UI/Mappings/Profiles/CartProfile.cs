using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CI3540.Core.Entities;
using CI3540.UI.Areas.Store.Models;

namespace CI3540.UI.Mappings.Profiles
{
    public class CartProfile : Profile
    {
        protected override void Configure()
        {
            // source --> destination
            CreateMap<Cart, CartViewModel>()
                .ForMember(model => model.Total, opt => { opt.NullSubstitute(0m); opt.ResolveUsing<CartTotal>(); })
                .ForMember(model => model.Tax, opt => { opt.NullSubstitute(0m); opt.ResolveUsing<CartTax>(); })
                .ForMember(model => model.OrderLineViewModels, opt => { opt.NullSubstitute(new List<OrderLineViewModel>());  opt.ResolveUsing(OrderLineResolver); });
        }

        private List<OrderLineViewModel> OrderLineResolver(Cart cart)
        {
            return cart.OrderLines.Select(ol => new OrderLineViewModel()
                {
                    Id = ol.Id,
                    ProductId = ol.Product.Id,
                    ProductName = ol.Product.Name,
                    Quantity = ol.Quantity,
                    Total = (ol.Product.Price*ol.Quantity),
                    Status = "Pending",
                })
                .ToList();
        }

        class CartTax : ValueResolver<Cart, decimal>
        {
            protected override decimal ResolveCore(Cart source)
            {
                var total = source.OrderLines.Sum(ol => ol.Product.Price * ol.Quantity);
                return (total / (1 + 0.175m));
            }
        }

        class CartTotal : ValueResolver<Cart, decimal>
        {
            protected override decimal ResolveCore(Cart source)
            {
                return source.OrderLines.Sum(line => line.Product.Price * line.Quantity);
            }
        }
    }
}



