using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using ShopTask.Resources.Products;

namespace ShopTask.Application.Models
{
    public class ProductModel
    {
        public int? Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Display]
        [RegularExpression(@"((^[1-9][0-9]*(\.[0-9]*)?)|(^0\.(([0-9]*)?[1-9]([0-9]*)?)))$")]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public CategoryModel Category { get; set; }
    }
}