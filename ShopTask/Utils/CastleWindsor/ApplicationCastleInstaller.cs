using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ShopTask.DataAccess.Entities;
using ShopTask.DataAccess.Repositories;

namespace ShopTask.Utils
{
    public class ApplicationCastleInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork>().LifestylePerWebRequest());
            container.Register(Component.For<IRepository<Category>>().ImplementedBy<CategoriesRepository>().LifestylePerWebRequest());
            container.Register(Component.For<IRepository<Product>>().ImplementedBy<ProductsRepository>().LifestylePerWebRequest());
            var controllers = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.BaseType == typeof(Controller));
            foreach (var controller in controllers)
            {
                container.Register(Component.For(controller).LifestyleTransient());
            }
        }
    }
}