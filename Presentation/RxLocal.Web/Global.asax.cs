using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RxLocal.Core;
using RxLocal.Core.Configuration;
using RxLocal.Core.Infrastructure;

namespace RxLocal.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Initialize engine context
            EngineContext.Initialize(false);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Ignore static resources
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            if (webHelper.IsStaticResource(Request))
                return;
        }
    }
}
