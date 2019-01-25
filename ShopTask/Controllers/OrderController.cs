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
        private IUnitOfWork _unitOfWork;
        private IRepository<Product> _productsRepository;
        private IRepository<Order> _ordersRepository;
        private IOrderService _orderService;

        public OrderController(IUnitOfWork unitOfWork, IRepository<Category> categoriesRepository,
            IRepository<Product> productsRepository, IRepository<Order> ordersRepository, IOrderService orderService) : base(categoriesRepository)
        {
            _unitOfWork = unitOfWork;
            _productsRepository = productsRepository;
            _ordersRepository = ordersRepository;
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

        [HttpGet]
        public async Task<JsonResult> GetProductsAsync()
        {
            var products = Mapper.Map<Product[], ProductOrderModel[]>((await _productsRepository
                .GetAllAsync(include: product => product.Category)).ToArray());
            return Json(products, JsonRequestBehavior.AllowGet);
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