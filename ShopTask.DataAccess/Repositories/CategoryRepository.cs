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
        private ShopContext _dbContext;

        public CategoriesRepository(ShopContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(Expression<Func<Category, object>> include = null)
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> FindAsync(Expression<Func<Category, bool>> where, Expression<Func<Category, object>> include = null)
        {
            return await _dbContext.Categories.Where(where).ToListAsync();
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
