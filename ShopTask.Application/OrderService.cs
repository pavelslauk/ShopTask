using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopTask.Application.Models;
using ShopTask.DomainModel.Entities;
using ShopTask.DomainModel.Repositories;
using ShopTask.DomainModel.Services;
using AutoMapper;

namespace ShopTask.Application
{
    public class OrderService : IOrderService
    {
        private IOrderDomainService _orderDomainService;
        private IUnitOfWork _unitOfWork;
        private IRepository<Product> _productsRepository;

        public OrderService(IOrderDomainService orderDomainService, IUnitOfWork unitOfWork, IRepository<Product> productsRepository)
        {
            _orderDomainService = orderDomainService;
            _unitOfWork = unitOfWork;
            _productsRepository = productsRepository;
        }

        public async Task<bool> SubmitOrderAsync(OrderDetailsModel orderDetails, CartItemModel[] orderItems)
        {
            var order = Mapper.Map<OrderDetailsModel, Order>(orderDetails);
            order.OrderItems = Mapper.Map<CartItemModel[], OrderItem[]>(orderItems);
            if (order.OrderItems?.Any() != true)
            {
                return false;
            }
            var productIds = order.OrderItems.Select(i => i.ProductId);
            var products = await _productsRepository.FindAsync(where: product => productIds.Contains(product.Id));
            foreach (var item in order.OrderItems)
            {
                item.Price = products.Single(p => p.Id == item.ProductId).Price;
            }
            var result = _orderDomainService.SubmitOrder(order, products);
            await _unitOfWork.CommitAsync();

            return result;
        }
    }
}
