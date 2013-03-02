using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using CI3540.Infrastructure.EntityFramework;
using CI3540.UI.App_Start;
using WebMatrix.WebData;

namespace CI3540.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            AutoMapperConfig.RegisterConfig();

            Database.SetInitializer(new StoreContextInitializer());

            using (var context = new StoreContext())
            {
                context.Database.Initialize(false);
            }

            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection(
                    connectionStringName: "DefaultConnection", 
                    userTableName: "User", 
                    userIdColumn: "Id", 
                    userNameColumn: "Email", 
                    autoCreateTables: false);
            }


        }
    }
}