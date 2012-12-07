using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using CustomModelBindingWithDateTime.Enumerations;
using CustomModelBindingWithDateTime.Resources;

namespace CustomModelBindingWithDateTime.Helpers 
{
    public class UiDateHelperValidationAttribute
    {
        public string PropertyName { get; set; }
        public ModelMetadata Metadata { get; set; }
        public IDictionary<string, object> Attributes { get; set; }
    }

    public static class UiDateHelper
    {
        private static readonly IList<UiDateHelperValidationAttribute> StoredValidationAttributes = new List<UiDateHelperValidationAttribute>();

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
            var attributes = new RouteValueDictionary(htmlAttributes);

            var propertyPath = ExpressionHelper.GetExpressionText(expression);
            var attributeKeyPropertyPath = propertyPath.ToLower().Replace(".", "");
            var meta = htmlHelper.ViewData.ModelMetadata;

            IDictionary<string, object> validationAttributes;
            if (StoredValidationAttributes.Any(q => q.PropertyName == meta.PropertyName && q.Metadata == meta))
            {
                validationAttributes = StoredValidationAttributes.First(q => q.PropertyName == meta.PropertyName && q.Metadata == meta).Attributes;
            }
            else
            {
                validationAttributes = htmlHelper.GetUnobtrusiveValidationAttributes(meta.PropertyName, meta);
                StoredValidationAttributes.Add(new UiDateHelperValidationAttribute{ PropertyName = meta.PropertyName, Metadata = meta, Attributes = validationAttributes});
            }
            //Add in the associated validation attributes
            var associatedValidationAttributes = validationAttributes.Where(attr => attr.Key.EndsWith(attributeKeyPropertyPath)).ToList();
            if(associatedValidationAttributes.Any())
            {
                var dataValAttribute = validationAttributes.FirstOrDefault(attr => attr.Key == "data-val");
                attributes.Add(dataValAttribute.Key, dataValAttribute.Value);
            }

            foreach (var attr in associatedValidationAttributes)
            {
                attributes.Add(attr.Key.Replace(attributeKeyPropertyPath, ""), attr.Value);
            }

            MvcHtmlString html = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression, attributes);
            return html;
        }
    }
}
