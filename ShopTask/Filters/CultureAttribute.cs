using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ShopTask.Utils;


namespace ShopTask.Filters
{
    public class CultureFilter : FilterAttribute, IActionFilter
    {        
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {    
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpCookie cultureCookie = filterContext.HttpContext.Request.Cookies[CookieKeys.CultureCookie];
            if (cultureCookie != null && LocalizationConfig.SupportedCultures.Contains(cultureCookie.Value))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(cultureCookie.Value);
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(cultureCookie.Value);
            }
        }
    }
}