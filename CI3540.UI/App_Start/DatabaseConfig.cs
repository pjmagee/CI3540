using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;

namespace CI3540.UI.App_Start
{
    public class DatabaseConfig
    {
        public static void RegisterDatabase()
        {
            ConfigureAppHarbor();
        }

        private static void ConfigureAppHarbor()
        {
            // AppHarbor 
            // http://support.appharbor.com/kb/add-ons/using-sequelizer
            //
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["SQLSERVER_URI"]))
            {
                var configuration = WebConfigurationManager.OpenWebConfiguration("~");
                var uriString = ConfigurationManager.AppSettings["SQLSERVER_URI"];
                var uri = new Uri(uriString);

                var sb = new SqlConnectionStringBuilder
                {
                    DataSource = uri.Host,
                    InitialCatalog = uri.AbsolutePath.Trim('/'),
                    UserID = uri.UserInfo.Split(':').First(),
                    Password = uri.UserInfo.Split(':').Last(),
                    MultipleActiveResultSets = true
                };

                configuration.ConnectionStrings.ConnectionStrings["DefaultConnection"].ConnectionString = sb.ConnectionString;
                configuration.Save();
            }
        }
    }
}