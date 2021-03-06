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
using Nito.AsyncEx;
using ShopTask.Controllers;

namespace ShopTask.Areas.Inventory.Controllers
{
    public class CategoriesController : BaseController
    {
        private IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork, IRepository<Category> categoriesRepository, IRepository<Product> productsRepository) : base (categoriesRepository, productsRepository)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CategoriesPartial()
        {
            var categories = Mapper.Map<IEnumerable<Category>, List<CategoryModel>>(_categoriesRepository.GetAllAsync().Result);
            categories.Add(new CategoryModel());

            return PartialView("CategoriesPartial", categories);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateCategories(Category[] categories)
        {
            ModelState.Clear();
            var isUpdated = await UpdateCategoriesInternal(categories);
            if (!isUpdated)
            {
                ModelState.AddModelError("UpdateFailed", "There is some error");
            }
            return CategoriesPartial();
        }

        private async Task<bool> UpdateCategoriesInternal(Category[] categories)
        {
            try
            {
                foreach (var category in categories)
                {
                    AddOrUpdateCategory(category);
                }
                await _unitOfWork.CommitAsync();

                return true;
            }
            catch (DbUpdateConcurrencyException e)
            {
                Logger.Default.Warn(e);
                return false;
            }
        }

        private void AddOrUpdateCategory(Category category)
        {
            if (category.Id == 0)
            {
                if (!string.IsNullOrEmpty(category.Name))
                {
                    _categoriesRepository.Add(category);
                }
            }
            else if (!string.IsNullOrEmpty(category.Name))
            {
                _categoriesRepository.Update(category);
            }
            else
            {
                _categoriesRepository.Delete(category);
            }           
        }
    }
}