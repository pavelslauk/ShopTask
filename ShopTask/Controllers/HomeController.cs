﻿using System;
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

                return PartialView("CategoriesPartial", categories);
            }
        }

        [HttpPost]
        public ActionResult UpdateCategories(Category[] categories)
        {
            try
            {
                ModelState.Clear();
                using (var dbContext = new ShopContext())
                {
                    UpdateAllCategories(categories, dbContext);
                    dbContext.SaveChanges();
                    return CategoriesPartial();
                }               
            }
            catch (DbUpdateConcurrencyException e)
            {
                Logger.Default.Warn(e);
                ModelState.AddModelError("UpdateFailed", "There is some error");
                return CategoriesPartial();
            }
        }

        private void UpdateAllCategories(Category[] categories, ShopContext dbContext)
        {
            foreach (var category in categories)
            {
                if (category.Id == null)
                {
                    AddNewCategory(category, dbContext);
                }
                else
                {
                    UpdateExistingCategory(category, dbContext);
                }
            }
        }

        private void UpdateExistingCategory(Category existingCategory, ShopContext dbContext)
        {
            if (!string.IsNullOrEmpty(existingCategory.Name))
            {
                dbContext.Entry(existingCategory).State = EntityState.Modified;
            }
            else
            {
                dbContext.Categories.Attach(existingCategory);
                dbContext.Categories.Remove(existingCategory);
            }
        }      
        
        private void AddNewCategory(Category newCategory, ShopContext dbContext)
        {
            if (!string.IsNullOrEmpty(newCategory.Name))
            {
                dbContext.Categories.Add(newCategory);
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