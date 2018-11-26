using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopTask.DataAccess.Entities;
using System.Data.Entity;
using System.Linq.Expressions;

namespace ShopTask.DataAccess.Repositories
{
    public class ProductsRepository : IRepository<Product>
    {
        private ShopContext _dbContext;

        public ProductsRepository(ShopContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, object>> include)
        {
            return await _dbContext.Products.Include(include).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> where, Expression<Func<Product, object>> include)
        {
            return await _dbContext.Products.Include(include).Where(where).ToListAsync();
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
    }
}
