using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace CI3540.UI.BootstrapSupport
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString ActionMenuItem(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
        {
            var tag = new TagBuilder("li");

            if (htmlHelper.ViewContext.RequestContext.IsCurrentRoute(null, controllerName, actionName))
            {
                tag.AddCssClass("selected");
            }

            tag.InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName).ToString();

            return MvcHtmlString.Create(tag.ToString());
        }

        public static MvcHtmlString ActionMenuItem(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues)
        {
            var tag = new TagBuilder("li");

            if (htmlHelper.ViewContext.RequestContext.IsCurrentRoute(null, controllerName, actionName))
            {
                tag.AddCssClass("selected");
            }

            tag.InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, null).ToString();

            return MvcHtmlString.Create(tag.ToString());
        }

        public static MvcHtmlString ActionMenuItem(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string areaName, object routeValues)
        {
            var tag = new TagBuilder("li");

            if (htmlHelper.ViewContext.RequestContext.IsCurrentRoute(areaName, controllerName, actionName))
            {
                tag.AddCssClass("selected");
            }

            tag.InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, null).ToString();

            return MvcHtmlString.Create(tag.ToString());
        }

        public static MvcHtmlString CategoryMenuItem(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string areaName, object routeValues)
        {
            var tag = new TagBuilder("li");

            RouteValueDictionary dictionary = new RouteValueDictionary(routeValues);
            string categoryId = Convert.ToString(dictionary["categoryId"]);

            if (htmlHelper.ViewContext.RequestContext.IsCurrentCategoryRoute(areaName, controllerName, categoryId, actionName))
            {
                tag.AddCssClass("active");
            }

            tag.InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, null).ToString();

            return MvcHtmlString.Create(tag.ToString());
        }

        public static MvcHtmlString CategoryHeaderItem(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string areaName, object routeValues)
        {
            var tag = new TagBuilder("li");
            tag.AddCssClass("nav-header");

            RouteValueDictionary dictionary = new RouteValueDictionary(routeValues);
            var categoryId = Convert.ToString(dictionary["categoryId"]);

            if (htmlHelper.ViewContext.RequestContext.IsCurrentCategoryRoute(areaName, controllerName, categoryId, actionName))
            {
                tag.AddCssClass("active");
            }

            tag.InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, null).ToString();

            return MvcHtmlString.Create(tag.ToString());
        }


        public static MvcHtmlString TryPartial(this HtmlHelper helper, string viewName, object model)
        {
            try
            {
                return helper.Partial(viewName, model);
            }
            catch (Exception)
            {

            }
            return MvcHtmlString.Empty;
        }
    }
}