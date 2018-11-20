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
    public class CategoriesRepository : IRepository<Category>
    {
        private ShopContext _context;
        public ShopContext Context { set { _context = value; } }

        public IEnumerable<Category> GetAll(Expression<Func<Category, object>> include = null)
        {
            return _context.Categories;
        }

        public Category GetById(int id)
        {
            return _context.Categories.Find(id);
        }

        public IEnumerable<Category> Find(Expression<Func<Category, bool>> where, Expression<Func<Category, object>> include = null)
        {
            return _context.Categories.Where(where);
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Update(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
        }

        public void Delete(Category category)
        {
            _context.Categories.Attach(category);
            _context.Categories.Remove(category);
        }
    }
}
