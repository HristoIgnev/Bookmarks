﻿namespace Bookmarks.Web
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Bookmarks.Web.Infrastructure.Mapping;
    using System.Reflection;
    using System.Web.Http;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            AutoMapperConfig.RegisterMappings(Assembly.GetExecutingAssembly());
            ViewEngineConfig.RegisterViewEngines(ViewEngines.Engines);
        }
    }
}
