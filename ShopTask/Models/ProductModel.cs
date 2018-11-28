using ShopTask.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using ShopTask.Resources;

namespace ShopTask.Models
{
    public class ProductModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Res), ErrorMessageResourceName = "EmptyProductTitle")]
        [Display(Name = "ProductModelTitle", ResourceType = typeof(Res))]
        public string Title { get; set; }

        [Display(Name = "ProductModelPrice", ResourceType = typeof(Res))]
        [RegularExpression(@"(^[1-9][0-9]*(\.[0-9]*)?)|(^0\.(([0-9]*)?[1-9]([0-9]*)?))$",
            ErrorMessageResourceType = typeof(Res),
            ErrorMessageResourceName = "IncorrectProductPrice")]
        public decimal Price { get; set; }

        [Display(Name = "ProductModelDescription", ResourceType = typeof(Res))]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public CategoryModel Category { get; set; }

        public SelectList Categories { get; set; }
    }
}