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
                defaults: new { controller = "Categories", action = "Index" });

            routes.MapRoute(
                name: "CreateProduct",
                url: "{controller}/Product",
                defaults: new { action = "CreateProduct" });

            routes.MapRoute(
                name: "EditProduct",
                url: "{controller}/Product/{productId}",
                defaults: new { action = "EditProduct" });

            routes.MapRoute(
                name: "GetProducts",
                url: "Order/GetProducts",
                defaults: new { controller = "Order", action = "GetProductsAsync" });

            routes.MapRoute(
                name: "GetCart",
                url: "Order/GetCart",
                defaults: new { controller = "Order", action = "GetCart" });

            routes.MapRoute(
                name: "SaveCart",
                url: "Order/SaveCart",
                defaults: new { controller = "Order", action = "SaveCart" });

            routes.MapRoute(
                name: "SaveOrder",
                url: "Order/SaveOrder",
                defaults: new { controller = "Order", action = "SaveOrder" });

            routes.MapRoute(
                name: "Order",
                url: "Order/{param}",
                defaults: new { controller = "Order", action = "Index", param = UrlParameter.Optional });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Products", action = "Index", id = UrlParameter.Optional });
        }
    }
}
