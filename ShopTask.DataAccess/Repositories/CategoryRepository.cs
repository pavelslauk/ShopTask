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
    public class CategoryRepository : IRepository<Category>
    {
        private ShopContext dbContext;

        public CategoryRepository(ShopContext context)
        {
            dbContext = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return dbContext.Categories;
        }

        public Category GetById(int id)
        {
            return dbContext.Categories.Find(id);
        }

        public void Add(Category category)
        {
            dbContext.Categories.Add(category);
        }

        public void Update(Category category)
        {
            dbContext.Entry(category).State = EntityState.Modified;
        }

        public void Delete(Category category)
        {
            dbContext.Categories.Attach(category);
            dbContext.Categories.Remove(category);
        }
    }
}
