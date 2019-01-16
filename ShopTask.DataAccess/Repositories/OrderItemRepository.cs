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
    class OrderItemsRepository : IRepository<OrderItem>
    {
        private ShopContext _dbContext;

        public OrderItemsRepository(ShopContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync(Expression<Func<OrderItem, object>> include)
        {
            return await _dbContext.OrderItems.Include(include).ToListAsync();
        }

        public async Task<OrderItem> GetByIdAsync(int id)
        {
            return await _dbContext.OrderItems.FindAsync(id);
        }

        public async Task<IEnumerable<OrderItem>> FindAsync(Expression<Func<OrderItem, bool>> where, Expression<Func<OrderItem, object>> include)
        {
            return await _dbContext.OrderItems.Include(include).Where(where).ToListAsync();
        }

        public void Add(OrderItem orderItem)
        {
            _dbContext.OrderItems.Add(orderItem);
        }

        public void Update(OrderItem orderItem)
        {
            _dbContext.Entry(orderItem).State = EntityState.Modified;
        }

        public void Delete(OrderItem orderItem)
        {
            _dbContext.OrderItems.Attach(orderItem);
            _dbContext.OrderItems.Remove(orderItem);
        }
    }
}
