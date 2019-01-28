using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopTask.DomainModel.Entities;

namespace ShopTask.DomainModel.Services
{
    public interface IOrderDomainService
    {
        bool SubmitOrder(ShippingInfo shippingInfo, IEnumerable<CartItem> cartItems);
    }
}
