using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopTask.Models;
using System.Data.Entity.Infrastructure;
using ShopTask.Core.Utils;
using ShopTask.DataAccess.Entities;
using ShopTask.DataAccess.Repositories;
using AutoMapper;

namespace ShopTask.Controllers
{
    public class HomeController : Controller
    {

        IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateProduct()
        {
            var productModel = new ProductModel { Categories = GetCategorySelectList(_unitOfWork.Categories) };

            return View("ProductView", productModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(ProductModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return View("ProductView");
            }

            var product = Mapper.Map<ProductModel, Product>(productModel);
            _unitOfWork.Products.Add(product);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditProduct(int productId)
        {
            var product = Mapper.Map<Product, ProductModel>(_unitOfWork.Products.GetById(productId));
            product.Categories = GetCategorySelectList(_unitOfWork.Categories, product.CategoryId);

            return View("ProductView", product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(ProductModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return View("ProductView");
            }

            var product = Mapper.Map<ProductModel, Product>(productModel);
            _unitOfWork.Products.Update(product);
            _unitOfWork.Save();

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
            var products = _unitOfWork.Products.GetAll(include: product => product.Category).ToList();

            return PartialView(products);
        }

        [HttpGet]
        public ActionResult Categories()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CategoriesPartial()
        {
            var categories = Mapper.Map<IEnumerable<Category>, List<CategoryModel>>(_unitOfWork.Categories.GetAll());
            categories.Add(new CategoryModel());
            return PartialView("CategoriesPartial", categories);
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
                foreach (var category in categories)
                {
                    AddOrUpdateCategory(category, _unitOfWork.Categories);
                }
                _unitOfWork.Save();
                return true;
            }
            catch (DbUpdateConcurrencyException e)
            {
                Logger.Default.Warn(e);
                return false;
            }
        }

        private void AddOrUpdateCategory(Category category, CategoriesRepository categories)
        {
            if (category.Id == 0)
            {
                AddNewCategory(category, categories);
            }
            else
            {
                UpdateExistingCategory(category, categories);
            }
        }

        private void UpdateExistingCategory(Category existingCategory, CategoriesRepository categories)
        {

            if (!string.IsNullOrEmpty(existingCategory.Name))
            {
                categories.Update(existingCategory);
            }
            else
            {
                categories.Delete(existingCategory);
            }
        }

        private void AddNewCategory(Category newCategory, CategoriesRepository categories)
        {

            if (!string.IsNullOrEmpty(newCategory.Name))
            {
                categories.Add(newCategory);
            }
        }

        private bool DeleteProductInternal(int productId)
        {
            try
            {
                var product = new Product { Id = productId };
                _unitOfWork.Products.Delete(product);
                _unitOfWork.Save();
                return true;
            }
            catch (DbUpdateConcurrencyException e)
            {
                Logger.Default.Warn(e);
                return !_unitOfWork.Products.Find(where: product => product.Id == productId, include: product => product.Category).Any();
            }
        }

        private SelectList GetCategorySelectList(CategoriesRepository categories, int currentCategory = 0)
        {
            if (currentCategory != 0)
            {
                return new SelectList(categories.GetAll().ToList(), "Id", "Name", currentCategory);
            }
            return new SelectList(categories.GetAll().ToList(), "Id", "Name");
        }
    }
}