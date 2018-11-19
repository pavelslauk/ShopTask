using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.Windsor;

namespace ShopTask.Utils
{
    public class CastleWindsorInitializer
    {
        public static void Initialize()
        {
            var container = new WindsorContainer();
            container.Install(new ApplicationCastleInstaller());
            ControllerBuilder.Current.SetControllerFactory(new CastleControllerFactory(container));
        }
    }
}