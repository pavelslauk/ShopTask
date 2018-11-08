using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopTask.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ShopTask.Utils;

namespace ShopTask.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateProduct()
        {
            return View("ProductView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View("ProductView");
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
                var product = dbContext.Products.Find(productId);
                return View("ProductView", product);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View("ProductView");
            }

            using (var dbContext = new ShopContext())
            {
                dbContext.Entry(product).State = EntityState.Modified;
                dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult DeleteProduct(int productId)
        {
            var isDeleted = DeleteProductInternal(productId);

            return Json(isDeleted, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ProductsPartial()
        {
            using (var dbContext = new ShopContext())
            {
                var products = dbContext.Products.ToList();

                return PartialView("ProductsPartial", products);
            }
        }

        [HttpGet]
        public ActionResult Categories()
        {
            return View();
        }

        public ActionResult CategoriesPartial()
        {
            using (var dbContext = new ShopContext())
            {
                var categories = dbContext.Categories.ToList();
                categories.Add(new Category());
                ModelState.Clear();

                return PartialView("CategoriesPartial", categories);
            }
        }

        [HttpPost]
        public JsonResult UpdateCategories(List<Category> changedCategories)
        {
            try
            {
                using (var dbContext = new ShopContext())
                {
                    AddNewCategory(changedCategories, dbContext);
                    UpdateExistingCategories(changedCategories, dbContext);
                    dbContext.SaveChanges();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }               
            }
            catch (Exception e)
            {
                Logger.Default.Warn(e);
                return Json(false, JsonRequestBehavior.AllowGet);
            }          
        }

        private void UpdateExistingCategories(List<Category> changedCategories, ShopContext dbContext)
        {
            for (int i = 0; i < changedCategories.Count(); i++)
            {
                if (changedCategories[i].Name != null)
                {
                    UpdateCategory(changedCategories[i], dbContext);
                }
                else
                {
                    DeleteCategory(changedCategories[i], dbContext);
                }
            }
        }      

        private void UpdateCategory(Category updatedCategory, ShopContext dbContext)
        {
            dbContext.Entry(updatedCategory).State = EntityState.Modified;
        }

        private void DeleteCategory(Category deletedCategory, ShopContext dbContext)
        {
            dbContext.Categories.Attach(deletedCategory);
            dbContext.Categories.Remove(deletedCategory);         
        }

        private void AddNewCategory(List<Category> changedCategories, ShopContext dbContext)
        {
            if (changedCategories.Last().Name != null)
            {
                dbContext.Categories.Add(changedCategories.Last());
            }
            changedCategories.Remove(changedCategories.Last());
        }

        private bool DeleteProductInternal(int productId)
        {
            using (var dbContext = new ShopContext())
            {
                try
                {
                    var product = new Product { Id = productId };
                    dbContext.Products.Attach(product);
                    dbContext.Products.Remove(product);
                    dbContext.SaveChanges();
                    return true;
                }
                catch (DbUpdateConcurrencyException e)
                {
                    Logger.Default.Warn(e);
                    return !dbContext.Products.Any(product => product.Id == productId);
                }
            }
        }
    }
}