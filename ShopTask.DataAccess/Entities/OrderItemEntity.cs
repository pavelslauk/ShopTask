using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTask.DataAccess.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
