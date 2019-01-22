using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopTask.Models
{
    public class OrderDetailsModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Comments { get; set; }
    }
}