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
using System.Threading.Tasks;

namespace ShopTask.Controllers
{
    public class ProductsController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<Product> _productsRepository;

        public ProductsController(IUnitOfWork unitOfWork, IRepository<Category> categoriesRepository, IRepository<Product> productsRepository) : base(categoriesRepository)
        {
            _unitOfWork = unitOfWork;
            _productsRepository = productsRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Index(int? filterCategoryId)
        {
            var products = (await _productsRepository.Find(where: product => !filterCategoryId.HasValue || product.CategoryId == filterCategoryId, include: product => product.Category)).ToList();
            ViewBag.FilterCategory = (await _categoriesRepository.Find(where: category => category.Id == filterCategoryId)).FirstOrDefault();

            return View(products);
        }

        [HttpGet]
        public async Task<ActionResult> CreateProduct()
        {
            var productModel = new ProductModel { Categories = await GetCategorySelectList() };

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
        public async Task<ActionResult> EditProduct(int productId)
        {
            var product = Mapper.Map<Product, ProductModel>(await _productsRepository.GetById(productId));
            product.Categories = await GetCategorySelectList(product.CategoryId);

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
        public async Task<JsonResult> DeleteProduct(int productId)
        {
            var isDeleted = await DeleteProductInternal(productId);

            return Json(isDeleted, JsonRequestBehavior.AllowGet);
        }

        private async Task<bool> DeleteProductInternal(int productId)
        {
            try
            {
                var product = new Product { Id = productId };
                _productsRepository.Delete(product);
                await _unitOfWork.Commit();
                return true;
            }
            catch (DbUpdateConcurrencyException e)
            {
                Logger.Default.Warn(e);
                return !(await _productsRepository.Find(where: product => product.Id == productId, include: product => product.Category)).Any();
            }
        }

        private async Task<SelectList> GetCategorySelectList(int? currentCategory = null)
        {
            return new SelectList(await _categoriesRepository.GetAll(), "Id", "Name", currentCategory);
        }
    }
}