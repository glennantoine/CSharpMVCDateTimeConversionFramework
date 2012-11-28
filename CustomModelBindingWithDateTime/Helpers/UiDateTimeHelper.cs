using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;

namespace CustomModelBindingWithDateTime.Helpers 
{
    public static class UiDateTimeHelper 
    {
        public static MvcHtmlString UiDateTimeBox(this HtmlHelper htmlHelper, string name, string value) 
        {
            var builder = new TagBuilder("input");
            builder.Attributes["type"] = "text";
            builder.Attributes["name"] = name;
            builder.Attributes["value"] = value;
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString UiDateTimeBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression) 
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var markup = UiDateTimeBox(htmlHelper, name, metadata.Model as string);

            return markup;
        }

        public static MvcHtmlString UiDateTimeBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes) 
        {
            RouteValueDictionary routeValues = new RouteValueDictionary(htmlAttributes);
            routeValues.Add("readonly", "read-only");

            MvcHtmlString html = default(MvcHtmlString);
            html = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression, routeValues);

            return html;
        }

        //public static MvcHtmlString TextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, bool editable) 
        //{
        //    MvcHtmlString html = default(MvcHtmlString);

        //    if (editable) 
        //    {
        //        html = Html.InputExtensions.TextBoxFor(htmlHelper, expression);
        //    } else 
        //    {
        //        html = Html.InputExtensions.TextBoxFor(htmlHelper, expression,
        //            new { @class = "readOnly", @readonly = "read-only" });
        //    }
        //    return html;
        //}

        //public static MvcHtmlString DropDownListFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList,
        //    object htmlAttributes, bool editable) 
        //{
        //    Func<TModel, TValue> deleg = expression.Compile();
        //    var result = deleg(htmlHelper.ViewData.Model);

        //    if (editable) 
        //    {
        //        return Html.SelectExtensions.DropDownListFor(htmlHelper, expression, selectList, htmlAttributes);
        //    } else 
        //    {
        //        string name = ExpressionHelper.GetExpressionText(expression);

        //        string selectedText = SelectInternal(htmlHelper, name, selectList);

        //        RouteValueDictionary routeValues = new RouteValueDictionary(htmlAttributes);
        //        routeValues.Add("class", "readOnly");
        //        routeValues.Add("readonly", "read-only");

        //        return Html.InputExtensions.TextBox(htmlHelper, name, selectedText, routeValues);
        //    }
        //}

        //private static string SelectInternal(HtmlHelper htmlHelper, string name, IEnumerable selectList) 
        //{
        //    ModelState state;
        //    string selectedText = string.Empty;
        //    string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

        //    object obj2 = null;
        //    if (htmlHelper.ViewData.ModelState.TryGetValue(fullHtmlFieldName, out state) && (state.Value != null)) 
        //    {
        //        obj2 = state.Value.ConvertTo(typeof(string), null);
        //    }
        //    if (obj2 == null) 
        //    {
        //        obj2 = htmlHelper.ViewData.Eval(fullHtmlFieldName);
        //    }

        //    if (obj2 != null) 
        //    {
        //        IEnumerable source = ((IEnumerable)new object[] { obj2 });

        //        HashSet<string> set = new HashSet<string>(source.Cast<object>().Select<object, string>(delegate(object value) 
        //        {
        //            return Convert.ToString(value, CultureInfo.CurrentCulture);
        //        }), StringComparer.OrdinalIgnoreCase);

        //        foreach (SelectListItem item in selectList) 
        //        {
        //            if ((item.Value != null) ? set.Contains(item.Value) : set.Contains(item.Text)) {
        //                selectedText = item.Text;
        //                break;
        //            }
        //        }
        //    }

        //    return selectedText;
        //}

        //public static MvcHtmlString TextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, 
        //                                                       object htmlAttributes, bool editable) 
        //{
        //    MvcHtmlString html = default(MvcHtmlString);

        //    if (editable) 
        //    {
        //        html = Html.InputExtensions.TextBoxFor(htmlHelper, expression, htmlAttributes);
        //    } else 
        //    {
        //        RouteValueDictionary routeValues = new RouteValueDictionary(htmlAttributes);
        //        routeValues.Add("class", "readOnly");
        //        routeValues.Add("readonly", "read-only");

        //        html = Html.InputExtensions.TextBoxFor(htmlHelper, expression, routeValues);
        //    }
        //    return html;
        //}
    }
}