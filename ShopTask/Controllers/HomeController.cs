using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopTask.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

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

            using (var dbContext = new ShopContext())
            {
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditProduct(int productId)
        {
            using (var dbContext = new ShopContext())
            {
                Product product = dbContext.Products.Find(productId);
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

            using (var dbContext = new ShopContext())
            {
                dbContext.Entry(product).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public bool DeleteProduct(int productId)
        {
            using (var dbContext = new ShopContext())
            {
                try
                {
                    Product product = new Product();
                    product.Id = productId;
                    dbContext.Products.Attach(product);
                    dbContext.Products.Remove(product);
                    dbContext.SaveChanges();
                    return true;
                }
                catch(DbUpdateConcurrencyException e)
                {
                    Product product = dbContext.Products.FirstOrDefault(p => p.Id == productId);
                    if (product == null)
                    {
                        return true;
                    }
                    return false;
                }                                
            }
        }

        public ActionResult ViewTable()
        {
            using (var dbContext = new ShopContext())
            {
                List<Product> products = dbContext.Products.ToList();
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