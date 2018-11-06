using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ShopTask.Models
{
    public class Category
    {
        public int? Id { get; set; }

        public string Name { get; set; }
    }
}