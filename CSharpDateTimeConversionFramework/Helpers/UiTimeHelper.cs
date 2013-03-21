using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using CSharpDateTimeConversionFramework.Utilities;

namespace CSharpDateTimeConversionFramework.Helpers
{
    public static class UiTimeHelper
    {
        public static MvcHtmlString UiTimeBox(this HtmlHelper htmlHelper, string name, string value)
        {
            var builder = new TagBuilder("input");
            builder.Attributes["type"] = "text";
            builder.Attributes["name"] = name;
            builder.Attributes["value"] = value;
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString UiTimeBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return UiTimeBoxFor(htmlHelper, expression, new {});
        }

        public static MvcHtmlString UiTimeBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
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
