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
    public class ProductsRepository
    {
        private ShopContext _dbContext;

        public ProductsRepository(ShopContext context)
        {
            _dbContext = context;
        }

        public IEnumerable<Product> GetAll(Expression<Func<Product, Category>> includePredicate)
        {
            return _dbContext.Products.Include(includePredicate);
        }

        public Product GetById(int id)
        {
            return _dbContext.Products.Find(id);
        }

        public IEnumerable<Product> Find(Expression<Func<Product, bool>> predicate, Expression<Func<Product, Category>> includePredicate)
        {
            return _dbContext.Products.Include(includePredicate).Where(predicate);
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
