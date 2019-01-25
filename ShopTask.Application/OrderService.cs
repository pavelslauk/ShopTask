using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopTask.Application.Models;
using ShopTask.DomainModel.Entities;
using ShopTask.DomainModel;
using AutoMapper;

namespace ShopTask.Application
{
    public class OrderService : IOrderService
    {
        private IOrderDomainService _orderDomainService;

        public OrderService(IOrderDomainService orderDomainService)
        {
            _orderDomainService = orderDomainService;
        }

        public async Task<bool> SubmitOrderAsync(OrderDetailsModel orderDetails, CartItemModel[] orderItems)
        {
            var order = Mapper.Map<OrderDetailsModel, Order>(orderDetails);
            order.OrderItems = Mapper.Map<CartItemModel[], OrderItem[]>(orderItems);
            if (order.OrderItems?.Any() != true)
            {
                return false;
            }
            var result = await _orderDomainService.SubmitOrder(order);
            return result;
        }
    }
}
