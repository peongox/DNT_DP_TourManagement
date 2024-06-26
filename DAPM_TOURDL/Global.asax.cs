using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace DAPM_TOURDL
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Session_Start(object sender, EventArgs e)
        {
            HttpContext.Current.Session["BackgroundColor_Client"] = "#ffffff";
            HttpContext.Current.Session["BackgroundColor_Client_Header"] = "rgba(225,225,225,0.3)";
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
