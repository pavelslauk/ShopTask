using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopTask.DataAccess.Entities;
using ShopTask.DataAccess.Repositories;
using ShopTask.Models;
using AutoMapper;

namespace ShopTask.Controllers
{
    public class ShopController : Controller
    {
        IRepository<Category> _categoriesRepository;

        public ShopController(IRepository<Category> categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        protected override void OnActionExecuted(ActionExecutedContext actionContext)
        {
            if (actionContext.Result is ViewResult)
            {
                ViewBag.Categories = Mapper.Map<IEnumerable<Category>, CategoryModel[]>(_categoriesRepository.GetAll());
            }
        }
    }
}