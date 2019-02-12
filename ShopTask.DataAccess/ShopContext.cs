using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ShopTask.DomainModel.Entities;

namespace ShopTask.DataAccess
{
    public class ShopContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<AttributeValue> AttributeValues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AttributeValue>().HasMany(attribute => attribute.Products)
                .WithMany(product => product.AttributeValues)
                .Map(t => t.MapLeftKey("AttributeId")
                .MapRightKey("ProductId")
                .ToTable("AttributeProduct"));
        }
    }
}