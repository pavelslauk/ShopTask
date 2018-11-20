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
        private bool _disposed = false;

        public UnitOfWork(ShopContext context)
        {
            _dbContext = context;
            _dbContext.Database.Log = transaction => Logger.Default.Info(transaction);
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
