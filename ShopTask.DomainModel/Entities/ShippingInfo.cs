﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTask.DomainModel.Entities
{
    public class ShippingInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Comments { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public ShippingInfo()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
