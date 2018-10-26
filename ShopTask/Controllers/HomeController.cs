using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopTask.Models;
using System.Data.Entity;

namespace ShopTask.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            using (ShopContext dbContext = new ShopContext())
            {
                dbContext.products.Add(product);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditProduct(int productId)
        {
            using (ShopContext dbContext = new ShopContext())
            {
                Product product = dbContext.products.Find(productId);
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            using (ShopContext dbContext = new ShopContext())
            {
                dbContext.Entry(product).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteProduct(int productId)
        {
            using (ShopContext dbContext = new ShopContext())
            {
                Product product = dbContext.products.Find(productId);
                dbContext.products.Remove(product);
                dbContext.SaveChanges();
            }
            return ViewTable();
        }

        public ActionResult ViewTable()
        {
            using (ShopContext dbContext = new ShopContext())
            {
                List<Product> products = dbContext.products.ToList();
                return PartialView("TablePartialView" ,products);
            }
        }

        [HttpGet]
        public JsonResult CheckPrice(decimal price)
        {
            bool result = (price > 0)&&(price<=1000000);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}