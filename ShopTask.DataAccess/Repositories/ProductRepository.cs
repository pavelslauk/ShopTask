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
        private ShopContext _context;
        public ShopContext Context { set { _context = value; } }

        public IEnumerable<Product> GetAll(Expression<Func<Product, object>> include)
        {
            return _context.Products.Include(include);
        }

        public Product GetById(int id)
        {
            return _context.Products.Find(id);
        }

        public IEnumerable<Product> Find(Expression<Func<Product, bool>> where, Expression<Func<Product, object>> include)
        {
            return _context.Products.Include(include).Where(where);
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public void Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }

        public void Delete(Product product)
        {
            _context.Products.Attach(product);
            _context.Products.Remove(product);
        }
    }
}
