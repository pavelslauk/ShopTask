using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTask.DataAccess.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        CategoriesRepository Categories { get; }
        ProductsRepository Products { get; }
        void Save();
    }
}
