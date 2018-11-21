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
        IRepository<Category> _categories;
        IRepository<Product> _products;

        public HomeController(IUnitOfWork unitOfWork, IRepository<Category> categories, IRepository<Product> products)
        {
            _unitOfWork = unitOfWork;
            _categories = categories;
            _products = products;
        }

        [HttpGet]
        public ActionResult Index(int? filterCategoryId)
        {
            return View(filterCategoryId);
        }

        [HttpGet]
        public ActionResult CreateProduct()
        {
            var productModel = new ProductModel { Categories = GetCategorySelectList(_categories) };

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
            _products.Add(product);
            _unitOfWork.Commit();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditProduct(int productId)
        {
            var product = Mapper.Map<Product, ProductModel>(_products.GetById(productId));
            product.Categories = GetCategorySelectList(_categories, product.CategoryId);

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
            _products.Update(product);
            _unitOfWork.Commit();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult DeleteProduct(int productId)
        {
            var isDeleted = DeleteProductInternal(productId);

            return Json(isDeleted, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ProductsPartial(int? filterCategoryId)
        {
            var products = new List<Product>();
            if (filterCategoryId == null)
            {
                products = _products.GetAll(include: product => product.Category).ToList();
            }
            else
            {
                products = _products.Find(where: product => product.CategoryId == filterCategoryId, include: product => product.Category).ToList();
            }

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
            var categories = Mapper.Map<IEnumerable<Category>, List<CategoryModel>>(_categories.GetAll());
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

        protected override void OnActionExecuted(ActionExecutedContext actionContext)
        {
            if (actionContext.Result is ViewResult)
            {
                ViewBag.Categories = Mapper.Map<IEnumerable<Category>, CategoryModel[]>(_categories.GetAll());
            }
        }

        private bool UpdateCategoriesInternal(Category[] categories)
        {
            try
            {
                foreach (var category in categories)
                {
                    AddOrUpdateCategory(category, _categories);
                }
                _unitOfWork.Commit();
                return true;
            }
            catch (DbUpdateConcurrencyException e)
            {
                Logger.Default.Warn(e);
                return false;
            }
        }

        private void AddOrUpdateCategory(Category category, IRepository<Category> categories)
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

        private void UpdateExistingCategory(Category existingCategory, IRepository<Category> categories)
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

        private void AddNewCategory(Category newCategory, IRepository<Category> categories)
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
                _products.Delete(product);
                _unitOfWork.Commit();
                return true;
            }
            catch (DbUpdateConcurrencyException e)
            {
                Logger.Default.Warn(e);
                return !_products.Find(where: product => product.Id == productId, include: product => product.Category).Any();
            }
        }

        private SelectList GetCategorySelectList(IRepository<Category> categories, int currentCategory = 0)
        {
            if (currentCategory != 0)
            {
                return new SelectList(categories.GetAll().ToList(), "Id", "Name", currentCategory);
            }
            return new SelectList(categories.GetAll().ToList(), "Id", "Name");
        }

    }
}