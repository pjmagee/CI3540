using AutoMapper;
using CI3540.Core.Entities;
using CI3540.UI.Areas.Admin.Models;
using CI3540.UI.Areas.Store.Models;

namespace CI3540.UI.Mappings.Profiles
{
    public class ImageProfile : Profile
    {
        protected override void Configure()
        {
            // source --> destination
            CreateMap<ProductImage, ProductImageViewModel>()
                .ForMember(model => model.Default, opt => opt.MapFrom(image => image.Primary))
                .ForMember(model => model.Url, opt => opt.MapFrom(image => image.Path))
                .ForMember(model => model.Delete, opt => opt.Ignore())
                .ForMember(model => model.Id, opt => opt.MapFrom(image => image.Id))
                .ForMember(model => model.ProductId, opt => opt.MapFrom(image => image.ProductId));
        }
    }
}