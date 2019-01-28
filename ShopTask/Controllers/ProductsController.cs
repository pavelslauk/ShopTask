using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopTask.Application.Models;
using System.Data.Entity.Infrastructure;
using ShopTask.Core.Utils;
using ShopTask.DomainModel.Entities;
using ShopTask.DomainModel.Repositories;
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
            var products = (await _productsRepository.FindAsync(where: product => !filterCategoryId.HasValue || product.CategoryId == filterCategoryId, include: product => product.Category))
                .ToList();
            ViewBag.FilterCategory = (await _categoriesRepository.FindAsync(where: category => category.Id == filterCategoryId))
                .FirstOrDefault();

            return View(products);
        }

        [HttpGet]
        public async Task<ActionResult> CreateProduct()
        {
            var productModel = new ProductModel { Categories = await GetCategorySelectListAsync() };

            return View("ProductView", productModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateProduct(ProductModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return View("ProductView");
            }
            var product = Mapper.Map<ProductModel, Product>(productModel);
            _productsRepository.Add(product);
            await _unitOfWork.CommitAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> EditProduct(int productId)
        {
            var product = Mapper.Map<Product, ProductModel>(await _productsRepository.GetByIdAsync(productId));
            product.Categories = await GetCategorySelectListAsync(product.CategoryId);

            return View("ProductView", product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProduct(ProductModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return View("ProductView");
            }
            
            var product = Mapper.Map<ProductModel, Product>(productModel);
            _productsRepository.Update(product);
            await _unitOfWork.CommitAsync();

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
                await _unitOfWork.CommitAsync();

                return true;
            }
            catch (DbUpdateConcurrencyException e)
            {
                Logger.Default.Warn(e);

                return !(await _productsRepository.FindAsync(where: product => product.Id == productId)).Any();
            }
        }

        private async Task<SelectList> GetCategorySelectListAsync(int? currentCategory = null)
        {
            return new SelectList(await _categoriesRepository.GetAllAsync(), "Id", "Name", currentCategory);
        }
    }
}