using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;
using CI3540.Core.Entities;
using CI3540.UI.Areas.Store.Models;
using CI3540.UI.Models;

namespace CI3540.UI.Mappings.Profiles
{
    public class CustomerProfile : Profile
    {
        protected override void Configure()
        {
            // source --> destination
            CreateMap<RegisterViewModel, Customer>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.Orders, opt => opt.Ignore())
                .ForMember(c => c.Reviews, opt => opt.Ignore())
                .ForMember(c => c.Cart, opt => opt.Ignore())
                .ForMember(c => c.CartId, opt => opt.Ignore())
                .ForMember(c => c.DateCreated, opt => opt.MapFrom(s => DateTime.Now))
                .ForMember(c => c.DateModified, opt => opt.MapFrom(s => DateTime.Now))
                .ForMember(c => c.Addresses, opt => opt.ResolveUsing(AddressResolver));

            // source --> destination
            CreateMap<Customer, CustomerViewModel>();
        }

        private ICollection<Address> AddressResolver(RegisterViewModel registerViewModel)
        {
            return new Collection<Address>()
                {
                    new Address()
                        {
                        
                            AddressLine1 = registerViewModel.Address.AddressLine1,
                            AddressLine2 = registerViewModel.Address.AddressLine2,
                            City = registerViewModel.Address.City,
                            County = registerViewModel.Address.County,
                            PostCode = registerViewModel.Address.PostCode
                        }
                };
        }
    }
}