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
                name: "SaveNewProduct",
                url: "Inventory/SaveNewProduct",
                defaults: new { controller = "Products", action = "SaveNewProduct" });

            context.MapRoute(
                name: "SaveChangedProduct",
                url: "Inventory/SaveChangedProduct",
                defaults: new { controller = "Products", action = "SaveChangedProduct" });

            context.MapRoute(
                name: "DeleteProduct",
                url: "Inventory/DeleteProduct",
                defaults: new { controller = "Products", action = "DeleteProduct" });

            context.MapRoute(
                name: "GetCategories",
                url: "Inventory/GetCategories",
                defaults: new { controller = "Products", action = "GetCategoriesAsync" });

            context.MapRoute(
                name: "Products",
                url: "Inventory/{param}",
                defaults: new { controller = "Products", action = "Index", param = UrlParameter.Optional });

            context.MapRoute(
                 "Inventory_default",
                 "Inventory/{controller}/{action}/{id}",
                 new { controller = "Products", action = "Index", id = UrlParameter.Optional });
        }
    }
}