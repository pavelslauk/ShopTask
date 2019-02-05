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
using ShopTask.Controllers;

namespace ShopTask.Areas.Inventory.Controllers
{
    public class ProductsController : BaseController
    {
        private IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork, IRepository<Category> categoriesRepository, IRepository<Product> productsRepository) : base(categoriesRepository, productsRepository)
        {
            _unitOfWork = unitOfWork;
            _productsRepository = productsRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetCategoriesAsync()
        {
            var categories = (await _categoriesRepository.GetAllAsync()).ToArray();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> SaveNewProduct(ProductModel newProduct)
        {
            var product = Mapper.Map<ProductModel, Product>(newProduct);
            _productsRepository.Add(product);
            await _unitOfWork.CommitAsync();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> SaveChangedProduct(ProductModel changetProduct)
        {
            var product = Mapper.Map<ProductModel, Product>(changetProduct);
            _productsRepository.Update(product);
            await _unitOfWork.CommitAsync();
            return Json(true, JsonRequestBehavior.AllowGet);
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
    }
}