using System.Web.Mvc;

namespace ShopTask.Areas.Inventory
{
    public class InventoryAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Inventory";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "Categories",
                url: "Inventory/Categories",
                defaults: new { controller = "Categories", action = "Index" });

            context.MapRoute(
                name: "CreateProduct",
                url: "Inventory/Product",
                defaults: new { controller = "Products", action = "CreateProduct" });

            context.MapRoute(
                name: "EditProduct",
                url: "Inventory/Product/{productId}",
                defaults: new { controller = "Products", action = "EditProduct" });

            context.MapRoute(
                 "Inventory_default",
                 "Inventory/{controller}/{action}/{id}",
                 new { controller = "Products", action = "Index", id = UrlParameter.Optional });
        }
    }
}