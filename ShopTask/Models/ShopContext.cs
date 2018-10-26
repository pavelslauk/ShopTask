using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ShopTask.Models
{
    public class ShopContext : DbContext
    {
        public DbSet<Product> products { get; set; }
    }
}