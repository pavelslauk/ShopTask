using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ShopTask.Models
{
    public class CategoryModel
    {
        public int? Id { get; set; }

        [Display(Name = "Category")]
        public string Name { get; set; }
    }
}