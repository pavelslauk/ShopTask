using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ShopTask.Utils
{
    public class LocalizationConfig
    {
        public static readonly HashSet<string> SupportedCultures = new HashSet<string>(ConfigurationManager.AppSettings.AllKeys
                             .Where(key => key.StartsWith("SupportedCulture"))
                             .Select(key => ConfigurationManager.AppSettings[key]));      
    }
}