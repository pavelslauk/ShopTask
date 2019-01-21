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
    class OrdersRepository : IRepository<Order>
    {
        private ShopContext _dbContext;

        public OrdersRepository(ShopContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync(Expression<Func<Order, object>> include)
        {
            return await _dbContext.Orders.Include(include).ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _dbContext.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> FindAsync(Expression<Func<Order, bool>> where, Expression<Func<Order, object>> include)
        {
            return await _dbContext.Orders.Include(include).Where(where).ToListAsync();
        }

        public void Add(Order order)
        {
            _dbContext.Orders.Add(order);
            foreach (var item in order.OrderItems)
            {
                _dbContext.OrderItems.Add(item);
            }
        }

        public void Update(Order order)
        {
            _dbContext.Entry(order).State = EntityState.Modified;
        }

        public void Delete(Order order)
        {
            _dbContext.Orders.Attach(order);
            _dbContext.Orders.Remove(order);
        }
    }
}
