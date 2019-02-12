﻿using System;
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
        private IRepository<ProductAttribute> _attributesRepository;

        public ProductsController(IUnitOfWork unitOfWork, IRepository<Category> categoriesRepository, 
            IRepository<Product> productsRepository, IRepository<ProductAttribute> attributesRepository) : base(categoriesRepository, productsRepository)
        {
            _unitOfWork = unitOfWork;
            _productsRepository = productsRepository;
            _attributesRepository = attributesRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetCategories()
        {
            var categories = (await _categoriesRepository.GetAllAsync()).ToArray();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetAttributes()
        {
            var attributes = (await _attributesRepository.GetAllAsync(attribute => attribute.AttributeValues)).ToArray();
            return Json(attributes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> SaveProduct(ProductModel productModel)
        {
            var product = Mapper.Map<ProductModel, Product>(productModel);
            if(product.Id == 0)
            {
                _productsRepository.Add(product);
            }
            else
            {
                _productsRepository.Update(product);
            } 
            await _unitOfWork.CommitAsync();
            return Json(true);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteProduct(int productId)
        {
            var isDeleted = await DeleteProductInternal(productId);

            return Json(isDeleted);
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