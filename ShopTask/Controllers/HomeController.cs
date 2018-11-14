using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopTask.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ShopTask.Utils;
using ShopTask.DataAccess;
using ShopTask.DataAccess.Entities;
using AutoMapper;



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
            using (var dbContext = new ShopContext())
            {
                var productModel = new ProductModel { Categories = GetCategorySelectList(dbContext) };

                return View("ProductView", productModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(ProductModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return View("ProductView");
            }


            using (var dbContext = new ShopContext())
            {
                var product = Mapper.Map<ProductModel, Product>(productModel);
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
                var product = Mapper.Map<Product, ProductModel>(dbContext.Products.Find(productId));
                product.Categories = GetCategorySelectList(dbContext, product.CategoryId);

                return View("ProductView", product);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(ProductModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return View("ProductView");
            }

            using (var dbContext = new ShopContext())
            {
                var product = Mapper.Map<ProductModel, Product>(productModel);
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
                var products = dbContext.Products.Include(product => product.Category).ToList();

                return PartialView(products);
            }
        }

        [HttpGet]
        public ActionResult Categories()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CategoriesPartial()
        {
            using (var dbContext = new ShopContext())
            {
                var categories = Mapper.Map<List<Category>, List<CategoryModel>>(dbContext.Categories.ToList());
                categories.Add(new CategoryModel());
                return PartialView("CategoriesPartial", categories);
            }
        }

        [HttpPost]
        public ActionResult UpdateCategories(Category[] categories)
        {
            ModelState.Clear();
            var isUpdated = UpdateCategoriesInternal(categories);
            if (!isUpdated)
            {
                ModelState.AddModelError("UpdateFailed", "There is some error");
            }
            return CategoriesPartial();
        }

        private bool UpdateCategoriesInternal(Category[] categories)
        {
            try
            {
                using (var dbContext = new ShopContext())
                {
                    foreach (var category in categories)
                    {
                        AddOrUpdateCategory(category, dbContext);
                    }
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (DbUpdateConcurrencyException e)
            {
                Logger.Default.Warn(e);
                return false;
            }
        }

        private void AddOrUpdateCategory(Category category, ShopContext dbContext)
        {
            if (category.Id == 0)
            {
                AddNewCategory(category, dbContext);
            }
            else
            {
                UpdateExistingCategory(category, dbContext);
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

        private SelectList GetCategorySelectList(ShopContext dbContext, int currentCategory = 0)
        {
            if(currentCategory != 0)
            {
                return new SelectList(dbContext.Categories.ToList(), "Id", "Name", currentCategory);
            }
            return new SelectList(dbContext.Categories.ToList(), "Id", "Name");
        }
    }
}