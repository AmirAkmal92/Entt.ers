using System;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Entt.Ers
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //var datetimeBinder = new UTCDateTimeModelBinder();
            //ModelBinders.Binders.Add(typeof(DateTime), datetimeBinder);
            //ModelBinders.Binders.Add(typeof(DateTime?), datetimeBinder);
        }

        protected void Application_BeginRequest()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-MY");
        }
    }
}
