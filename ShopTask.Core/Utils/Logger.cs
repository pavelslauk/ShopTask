using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using log4net.Config;

namespace ShopTask.Core.Utils
{
    public static class Logger
    {
        public static ILog Default { get; } = LogManager.GetLogger("Default");
    }
}