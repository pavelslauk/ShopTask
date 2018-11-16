﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopTask.Core.Utils;

namespace ShopTask.DataAccess.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private ShopContext _dbContext = new ShopContext();
        private CategoriesRepository _categoriesRepository;
        private ProductsRepository _productsRepository;
        private bool _disposed = false;

        public UnitOfWork()
        {
            _dbContext.Database.Log = transaction => Logger.Default.Info(transaction);
        }

        public CategoriesRepository Categories => _categoriesRepository ?? (_categoriesRepository = new CategoriesRepository(_dbContext));

        public ProductsRepository Products => _productsRepository ?? (_productsRepository = new ProductsRepository(_dbContext));

        public void Save()
        {           
            _dbContext.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
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