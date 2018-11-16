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
    public class CategoriesRepository
    {
        private ShopContext _dbContext;

        public CategoriesRepository(ShopContext context)
        {
            _dbContext = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return _dbContext.Categories;
        }

        public Category GetById(int id)
        {
            return _dbContext.Categories.Find(id);
        }

        public IEnumerable<Category> Find(Expression<Func<Category, bool>> predicate)
        {
            return _dbContext.Categories.Where(predicate);
        }

        public void Add(Category category)
        {
            _dbContext.Categories.Add(category);
        }

        public void Update(Category category)
        {
            _dbContext.Entry(category).State = EntityState.Modified;
        }

        public void Delete(Category category)
        {
            _dbContext.Categories.Attach(category);
            _dbContext.Categories.Remove(category);
        }
    }
}
