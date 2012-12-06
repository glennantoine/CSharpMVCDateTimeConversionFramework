using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using CustomModelBindingWithDateTime.Enumerations;
using CustomModelBindingWithDateTime.Resources;

namespace CustomModelBindingWithDateTime.Helpers 
{
    public static class UiDateHelper
    {
        private static readonly Dictionary<UiDateClientValidationModes, Dictionary<string, string>> _uiDateValidationModes = new Dictionary<UiDateClientValidationModes, Dictionary<string, string>>();
        private static readonly Dictionary<UiTimeClientValidationModes, Dictionary<string, string>> _uiTimeValidationModes = new Dictionary<UiTimeClientValidationModes, Dictionary<string, string>>();

        static UiDateHelper()
        {
            //Define Ui Client Validation for Dates
            UiDateValidationModes();

            //Define Ui Client Validation for Time
            UiTimeValidationModes();
        }

        public static MvcHtmlString UiDateTimeBox(this HtmlHelper htmlHelper, string name, string value) 
        {
            var builder = new TagBuilder("input");
            builder.Attributes["type"] = "text";
            builder.Attributes["name"] = name;
            builder.Attributes["value"] = value;
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString UiDateBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression) 
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var markup = UiDateTimeBox(htmlHelper, name, metadata.Model as string);

            return markup;
        }

        public static MvcHtmlString UiDateBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes) 
        {
            var attributes = new RouteValueDictionary(htmlAttributes);
            MvcHtmlString html = default(MvcHtmlString);
            html = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression, attributes);

            return html;
        }

        public static MvcHtmlString UiDateBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes, UiDateClientValidationModes[] validators)
        {
            var attributes = new RouteValueDictionary(htmlAttributes);

            if (validators != null)
            {
                foreach (var validator in validators)
                {
                    var dataAnnotations = _uiDateValidationModes[validator];
                    foreach (var dataAnnotation in dataAnnotations)
                    {
                        attributes.Add(dataAnnotation.Key, dataAnnotation.Value); 
                    }
                }
            }

            MvcHtmlString html = default(MvcHtmlString);
            html = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression, attributes);

            return html;
        }

        public static void UiTimeValidationModes() 
        {

            _uiTimeValidationModes.Add(UiTimeClientValidationModes.TimeRequired, new Dictionary<string, string>{
                                                                                                                {"data-val-required", ValidationResource.TimeRequired},
                                                                                                            });
            _uiTimeValidationModes.Add(UiTimeClientValidationModes.TimeValidation, new Dictionary<string, string>{
                                                                                                                {"data-val-time", ValidationResource.DateFormat},
                                                                                                            });
            _uiTimeValidationModes.Add(UiTimeClientValidationModes.TimeGreaterThanAttributeValidation, new Dictionary<string, string>{
                                                                                                                {"data-val-timenotinpast", ValidationResource.TimeNotFuture},
                                                                                                            });
            _uiTimeValidationModes.Add(UiTimeClientValidationModes.TimeGreaterThanEqualAttributeValidation, new Dictionary<string, string>{
                                                                                                                {"data-val-timenotinfuture", ValidationResource.TimeMustBeGreaterThanEqualTo},
                                                                                                            });
        }

        public static void UiDateValidationModes() 
        {
            _uiDateValidationModes.Add(UiDateClientValidationModes.DateRequired, new Dictionary<string, string>{
                                                                                                                {"data-val-required", ValidationResource.DateRequired},
                                                                                                            });
            _uiDateValidationModes.Add(UiDateClientValidationModes.DateValidation, new Dictionary<string, string>{
                                                                                                                {"data-val-date", ValidationResource.DateFormat},
                                                                                                            });
            _uiDateValidationModes.Add(UiDateClientValidationModes.DateNotInPastValidation, new Dictionary<string, string>{
                                                                                                                {"data-val-datenotinpast", ValidationResource.DateNotInPast},
                                                                                                            });
            _uiDateValidationModes.Add(UiDateClientValidationModes.DateNotInFutureValidation, new Dictionary<string, string>{
                                                                                                                {"data-val-datenotinfuture", ValidationResource.DateNotInFuture},
                                                                                                            });
            _uiDateValidationModes.Add(UiDateClientValidationModes.DateGreaterThanAttributeValidation, new Dictionary<string, string>{
                                                                                                                {"data-val-notgreaterthan", ValidationResource.DateMustBeGreaterThanEqualTo},
                                                                                                            });
        }

    }
}
