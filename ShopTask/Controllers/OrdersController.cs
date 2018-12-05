using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopTask.DataAccess.Entities;
using ShopTask.DataAccess.Repositories;
using System.Threading.Tasks;
using AutoMapper;
using ShopTask.Models;

namespace ShopTask.Controllers
{
    public class OrdersController : BaseController
    {
        private IRepository<Product> _productsRepository;

        public OrdersController(IUnitOfWork unitOfWork, IRepository<Category> categoriesRepository, IRepository<Product> productsRepository) : base(categoriesRepository)
        {
            _productsRepository = productsRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetProductsAsync()
        {
            var products = Mapper.Map<Product[], JSONProductModel[]>((await _productsRepository
                .GetAllAsync(include: product => product.Category)).ToArray());
            return Json(products, JsonRequestBehavior.AllowGet);
        }
    }
}