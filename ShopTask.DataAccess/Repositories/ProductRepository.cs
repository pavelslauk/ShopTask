using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopTask.DataAccess.Entities;
using System.Data.Entity;

namespace ShopTask.DataAccess.Repositories
{
    public class ProductsRepository : IRepository<Product>
    {
        private ShopContext _dbContext;

        public ProductsRepository(ShopContext context)
        {
            _dbContext = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _dbContext.Products.Include(product => product.Category);
        }

        public Product GetById(int id)
        {
            return _dbContext.Products.Find(id);
        }

        public IEnumerable<Product> Find(Func<Product, bool> predicate)
        {

            return _dbContext.Products.Include(product => product.Category).Where(predicate);
        }

        public void Add(Product product)
        {
            _dbContext.Products.Add(product);
        }

        public void Update(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
        }

        public void Delete(Product product)
        {
            _dbContext.Products.Attach(product);
            _dbContext.Products.Remove(product);
        }

        public bool IsAnyProduct(Func<Product, bool> predicate)
        {
            return _dbContext.Products.Any(predicate);
        }
    }
}
