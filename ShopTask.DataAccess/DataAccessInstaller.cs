using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ShopTask.DataAccess.Entities;
using ShopTask.DataAccess.Repositories;

namespace ShopTask.DataAccess
{
    public class DataAccessInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ShopContext>().LifestylePerWebRequest());
            container.Register(Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork>().LifestylePerWebRequest());
            container.Register(Component.For<IRepository<Category>>().ImplementedBy<CategoriesRepository>().LifestylePerWebRequest());
            container.Register(Component.For<IRepository<Product>>().ImplementedBy<ProductsRepository>().LifestylePerWebRequest());
        }
    }
}