using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTask.DomainModel.Entities
{
    public class CartItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }
    }
}
