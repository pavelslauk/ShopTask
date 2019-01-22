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
using Newtonsoft.Json.Linq;


namespace ShopTask.Controllers
{
    public class OrderController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<Product> _productsRepository;
        private IRepository<Order> _ordersRepository;

        public OrderController(IUnitOfWork unitOfWork, IRepository<Category> categoriesRepository,
            IRepository<Product> productsRepository, IRepository<Order> ordersRepository) : base(categoriesRepository)
        {
            _unitOfWork = unitOfWork;
            _productsRepository = productsRepository;
            _ordersRepository = ordersRepository;
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
        public void SaveCart(OrderItemModel[] cart)
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
        public async Task<JsonResult> SaveOrder(OrderModel orderModel)
        {
            var order = Mapper.Map<OrderModel, Order>(orderModel);
            order.OrderItems = Mapper.Map<OrderItemModel[], OrderItem[]>((OrderItemModel[])Session["Cart"]);
            if (!(order.OrderItems?.Any() ?? new bool?(false)).Value)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            var products = await _productsRepository.GetAllAsync(product => product.Category);
            foreach (var item in order.OrderItems)
            {
                item.Price = products.First(p => p.Id == item.ProductId).Price;
            }
            _ordersRepository.Add(order);
            await _unitOfWork.CommitAsync();

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}