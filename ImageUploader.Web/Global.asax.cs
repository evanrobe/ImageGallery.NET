using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Xml.Linq;

namespace ImageUploader.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            string correctConString = "Data Source=" + System.Web.Hosting.HostingEnvironment.MapPath(" / ") + "imageDB.sdf;Persist Security Info=False;";
            string correctImageDir = System.Web.Hosting.HostingEnvironment.MapPath(" / ") + "images/";

            // a way to make the configuration of the project simpler.
            if (System.Configuration.ConfigurationManager.ConnectionStrings["imageDB"].ConnectionString != correctConString)
            {
                ChangeConnectionString("imageDB", correctConString);
            }

            if (System.Configuration.ConfigurationManager.AppSettings["imageStorageLocation"] != correctConString)
            {
                ChangeAppSetting("imageStorageLocation", correctImageDir);
            }

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private bool ChangeConnectionString(string connStringName, string newValue)
        {
            var settings = ConfigurationManager.ConnectionStrings[connStringName];
            var fi = typeof(ConfigurationElement).GetField(
                          "_bReadOnly",
                          BindingFlags.Instance | BindingFlags.NonPublic);
            fi.SetValue(settings, false);
            settings.ConnectionString = newValue;
            return true;
        }

        private bool ChangeAppSetting(string name, string newValue)
        {
            ConfigurationManager.AppSettings[name] = newValue;
            return true;
        }
    }
}
