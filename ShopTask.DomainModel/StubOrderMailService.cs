using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopTask.Core.Utils;
using ShopTask.DomainModel.Entities;

namespace ShopTask.DomainModel
{
    class StubOrderMailService : IOrderMailService
    {
        public void SendMessage(Order order, IEnumerable<CartItem> cartItems)
        {
            var message = new StringBuilder(order.Name + " " + order.Surname + " заказал:");
            message.AppendLine();
            foreach (var item in cartItems)
            {
                message.AppendLine(item.Count + " " + item.Title);
            }

            Logger.Default.Debug(message.ToString());
        }
    }
}
