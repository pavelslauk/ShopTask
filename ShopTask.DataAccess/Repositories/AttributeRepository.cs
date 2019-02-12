using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopTask.DomainModel.Entities;
using ShopTask.DomainModel.Repositories;
using System.Data.Entity;
using System.Linq.Expressions;


namespace ShopTask.DataAccess.Repositories
{
    class AttributeRepository : IRepository<ProductAttribute>
    {
        private ShopContext _dbContext;

        public AttributeRepository(ShopContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<ProductAttribute>> GetAllAsync(Expression<Func<ProductAttribute, object>> include)
        {
            return await _dbContext.ProductAttributes.Include(include).ToListAsync();
        }

        public async Task<ProductAttribute> GetByIdAsync(int id)
        {
            return await _dbContext.ProductAttributes.FindAsync(id);
        }

        public async Task<IEnumerable<ProductAttribute>> FindAsync(Expression<Func<ProductAttribute, bool>> where, 
            Expression<Func<ProductAttribute, object>> include)
        {
            return await _dbContext.ProductAttributes.Include(include).Where(where).ToListAsync();
        }

        public void Add(ProductAttribute productAttribute)
        {
            _dbContext.ProductAttributes.Add(productAttribute);
        }

        public void Update(ProductAttribute productAttribute)
        {
            _dbContext.Entry(productAttribute).State = EntityState.Modified;
        }

        public void Delete(ProductAttribute productAttribute)
        {
            _dbContext.ProductAttributes.Attach(productAttribute);
            _dbContext.ProductAttributes.Remove(productAttribute);
        }
    }
}
