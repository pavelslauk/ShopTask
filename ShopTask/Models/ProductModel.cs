using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace ShopTask.Models
{
    public class Product
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string title { get; set; }
        [Display(Name = "Price")]
        [Remote("CheckPrice", "Home", ErrorMessage = "Incorrect value")]
        public decimal price { get; set; }
        public string description { get; set; }
    }
}