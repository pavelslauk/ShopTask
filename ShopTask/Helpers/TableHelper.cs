using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ShopTask.Helpers
{
    public static class TableHelper
    {
        public static MvcHtmlString AddHiddenClass(this HtmlHelper html, bool tableIsExists)
        {         
            if (tableIsExists)
            {
                return new MvcHtmlString("hidden ");
            }
            return new MvcHtmlString("");
        }
    }
}