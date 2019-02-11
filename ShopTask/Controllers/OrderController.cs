using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopTask.DomainModel.Repositories;  //
using ShopTask.DomainModel.Entities;   //
using ShopTask.Application.Models;
using ShopTask.Application;
using System.Threading.Tasks;
using AutoMapper;


namespace ShopTask.Controllers
{
    public class OrderController : BaseController
    {
        private IOrderService _orderService;

        public OrderController(IRepository<Category> categoriesRepository,
            IRepository<Product> productsRepository, IOrderService orderService) : base(categoriesRepository, productsRepository)
        {
            _productsRepository = productsRepository;
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetCart()
        {
            return Json(Session["Cart"], JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void SaveCart(CartItemModel[] cart)
        {
            Session["Cart"] = cart;
        }

        [HttpPost]
        public async Task<JsonResult> SaveOrder(OrderDetailsModel orderDetails)
        {
            var orderItems = (CartItemModel[])Session["Cart"];
            var result = await _orderService.SubmitOrderAsync(orderDetails, orderItems);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}