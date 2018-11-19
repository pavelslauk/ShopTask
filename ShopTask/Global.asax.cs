using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ShopTask.Core.Utils;
using ShopTask.Utils;

namespace ShopTask
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure();
            AutoMapperConfig.Initialize();
            CastleWindsorInitializer.Initialize();
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            Logger.Default.Error(ex);
        }
    }
}
