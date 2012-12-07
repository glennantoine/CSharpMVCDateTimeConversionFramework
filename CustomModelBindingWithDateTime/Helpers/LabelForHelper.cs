﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using CustomModelBindingWithDateTime.Models;

namespace CustomModelBindingWithDateTime.Helpers
{
    public static class LabelForHelper
    {

        public static MvcHtmlString MyLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            return MyLabelFor(html, expression, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString MyLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            string labelFor = htmlFieldName.Replace("[", "_").Replace("]", "_");
            if (String.IsNullOrEmpty(labelText))
            {
                return MvcHtmlString.Empty;
            }

            //If the display value is specified in the additional values, use that instead
            var propertyPath = ExpressionHelper.GetExpressionText(expression);
            var displayAttributes = html.ViewData.ModelMetadata.AdditionalValues.Where(q => q.Key == UiDateTimeDisplayAttributeProvider.UiDateTimeDisplayAttributeKey)
                .SelectMany(q => (List<UiDateTimeDisplayAttribute>)q.Value, (source, value) => new { source, value })
                .Select(q => q.value).ToList();
            if (displayAttributes.Any(s => s.PropertyPath == propertyPath))
            {
                labelText = displayAttributes.First(q => q.PropertyPath == propertyPath).DisplayName;
            }

            TagBuilder tag = new TagBuilder("label");
            tag.MergeAttributes(htmlAttributes);
            tag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(labelFor));

            //var propertyInfo = metadata.ContainerType.GetProperty(metadata.PropertyName);

            //var customAttributes = propertyInfo.GetCustomAttributes(true).Where(a => a is RequiredAttribute || a is AtLeastNRequiredValidation).ToList();
            //if(customAttributes.Any())
            //{
            //    tag.AddCssClass("req");
            //}

            tag.SetInnerText(labelText);

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }
    }
}
