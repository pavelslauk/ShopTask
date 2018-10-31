using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ShopTask.Helpers
{
    public static class TableHelper
    {
        public static MvcHtmlString EmptyTablePlaceholder(this HtmlHelper html, string placeholder, bool tableIsExists)
        {
            var pTag = new TagBuilder("p");
            pTag.SetInnerText(placeholder);
            if (tableIsExists)
            {
                pTag.MergeAttribute("class", "js-empty-table-placeholder hidden");
            }
            else
            {
                pTag.MergeAttribute("class", "js-empty-table-placeholder");
            }

            return new MvcHtmlString(pTag.ToString());
        }
    }
}