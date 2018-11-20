﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace ShopTask.Utils
{
    public class CastleWindsorInitializer
    {
        public static void Initialize()
        {
            var container = new WindsorContainer();
            container.Install(new ApplicationInstaller(), FromAssembly.Named("ShopTask.DataAccess"));
            ControllerBuilder.Current.SetControllerFactory(new CastleControllerFactory(container));
        }
    }
}