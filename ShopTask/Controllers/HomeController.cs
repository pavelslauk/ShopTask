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

        public ActionResult ViewTable()
        {
            using (var dbContext = new ShopContext())
            {
                var products = dbContext.Products.ToList();

                return PartialView("TablePartialView", products);
            }
        }

        public ActionResult CategoryForm()
        {
            return View();
        }

        public ActionResult ViewCategoryTable()
        {
            using (var dbContext = new ShopContext())
            {
                var categories = dbContext.Categories.ToArray();

                return PartialView("CategoryTablePartialView", categories);
            }
        }

        public ActionResult ChangeCategories(string newCategoryName, Category[] changedCategories)
        {
            AddNewCategory(newCategoryName);
            if(changedCategories != null)
            {
                ChangeCategoriesInternal(changedCategories);
            }           

            return ViewCategoryTable();
        }

        private void ChangeCategoriesInternal(Category[] changedCategories)
        {
            for (int i = 0; i < changedCategories.Length; i++)
            {
                if (changedCategories[i].Name != null)
                {
                    UpdateCategory(changedCategories[i]);
                }
                else
                {
                    DeleteCategory(changedCategories[i]);
                }
            }
        }      

        private void UpdateCategory(Category updatedCategory)
        {
            using (var dbContext = new ShopContext())
            {
                dbContext.Entry(updatedCategory).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }

        private void DeleteCategory(Category deletedCategory)
        {
            using (var dbContext = new ShopContext())
            {
                try
                {
                    dbContext.Categories.Attach(deletedCategory);
                    dbContext.Categories.Remove(deletedCategory);
                    dbContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    Logger.Default.Warn(e);
                }
            }
        }

        private void AddNewCategory(string newCategoryName)
        {
            if (newCategoryName != "")
            {
                using (var dbContext = new ShopContext())
                {
                    var category = new Category { Name = newCategoryName };
                    dbContext.Categories.Add(category);
                    dbContext.SaveChanges();
                }
            }
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