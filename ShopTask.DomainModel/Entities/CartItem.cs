using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTask.DomainModel.Entities
{
    public class CartItem
    {
        public string Title { get; set; }

        public decimal Price { get; set; }

        public int ProductId { get; set; }

        public int Count { get; set; }
    }
}
