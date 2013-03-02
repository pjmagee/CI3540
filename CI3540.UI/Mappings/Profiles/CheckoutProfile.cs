using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CI3540.UI.Areas.Store.Models;
using CI3540.UI.Models;

namespace CI3540.UI.Mappings.Profiles
{
    public class CheckoutProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AddressViewModel, DeliveryAddressViewModel>()
                .ForMember(model => model.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}