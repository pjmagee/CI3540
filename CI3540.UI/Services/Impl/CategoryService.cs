using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CI3540.Core.Entities;
using CI3540.Infrastructure.EntityFramework;
using CI3540.UI.Areas.Admin.Models;
using CI3540.UI.Areas.Store.Models;
using Ninject;

namespace CI3540.UI.Services.Impl
{
    public class CategoryService : ICategoryService
    {
        private readonly StoreContext context;

        [Inject]
        public CategoryService(StoreContext context)
        {
            this.context = context;
        }

        public CategoryViewModel GetCategoryById(int id)
        {
            Category category = context.Categories.Find(id);
            return Mapper.Map<CategoryViewModel>(category);
        }

        public CategoryViewModel GetCategoryByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryViewModel> GetCategories()
        {
            List<Category> categories = context.Categories.ToList();
            return Mapper.Map<List<CategoryViewModel>>(categories);
        }

        public IEnumerable<CategoryViewModel> GetParentCategories()
        {
            List<Category> categories = context.Categories.Where(category => category.Parent == null).ToList();
            return Mapper.Map<List<CategoryViewModel>>(categories);
        }

        public IEnumerable<CategoryViewModel> GetCategoryByParentCategory(int? parentId)
        {
            List<Category> categories = context.Categories.Where(category => category.ParentId == parentId).ToList();
            return Mapper.Map<List<CategoryViewModel>>(categories);
        }

        public IEnumerable<CategoryViewModel> GetCategoriesByName(string name)
        {
            List<Category> categories = context.Categories.Where(c => c.Name.Contains(name)).ToList();
            return Mapper.Map<List<CategoryViewModel>>(categories);
        }

        public IEnumerable<CategoryViewModel> GetCategories(int? parentId, string name)
        {
            List<Category> categories = context.Categories.Where(category => category.ParentId == parentId).ToList();

            if (!string.IsNullOrEmpty(name))
            {
                categories = categories.Where(c => c.Name.Contains(name)).ToList();
            }

            return Mapper.Map<List<CategoryViewModel>>(categories);
        }

        public IEnumerable<CategoryTagViewModel> GetCategoryTagsFromCategoryIds(int[] categoryIds)
        {
            var tags = from id in categoryIds
                       select context.Categories.Find(id)
                       into category
                       where category != null
                       select new CategoryTagViewModel()
                           {
                               CategoryId = category.Id,
                               CategoryName = category.Name,
                               CategoryDescription = category.Description
                           };

            return tags.ToList();
        }

        public CategoryViewModel UpdateCategory(CategoryViewModel model)
        {
            var category = context.Categories.Find(model.Id);
            Mapper.Map(model, category);
            var parent = context.Categories.Find(model.ParentId);
            category.Parent = parent;
            context.SaveChanges();
            return Mapper.Map<CategoryViewModel>(category);
        }

        public bool CategoryExists(string name)
        {
            return context.Categories.Any(c => c.Name.ToUpper() == name.ToUpper());
        }

        public CategoryViewModel CreateCategory(NewCategoryViewModel model)
        {
            var category = Mapper.Map<Category>(model);
            context.Categories.Add(category);
            context.SaveChanges();
            return Mapper.Map<CategoryViewModel>(category);
        }
    }
}