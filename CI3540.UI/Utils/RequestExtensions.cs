using System;
using System.Linq;
using System.Web.Routing;

namespace CI3540.UI.Utils
{
    /// <summary>
    /// From Geeks With Blogs
    /// http://geekswithblogs.net/bdiaz/archive/2010/04/09/handy-asp.net-mvc-2-extension-methods-ndash-where-am-i.aspx
    /// </summary>
    public static class RequestExtensions
    {
        public static bool IsCurrentRoute(this RequestContext context, string areaName, string controllerName, params string[] actionNames)
        {
            var routeData = context.RouteData;
            var routeArea = routeData.DataTokens["area"] as string;

            bool current = 
                ((string.IsNullOrEmpty(routeArea) && string.IsNullOrEmpty(areaName)) || 
                (routeArea == areaName)) && ((string.IsNullOrEmpty(controllerName)) || 
                (routeData.GetRequiredString("controller") == controllerName)) && ((actionNames == null) || 
                actionNames.Contains(routeData.GetRequiredString("action")));
            
            return current;
        }

        public static bool IsCurrentCategoryRoute(this RequestContext context, string areaName, string controllerName, string id, params string[] actionNames)
        {
            var routeData = context.RouteData;
            var routeArea = routeData.DataTokens["area"] as string;

            object categoryId = null;

            if(routeData.Values.TryGetValue("categoryId", out categoryId))
            {
                return (actionNames.Contains(routeData.GetRequiredString("action")) && Convert.ToString(categoryId) == id && (routeArea == areaName));
            }

            return false;
        }
    }
}