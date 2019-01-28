using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopTask.Core.Utils;
using ShopTask.DomainModel.Entities;
using ShopTask.DomainModel.Repositories;
using ShopTask.DomainModel.Services;
using AutoMapper;


namespace ShopTask.DomainModel
{
    public class OrderDomainService : IOrderDomainService
    {

        private IUnitOfWork _unitOfWork;
        private IRepository<Product> _productsRepository;
        private IRepository<Order> _ordersRepository;
        private IOrderMailService _orderMailService;

        public OrderDomainService(IUnitOfWork unitOfWork, IRepository<Product> productsRepository, 
            IRepository<Order> ordersRepository, IOrderMailService orderMailService)
        {
            _unitOfWork = unitOfWork;
            _productsRepository = productsRepository;
            _ordersRepository = ordersRepository;
            _orderMailService = orderMailService;
        }

        public bool SubmitOrder(ShippingInfo shippingInfo, CartItem[] cartItems)
        {
            var order = Mapper.Map<ShippingInfo, Order>(shippingInfo);
            order.OrderItems = Mapper.Map<CartItem[], OrderItem[]>(cartItems);
            _ordersRepository.Add(order);
            _orderMailService.SendMessage(order, cartItems);

            return true;
        }
 
    }
}
