using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopTask.Models
{
    public class OrderItemModel
    {
        public int ProductId { get; set; }

        public int Count { get; set; }
    }
}