using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CI3540.Core.Entities;
using CI3540.UI.Areas.Admin.Models;
using CI3540.UI.Areas.Store.Models;
using CI3540.UI.Models;
using CI3540.UI.Utils;

namespace CI3540.UI.Mappings.Profiles
{
    public class OrderProfile : Profile
    {
        protected override void Configure()
        {
            // source --> destination
            CreateMap<Order, OrderViewModel>()
                .ForMember(model => model.Total, opt => { opt.NullSubstitute(0m); opt.MapFrom(order => order.Total); })
                .ForMember(model => model.Tax, opt => opt.ResolveUsing<OrderTax>())
                .ForMember(model => model.DateCreated, opt => opt.MapFrom(order => order.DateCreated))
                .ForMember(model => model.DateModified, opt => opt.MapFrom(order => order.DateModified))
                .ForMember(model => model.Status, opt => opt.MapFrom(order => order.Status.DescriptionAttr()))
                .ForMember(model => model.StatusId, opt => opt.MapFrom(order => (int)order.Status))
                .ForMember(model => model.OrderLineViewModels, opt => { opt.NullSubstitute(new List<OrderLineViewModel>()); opt.ResolveUsing(OrderLineResolver); });

            // source --> destination
            CreateMap<Address, AddressViewModel>()
                .ForMember(model => model.AddressLine1, opt => opt.ResolveUsing(address => address.AddressLine1))
                .ForMember(model => model.AddressLine2, opt => opt.ResolveUsing(address => address.AddressLine2))
                .ForMember(model => model.City, opt => opt.ResolveUsing(address => address.City))
                .ForMember(model => model.County, opt => opt.MapFrom(address => address.County))
                .ForMember(model => model.PostCode, opt => opt.MapFrom(address => address.PostCode))
                .ForMember(model => model.Id, opt => opt.MapFrom(address => address.Id));

            // source --> destination
            CreateMap<Order, OrderSummaryViewModel>()
                .ForMember(model => model.OrderId, opt => opt.MapFrom(order => order.Id))
                .ForMember(model => model.Status, opt => opt.MapFrom(order => order.Status.DescriptionAttr()))
                .ForMember(model => model.OrderSubmitted, opt => opt.MapFrom(order => order.DateCreated.ToShortDateString()))
                .ForMember(model => model.CustomerId, opt => opt.MapFrom(order => order.CustomerId))
                .ForMember(model => model.Items, opt => opt.MapFrom(order => order.OrderLines.Sum(li => li.Quantity)));

        }

        private List<OrderLineViewModel> OrderLineResolver(Order order)
        {
            return order.OrderLines.Select(ol => new OrderLineViewModel()
            {
                Id = ol.Id,
                ProductId = ol.Product.Id,
                ProductName = ol.Product.Name,
                Quantity = ol.Quantity,
                Total = (ol.Product.Price * ol.Quantity),
                Status = ol.Status.ToString(),

            }).ToList();
        }

        private class OrderTax : ValueResolver<Order, decimal>
        {
            protected override decimal ResolveCore(Order source)
            {
                var total = source.OrderLines.Sum(ol => ol.Product.Price * ol.Quantity);
                return total - (total / (1 + 0.175m));
            }
        }
    }
}