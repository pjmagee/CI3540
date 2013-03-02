using System.Linq;
using AutoMapper;
using CI3540.Core.Entities;
using CI3540.UI.Areas.Admin.Models;
using CI3540.UI.Areas.Store.Models;

namespace CI3540.UI.Mappings.Profiles
{
    public class CategoryProfile : Profile
    {
        protected override void Configure()
        {
            // source --> destination
            CreateMap<Category, CategoryViewModel>()
                .ForMember(model => model.NumberOfProducts, opt => opt.MapFrom(category => category.Products.Count))
                .ForMember(model => model.IsParentCategory, opt => opt.MapFrom(category => category.Parent == null))
                .ForMember(model => model.HasChildCategories, opt => opt.ResolveUsing(category => category.Children.Any()))
                .ForMember(model => model.NumberOfCategories, opt => opt.MapFrom(category => category.Children.Count));

            // source --> destination
            CreateMap<NewCategoryViewModel, Category>()
                .ForMember(category => category.Id, opt => opt.Ignore())
                .ForMember(category => category.Products, opt => opt.Ignore())
                .ForMember(category => category.Parent, opt => opt.Ignore())
                .ForMember(category => category.Children, opt => opt.Ignore())
                .ForMember(category => category.ParentId, opt => opt.MapFrom(model => model.ParentId));
        }
    }
}