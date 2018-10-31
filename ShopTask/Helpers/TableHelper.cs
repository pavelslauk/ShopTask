using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ShopTask.Helpers
{
    public static class TableHelper
    {
        public static MvcHtmlString EmptyTablePlaceholder(this HtmlHelper html, string placeholder, bool tableIsExists, object htmlAttributes = null)
        {
            var pTag = new TagBuilder("p");
            pTag.SetInnerText(placeholder);
            pTag.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));          
            if (tableIsExists)
            {
                pTag.MergeAttribute("class", pTag.Attributes["class"] + " hidden", true);
            }

            return new MvcHtmlString(pTag.ToString());
        }
    }
}