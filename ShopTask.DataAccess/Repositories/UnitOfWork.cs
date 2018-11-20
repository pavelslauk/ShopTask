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

        public ShopContext Context => _dbContext ?? (_dbContext = new ShopContext());

        public UnitOfWork()
        {
            Context.Database.Log = transaction => Logger.Default.Info(transaction);
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
