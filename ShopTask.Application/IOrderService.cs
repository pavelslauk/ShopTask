using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopTask.Application.Models;


namespace ShopTask.Application
{
    public interface IOrderService
    {
        Task<bool> SubmitOrderAsync(OrderDetailsModel orderDetails, CartItemModel[] orderItems);
    }
}
