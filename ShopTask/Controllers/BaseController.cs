using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopTask.DomainModel.Entities;
using ShopTask.DomainModel.Repositories;
using ShopTask.Application.Models;
using AutoMapper;
using System.Threading.Tasks;
using Nito.AsyncEx;
using ShopTask.Utils;

namespace ShopTask.Controllers
{
    public class BaseController : Controller
    {
        protected IRepository<Category> _categoriesRepository;

        public BaseController(IRepository<Category> categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        [HttpPost]
        public void ChangeCulture(string culture)
        {
            Response.Cookies.Add(CreateCookie(culture));
        }      

        protected override void OnActionExecuted(ActionExecutedContext actionContext)
        {
            if (actionContext.Result is ViewResult)
            {
                ViewBag.Categories = Mapper.Map<IEnumerable<Category>, CategoryModel[]>(_categoriesRepository.GetAllAsync().Result);
            }
        }

        private HttpCookie CreateCookie(string culture)
        {
            var cookie = new HttpCookie(CookieKeys.CultureCookie);
            cookie.HttpOnly = false;
            cookie.Value = culture;
            cookie.Expires = DateTime.Now.AddYears(1);

            return cookie;
        }
    }
}