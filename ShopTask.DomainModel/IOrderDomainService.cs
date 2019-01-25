using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopTask.DomainModel.Entities;

namespace ShopTask.DomainModel
{
    public interface IOrderDomainService
    {
        Task<bool> SubmitOrder(Order order);
    }
}
