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
using Nito.AsyncEx;

namespace ShopTask.Controllers
{
    public class OrderController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<Product> _productsRepository;
        private IRepository<Order> _ordersRepository;
        private IRepository<OrderItem> _orderItemsRepository;

        public OrderController(IUnitOfWork unitOfWork, IRepository<Category> categoriesRepository, IRepository<Product> productsRepository, 
            IRepository<Order> ordersRepository, IRepository<OrderItem> orderItemsRepository) : base(categoriesRepository)
        {
            _unitOfWork = unitOfWork;
            _productsRepository = productsRepository;
            _ordersRepository = ordersRepository;
            _orderItemsRepository = orderItemsRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public object GetCart()
        {
            return Session["Cart"];
        }

        [HttpPost]
        public void SaveCart(string cart)
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
        public void SaveOrder(string data)
        {
            Order order = JObject.Parse(data)["order"].ToObject<Order>();
            OrderItem[] orderItems = JObject.Parse(data)["cart"].ToObject<OrderItem[]>();
            _ordersRepository.Add(order);
            AsyncContext.Run(async () => await _unitOfWork.CommitAsync());
            SaveOrderItems(order.Id, orderItems);
        }

        private void SaveOrderItems(int orderId, OrderItem[] orderItems)
        {
            foreach(var item in orderItems)
            {
                item.OrderId = orderId;
                _orderItemsRepository.Add(item);
            }
            AsyncContext.Run(async () => await _unitOfWork.CommitAsync());
        }
    }
}