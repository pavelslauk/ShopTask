﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace ShopTask.DataAccess.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, object>> include = null);
        T GetById(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> where, Expression<Func<T, object>> include = null);
        void Add(T item);
        void Update(T item);
        void Delete(T item);
    }
}
