using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CI3540.Core.Entities;
using CI3540.UI.Areas.Admin.Models;
using CI3540.UI.Areas.Store.Models;

namespace CI3540.UI.Mappings.Profiles
{
    public class ProductProfile : Profile
    {
        protected override void Configure()
        {
            // source --> destination
            CreateMap<NewProductViewModel, Product>()
                .ForMember(product => product.Id, opt => opt.Ignore())
                .ForMember(product => product.ProductImages, opt => opt.Ignore())
                .ForMember(product => product.Reviews, opt => opt.Ignore())
                .ForMember(product => product.Categories, opt => opt.Ignore())
                .ForMember(product => product.Created, opt => opt.MapFrom(model => DateTime.Now))
                .ForMember(product => product.Modified, opt => opt.MapFrom(model => DateTime.Now));

            // source --> destination
            CreateMap<EditProductViewModel, Product>()
                .ForMember(product => product.Id, opt => opt.Ignore())
                .ForMember(product => product.ProductImages, opt => opt.Ignore())
                .ForMember(product => product.Reviews, opt => opt.Ignore())
                .ForMember(product => product.Categories, opt => opt.Ignore())
                .ForMember(product => product.Created, opt => opt.Ignore())
                .ForMember(product => product.Modified, opt => opt.MapFrom(model => DateTime.Now));

            // source --> destination
            CreateMap<Product, ProductSummaryViewModel>()
                .ForMember(model => model.DefaultImageUrl, opt => opt.MapFrom(product => product.ProductImages.First(image => image.Primary).Path));

            // source --> destination
            CreateMap<Product, ProductViewModel>()
                .ForMember(model => model.Tags, opt => opt.ResolveUsing(CategoryTagResolver))
                .ForMember(model => model.DefaultImageUrl, opt => opt.MapFrom(product => product.ProductImages.First(image => image.Primary).Path))
                .ForMember(model => model.Images, opt => opt.ResolveUsing(ImagesResolver))
                .ForMember(model => model.Reviews, opt => opt.ResolveUsing(ReviewResolver));

            // source --> destination
            CreateMap<Product, EditProductViewModel>()
                .ForMember(model => model.Tags, opt => opt.ResolveUsing(CategoryTagResolver))
                .ForMember(model => model.Images, opt => opt.ResolveUsing(ImagesResolver))
                .ForMember(model => model.Files, opt => opt.Ignore())
                .ForMember(model => model.SelectedDefaultImage, opt => opt.MapFrom(product => product.ProductImages.SingleOrDefault(image => image.Primary).Id))
                .ForMember(model => model.CategoryIds, opt => opt.MapFrom(product => product.Categories.Select(category => category.Id)));
        }

        private IEnumerable<ReviewViewModel> ReviewResolver(Product product)
        {
            return product.Reviews.Select(review => new ReviewViewModel()
                {
                    Id = review.Id,
                    AuthorName = review.Customer.Forename,
                    Comment = review.Comment,
                    DatePosted = review.Created,
                    Rating = review.Rating
                });
        }

        private IEnumerable<ProductImageViewModel> ImagesResolver(Product product)
        {
            return product.ProductImages.Select(image => new ProductImageViewModel()
                {
                    Id = image.Id,
                    Default = image.Primary,
                    Url = image.Path
                });
        }

        private IEnumerable<CategoryTagViewModel> CategoryTagResolver(Product product)
        {
            return product.Categories.Select(category => new CategoryTagViewModel()
                {
                    CategoryDescription = category.Description,
                    CategoryName = category.Name,
                    CategoryId = category.Id
                });
        }
    }
}