using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopTask.Utils
{
    public class Locals
    {
        public static readonly HashSet<string> cultures = new HashSet<string> { "en", "ru" };

        public static readonly HashSet<string> languagesNames = new HashSet<string> { "English", "Русский" };
    }
}