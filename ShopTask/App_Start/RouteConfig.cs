using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopTask
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Categories",
                url: "Categories",
                defaults: new { controller = "Home", action = "Categories" });

            routes.MapRoute(
                name: "CreateProduct",
                url: "Product",
                defaults: new { controller = "Home", action = "CreateProduct" });

            routes.MapRoute(
                name: "EditProduct",
                url: "Product/{productId}",
                defaults: new { controller = "Home", action = "EditProduct" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
