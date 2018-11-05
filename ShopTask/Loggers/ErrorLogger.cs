using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using log4net.Config;

namespace ShopTask.Loggers
{
    public static class ErrorLogger
    {
        public static ILog Log { get; } = LogManager.GetLogger("ErrorLogger");

        public static void InitLogger()
        {
            XmlConfigurator.Configure();
        }
    }
}