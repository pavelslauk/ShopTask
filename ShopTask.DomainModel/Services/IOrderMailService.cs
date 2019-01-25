using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopTask.DomainModel.Entities;

namespace ShopTask.DomainModel
{
    public interface IOrderMailService
    {
        void SendMessage(Order order, IEnumerable<Product> products);
    }
}
