using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using CustomModelBindingWithDateTime.Utilities;

namespace CustomModelBindingWithDateTime.Helpers 
{
    public static class UiDateHelper
    {
        public static MvcHtmlString UiDateBox(this HtmlHelper htmlHelper, string name, string value) 
        {
            var builder = new TagBuilder("input");
            builder.Attributes["type"] = "text";
            builder.Attributes["name"] = name;
            builder.Attributes["value"] = value;
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString UiDateBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return UiDateBoxFor(htmlHelper, expression, new {});
        }

        public static MvcHtmlString UiDateBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            var propertyPath = ExpressionHelper.GetExpressionText(expression);
            var attributeKeyPropertyPath = propertyPath.ToLower().Replace(".", "");
            var meta = htmlHelper.ViewData.ModelMetadata;

            var attributes = UiDateTimeUtilities.AddViewDataHtmlAttributes(htmlHelper, propertyPath, new RouteValueDictionary(htmlAttributes));

            var validationAttributes = UiDateTimeUtilities.ChildValidationAttributes(htmlHelper, meta.PropertyName, propertyPath, meta);

            foreach (var attr in validationAttributes)
            {
                attributes.Add(attr.Key.Replace(attributeKeyPropertyPath, ""), attr.Value);
            }

            MvcHtmlString html = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression, attributes);
            return html;
        }
    }
}
