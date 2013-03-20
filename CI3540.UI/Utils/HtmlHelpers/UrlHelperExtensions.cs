using System.ComponentModel;
using System.Web.Mvc;
using System.Web.Routing;

namespace CI3540.UI.Utils.HtmlHelpers
{
    /// <summary>
    /// From Andrei Volkov
    /// http://www.zvolkov.com/clog/2012/04/01/asp-net-mvc-build-url-based-on-current-url/
    /// </summary>
    public static class UrlHelperExtensions
    {
        public static bool IsCurrent(this UrlHelper urlHelper, string areaName, string controllerName, params string[] actionNames)
        {
            return urlHelper.RequestContext.IsCurrentRoute(areaName, controllerName, actionNames);
        }

        public static string Selected(this UrlHelper urlHelper, string areaName, string controllerName, params string[] actionNames)
        {
            return urlHelper.IsCurrent(areaName, controllerName, actionNames) ? "selected" : string.Empty;
        }

        public static MvcHtmlString Current(this UrlHelper helper, object substitutes)
        {
            var rd = new RouteValueDictionary(helper.RequestContext.RouteData.Values);
            var qs = helper.RequestContext.HttpContext.Request.QueryString;

            foreach (string param in qs)
                if (!string.IsNullOrEmpty(qs[param]))
                    rd[param] = qs[param];

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(substitutes.GetType()))
            {
                var value = property.GetValue(substitutes) as string;

                if (string.IsNullOrEmpty(value))
                    rd.Remove(property.Name);
                else
                    rd[property.Name] = value;

            }

            var url = helper.RouteUrl(rd);
            return new MvcHtmlString(url);
        }
    }
}