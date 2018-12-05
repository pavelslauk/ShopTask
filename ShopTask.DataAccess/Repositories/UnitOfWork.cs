using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopTask.Core.Utils;

namespace ShopTask.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ShopContext _dbContext;

        public UnitOfWork(ShopContext context)
        {
            _dbContext = context;
            _dbContext.Database.Log = transaction => Logger.Default.Info(transaction);
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
