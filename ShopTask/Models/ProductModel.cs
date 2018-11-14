using ShopTask.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace ShopTask.Models
{
    public class ProductModel
    {
        public int? Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Price")]
        [RegularExpression(@"(^[1-9][0-9]*(\.[0-9]*)?)|(^0\.(([0-9]*)?[1-9]([0-9]*)?))$", ErrorMessage = "Incorrect value")]
        public decimal Price { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public CategoryModel Category { get; set; }

        public SelectList Categories { get; set; }
    }
}