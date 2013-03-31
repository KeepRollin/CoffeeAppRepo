using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CoffeeApp.Web.Infrastructure;
using CoffeeApp.Domain.Entities;
using CoffeeApp.Web.Binders;

namespace CoffeeApp.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Coffee", action = "List", id = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute(
                "", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Order", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute(
               "", // Route name
               "{controller}/{action}", // URL with parameters
               new { controller = "Cart", action = "Index"} // Parameter defaults
           );
            routes.MapRoute(null, "{controller}/{action}");
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}