using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopTask.Models;
using System.Data.Entity.Infrastructure;
using ShopTask.Utils;
using ShopTask.DataAccess.Entities;
using ShopTask.DataAccess.Repositories;
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
            using (var unitOfWork = new UnitOfWork())
            {
                var productModel = new ProductModel { Categories = GetCategorySelectList(unitOfWork) };

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


            using (var unitOfWork = new UnitOfWork())
            {
                var product = Mapper.Map<ProductModel, Product>(productModel);
                unitOfWork.Products.Add(product);
                unitOfWork.Save();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditProduct(int productId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var product = Mapper.Map<Product, ProductModel>(unitOfWork.Products.GetById(productId));
                product.Categories = GetCategorySelectList(unitOfWork, product.CategoryId);

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

            using (var unitOfWork = new UnitOfWork())
            {
                var product = Mapper.Map<ProductModel, Product>(productModel);
                unitOfWork.Products.Update(product);
                unitOfWork.Save();
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
            using (var unitOfWork = new UnitOfWork())
            {
                var products = unitOfWork.Products.GetAll().ToList();

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
            using (var unitOfWork = new UnitOfWork())
            {
                var categories = Mapper.Map<List<Category>, List<CategoryModel>>(unitOfWork.Categories.GetAll().ToList());
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
                using (var unitOfWork = new UnitOfWork())
                {
                    foreach (var category in categories)
                    {
                        AddOrUpdateCategory(category, unitOfWork);
                    }
                    unitOfWork.Save();
                    return true;
                }
            }
            catch (DbUpdateConcurrencyException e)
            {
                Logger.Default.Warn(e);
                return false;
            }
        }

        private void AddOrUpdateCategory(Category category, UnitOfWork unitOfWork)
        {
            if (category.Id == 0)
            {
                AddNewCategory(category, unitOfWork);
            }
            else
            {
                UpdateExistingCategory(category, unitOfWork);
            }
        }

        private void UpdateExistingCategory(Category existingCategory, UnitOfWork unitOfWork)
        {

            if (!string.IsNullOrEmpty(existingCategory.Name))
            {
                unitOfWork.Categories.Update(existingCategory);
            }
            else
            {
                unitOfWork.Categories.Delete(existingCategory);
            }
        }      
        
        private void AddNewCategory(Category newCategory, UnitOfWork unitOfWork)
        {

            if (!string.IsNullOrEmpty(newCategory.Name))
            {
                unitOfWork.Categories.Add(newCategory);
            }
        }

        private bool DeleteProductInternal(int productId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    var product = new Product { Id = productId };
                    unitOfWork.Products.Delete(product);
                    unitOfWork.Save();
                    return true;
                }
                catch (DbUpdateConcurrencyException e)
                {
                    Logger.Default.Warn(e);
                    return !unitOfWork.Products.IsAnyProduct(product => product.Id == productId);
                }
            }
        }

        private SelectList GetCategorySelectList(UnitOfWork unitOfWork, int currentCategory = 0)
        {
            if(currentCategory != 0)
            {
                return new SelectList(unitOfWork.Categories.GetAll().ToList(), "Id", "Name", currentCategory);
            }
            return new SelectList(unitOfWork.Categories.GetAll().ToList(), "Id", "Name");
        }
    }
}