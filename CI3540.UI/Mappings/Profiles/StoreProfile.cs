using System;
using System.Collections.Generic;
using AutoMapper;
using CI3540.UI.Areas.Admin.Models;
using CI3540.UI.Areas.Store.Models;

namespace CI3540.UI.Mappings.Profiles
{
    public class StoreProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Tuple<List<ProductViewModel>, List<CategoryViewModel>, CartViewModel>, ListingViewModel>()
                .ForMember(model => model.ProductViewModels, opt => opt.MapFrom(tuple => tuple.Item1))
                .ForMember(model => model.CategoryViewModels, opt => opt.MapFrom(tuple => tuple.Item2))
                .ForMember(model => model.CartViewModel, opt =>
                    {
                        opt.MapFrom(tuple => tuple.Item3);
                        opt.NullSubstitute(new CartViewModel() { OrderLineViewModels = new List<OrderLineViewModel>() });
                    });
        }
    }
}