using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopTask.DataAccess.Entities
{
    public class Product
    {
        public int? Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}