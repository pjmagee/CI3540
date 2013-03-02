using System.Web.Mvc;

namespace CI3540.UI.Areas.Store
{
    public class StoreAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Store"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "StoreProducts",
                url: "Store/{controller}/{action}/{categoryId}/{categoryName}/",
                defaults: new { controller = "Products", action = "Index", categoryId = UrlParameter.Optional, categoryName = UrlParameter.Optional },
                namespaces: new [] { "CI3540.UI.Areas.Store.*"}
            );

            context.MapRoute(
                name: "StoreDefault",
                url: "Store/{controller}/{action}/{id}",
                defaults: new { controller = "Products", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "CI3540.UI.Areas.Store.*" });
        }
    }
}
