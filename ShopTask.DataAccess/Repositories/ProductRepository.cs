using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopTask.DataAccess.Interfaces;
using ShopTask.DataAccess.Entities;
using System.Data.Entity;

namespace ShopTask.DataAccess.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private ShopContext dbContext;

        public ProductRepository(ShopContext context)
        {
            dbContext = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return dbContext.Products.Include(product => product.Category);
        }

        public Product GetById(int id)
        {
            return dbContext.Products.Find(id);
        }

        public void Add(Product product)
        {
            dbContext.Products.Add(product);
        }

        public void Update(Product product)
        {
            dbContext.Entry(product).State = EntityState.Modified;
        }

        public void Delete(Product product)
        {
            dbContext.Products.Attach(product);
            dbContext.Products.Remove(product);
        }

        public bool IsAnyProduct(Func<Product, bool> predicate)
        {
            return dbContext.Products.Any(predicate);
        }
    }
}
