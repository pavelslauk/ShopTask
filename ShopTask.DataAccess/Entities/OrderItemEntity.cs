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

        public Product Product { get; set; }

        [RegularExpression(@"(^[1-9][0-9]*(((\.)|(\,))[0-9]*)?)|(^0((\.)|(\,))(([0-9]*)?[1-9]([0-9]*)?))$")]
        public decimal OrderPrice { get; set; }

        public int Count { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}
