using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CI3540.Core.Entities;
using CI3540.Infrastructure.EntityFramework;
using CI3540.UI.Areas.Admin.Models;
using CI3540.UI.Areas.Store.Models;
using CI3540.UI.BootstrapSupport.HtmlHelpers.Paging;
using CI3540.UI.Controllers;
using CI3540.UI.Services;
using Ninject;

namespace CI3540.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// http://stackoverflow.com/questions/4867602/entity-framework-there-is-already-an-open-datareader-associated-with-this-comma
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class CategoriesController : BootstrapBaseController
    {
        private StoreContext db = new StoreContext();

        private readonly ICategoryService categoryService;

        [Inject]
        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        /// <summary>
        /// http://stackoverflow.com/questions/3048979/using-like-operator-in-linq-to-entity
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Autocomplete(FormCollection collection)
        {
            string name = collection["name"];

            var categoryViewModels = categoryService.GetCategoriesByName(name);

            var projection = from category in categoryViewModels
                             select new { Id = category.Id, Name = category.Name, Description = category.Description };

            return Json(projection);
        }

        [HttpGet]
        public JsonResult ValidateCategoryName(string name)
        {
            bool valid = !categoryService.CategoryExists(name);
            return Json(valid, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Index(int? page, int? categoryId, string name, int pageSize = 10)
        {
            var pageNumber = (page ?? 1);
            var categories = categoryService.GetCategories(categoryId, name);
            ViewBag.CurrentName = name;
            ViewBag.Categories = new SelectList(categoryService.GetParentCategories(), "Id", "Name", categoryId);
            return View("Index", categories.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var category = categoryService.GetCategoryById(id);
            return View(category);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var categories = categoryService.GetCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            var categories = categoryService.GetCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            if (ModelState.IsValid)
            {
                try
                {
                    var categoryViewModel = categoryService.CreateCategory(model);
                    Success(string.Format("Category [{0}] with id [{1}] created.", categoryViewModel.Name, categoryViewModel.Id));
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    Error(e.Message);
                    return View("Create", model);
                }
            }

            return View("Create", model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var category = categoryService.GetCategoryById(id);
            var categories = categoryService.GetCategories().Where(c => c.Id != id).ToList();
            var selectedParent = categories.Find(c => c.Id == category.ParentId);
            ViewBag.Categories = new SelectList(categories, "Id", "Name", selectedParent);
            ViewBag.ChildCategories = new MultiSelectList(categories, "Id", "Name", category.Children.Select(c => c.Id));
            return View(category);
        }
        
        [HttpPost]
        public ActionResult Edit(int id, CategoryViewModel model)
        {
            var categories = categoryService.GetCategories().Where(c => c.Id != id).ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", categories.Find(c => c.Id == model.ParentId));

            if (ModelState.IsValid)
            {
                try
                {
                    var category = categoryService.UpdateCategory(model);
                    Success(string.Format("Category [{0}] with id [{1}] was modified.", category.Name, category.Id));

                    return RedirectToAction("Index");
                }
                catch(Exception e)
                {
                    Error(e.Message);
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<CategoryViewModel>(category);

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var category = db.Categories.Find(id);
            var model = Mapper.Map<Category, CategoryViewModel>(category);

            try
            {
                db.Categories.Remove(category);
                db.SaveChanges();
                Success(string.Format("Category [{0}] with id [{1}] was removed.", category.Name, category.Id));
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                Error(e.Message);
            }

            return View(model);
        }
    }
}
