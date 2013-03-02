using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CI3540.UI.Services;
using Ninject;

namespace CI3540.UI.Filters
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class DynamicCategoriesAttribute : ActionFilterAttribute
    {
        [Inject]
        public ICategoryService CategoryService { get; set; }
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Add Categories sir!
            filterContext.Controller.ViewBag.Categories = CategoryService.GetCategories();
        }
    }
}