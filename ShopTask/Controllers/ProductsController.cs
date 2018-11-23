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
    public class ProductsController : ShopController
    {
        IUnitOfWork _unitOfWork;
        IRepository<Category> _categoriesRepository;
        IRepository<Product> _productsRepository;

        public ProductsController(IUnitOfWork unitOfWork, IRepository<Category> categoriesRepository, IRepository<Product> productsRepository) : base(categoriesRepository)
        {
            _unitOfWork = unitOfWork;
            _categoriesRepository = categoriesRepository;
            _productsRepository = productsRepository;
        }

        [HttpGet]
        public ActionResult Index(int? filterCategoryId)
        {
            var products = _productsRepository.Find(where: product => !filterCategoryId.HasValue || product.CategoryId == filterCategoryId, include: product => product.Category).ToList();
            ViewBag.FilterCategory = _categoriesRepository.Find(where: category => category.Id == filterCategoryId).FirstOrDefault();

            return View(products);
        }

        [HttpGet]
        public ActionResult CreateProduct()
        {
            var productModel = new ProductModel { Categories = GetCategorySelectList(null) };

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
            _productsRepository.Add(product);
            _unitOfWork.Commit();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditProduct(int productId)
        {
            var product = Mapper.Map<Product, ProductModel>(_productsRepository.GetById(productId));
            product.Categories = GetCategorySelectList(product.CategoryId);

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
            _productsRepository.Update(product);
            _unitOfWork.Commit();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult DeleteProduct(int productId)
        {
            var isDeleted = DeleteProductInternal(productId);

            return Json(isDeleted, JsonRequestBehavior.AllowGet);
        }

        private bool DeleteProductInternal(int productId)
        {
            try
            {
                var product = new Product { Id = productId };
                _productsRepository.Delete(product);
                _unitOfWork.Commit();
                return true;
            }
            catch (DbUpdateConcurrencyException e)
            {
                Logger.Default.Warn(e);
                return !_productsRepository.Find(where: product => product.Id == productId, include: product => product.Category).Any();
            }
        }

        private SelectList GetCategorySelectList(int? currentCategory)
        {
            return new SelectList(_categoriesRepository.GetAll().ToList(), "Id", "Name", currentCategory);
        }
    }
}