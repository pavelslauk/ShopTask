using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopTask.DataAccess.Entities;
using ShopTask.DataAccess.Repositories;
using ShopTask.Models;
using AutoMapper;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace ShopTask.Controllers
{
    public class BaseController : Controller
    {
        protected IRepository<Category> _categoriesRepository;

        public BaseController(IRepository<Category> categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        [HttpGet]
        public ActionResult ChangeCulture(string culture)
        {
            Response.Cookies.Add(GetCookie(culture));

            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }      

        protected override void OnActionExecuted(ActionExecutedContext actionContext)
        {
            if (actionContext.Result is ViewResult)
            {
                ViewBag.Categories = Mapper.Map<IEnumerable<Category>, CategoryModel[]>(AsyncContext.Run(async () => await _categoriesRepository.GetAllAsync()));
            }
        }

        private HttpCookie GetCookie(string culture)
        {
            var cookie = Request.Cookies["lang"];
            if (cookie != null)
            {
                cookie.Value = culture;
            }
            else
            {
                cookie = new HttpCookie("lang");
                cookie.HttpOnly = false;
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }

            return cookie;
        }
    }
}