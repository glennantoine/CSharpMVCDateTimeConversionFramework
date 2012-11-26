using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomModelBindingWithDateTime.Models;

namespace CustomModelBindingWithDateTime.Helpers 
{
    public static class ExtensionMethods 
    {
        public static MvcHtmlString EditorForProperty(this HtmlHelper html, UiDateTimeModel model) 
        {
            // creates an error: The type arguments for method 'EditorFor' cannot be inferred from the usage. Try specifying the type arguments explicitly.
            //return System.Web.Mvc.Html.EditorExtensions.Editor(model, "UiDateTimeModel");
            return null;
        }
    }
}

