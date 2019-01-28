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
            var shippingInfo = Mapper.Map<OrderDetailsModel, ShippingInfo>(orderDetails);
            var cartItems = Mapper.Map<CartItemModel[], CartItem[]>(orderItems);
            if (cartItems?.Any() != true)
            {
                return false;
            }
            var productIds = cartItems.Select(i => i.ProductId);
            var products = await _productsRepository.FindAsync(where: product => productIds.Contains(product.Id));
            foreach (var item in cartItems)
            {
                item.Price = products.Single(p => p.Id == item.ProductId).Price;
                item.Title = products.Single(p => p.Id == item.ProductId).Title;
            }
            var result = _orderDomainService.SubmitOrder(shippingInfo, cartItems);
            await _unitOfWork.CommitAsync();

            return result;
        }
    }
}
