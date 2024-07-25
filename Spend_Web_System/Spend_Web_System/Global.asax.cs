using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Spend_Web_System
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


        
        }

        protected void Application_AuthorizeRequest(Object sender, EventArgs e)
        {
            if (this.Request.Path.ToUpper().EndsWith("AUTH/LOGIN") && this.Request.IsAuthenticated)
            {
                this.Response.Redirect("acessDenied");
            }
        }
    }
}
