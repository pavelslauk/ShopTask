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
    public class CategoriesController : Controller
    {
        IUnitOfWork _unitOfWork;
        IRepository<Category> _categoriesRepository;

        public CategoriesController(IUnitOfWork unitOfWork, IRepository<Category> categoriesRepository, IRepository<Product> productsRepository)
        {
            _unitOfWork = unitOfWork;
            _categoriesRepository = categoriesRepository;
        }

        [HttpGet]
        public ActionResult Categories()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CategoriesPartial()
        {
            var categories = Mapper.Map<IEnumerable<Category>, List<CategoryModel>>(_categoriesRepository.GetAll());
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
                ViewBag.Categories = Mapper.Map<IEnumerable<Category>, CategoryModel[]>(_categoriesRepository.GetAll());
            }
        }

        private bool UpdateCategoriesInternal(Category[] categories)
        {
            try
            {
                foreach (var category in categories)
                {
                    AddOrUpdateCategory(category, _categoriesRepository);
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

    }
}