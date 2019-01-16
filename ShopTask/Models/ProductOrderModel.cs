using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopTask.Models
{
    public class ProductOrderModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }
    }
}