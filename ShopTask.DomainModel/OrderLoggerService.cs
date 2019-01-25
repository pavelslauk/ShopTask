using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopTask.Core.Utils;
using ShopTask.DomainModel.Entities;

namespace ShopTask.DomainModel
{
    class OrderLoggerService : IOrderMailService
    {
        public void SendMessage(Order order, IEnumerable<Product> products)
        {
            var message = new StringBuilder(order.Name + " " + order.Surname + " заказал:");
            message.AppendLine();
            foreach (var item in order.OrderItems)
            {
                message.AppendLine(item.Count + " " + products.Single(p => p.Id == item.ProductId).Title);
            }

            Logger.Default.Debug(message.ToString());
        }
    }
}
