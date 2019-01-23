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
            var order = Mapper.Map<OrderDetailsModel, Order>(orderDetails);
            order.OrderItems = Mapper.Map<CartItemModel[], OrderItem[]>((CartItemModel[])Session["Cart"]);
            if (order.OrderItems?.Any() != true)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            var productIds = order.OrderItems.Select(i => i.ProductId);
            var products = await _productsRepository.FindAsync(where: product => productIds.Contains(product.Id));
            foreach (var item in order.OrderItems)
            {
                item.Price = products.Single(p => p.Id == item.ProductId).Price;
            }
            _ordersRepository.Add(order);
            await _unitOfWork.CommitAsync();

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}