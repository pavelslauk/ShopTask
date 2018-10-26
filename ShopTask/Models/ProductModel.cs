using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace ShopTask.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Price")]
        [Remote("CheckPrice", "Home", ErrorMessage = "Incorrect value")]
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}