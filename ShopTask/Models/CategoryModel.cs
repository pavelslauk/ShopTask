﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ShopTask.Resources;

namespace ShopTask.Models
{
    public class CategoryModel
    {
        public int? Id { get; set; }

        [Display(Name = "CategoryModelName", ResourceType = typeof(Res))]
        public string Name { get; set; }
    }
}