using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using CustomModelBindingWithDateTime.Enumerations;

namespace CustomModelBindingWithDateTime.Helpers 
{
    public static class UiDateTimeHelper
    {
        private static readonly Dictionary<UiDateClientValidationModes, Dictionary<string, string>> _uiDateValidationModes = new Dictionary<UiDateClientValidationModes, Dictionary<string, string>>();

        static UiDateTimeHelper()
        {
            //Setup Ui Client Validation for Dates
            _uiDateValidationModes.Add(UiDateClientValidationModes.DateRequired, new Dictionary<string, string>{
                                                                                                                {"data-val-required", "Date is required"},
                                                                                                            });
            _uiDateValidationModes.Add(UiDateClientValidationModes.DateValidation, new Dictionary<string, string>{
                                                                                                                {"data-val-date", "Date is improperly formated: MM/DD/YYYY."},
                                                                                                            });
            _uiDateValidationModes.Add(UiDateClientValidationModes.DateNotInPastValidation, new Dictionary<string, string>{
                                                                                                                {"data-val-datenotinpast", "Date is required"},
                                                                                                            });
            _uiDateValidationModes.Add(UiDateClientValidationModes.DateNotInFutureValidation, new Dictionary<string, string>{
                                                                                                                {"data-val-datenotinfuture", "Date is required"},
                                                                                                            });
            _uiDateValidationModes.Add(UiDateClientValidationModes.DateGreaterThanAttributeValidation, new Dictionary<string, string>{
                                                                                                                {"data-val-notgreaterthan", "Date Must be Greater Than"},
                                                                                                            });
            
        }

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
            var attributes = new RouteValueDictionary(htmlAttributes);
            MvcHtmlString html = default(MvcHtmlString);
            html = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression, attributes);

            return html;
        }

        public static MvcHtmlString UiDateTimeBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes, UiDateClientValidationModes[] validators)
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

        public static Dictionary<UiTimeClientValidationModes, string> UiTimeValidationModes() 
        {
            var timeValidation = new Dictionary<UiTimeClientValidationModes, string>();
            timeValidation.Add(UiTimeClientValidationModes.TimeRequired, "TimeRequired");
            timeValidation.Add(UiTimeClientValidationModes.TimeValidation, "TimeValidation");
            timeValidation.Add(UiTimeClientValidationModes.TimeGreaterThanAttributeValidation, "TimeGreaterThanAttributeValidation");
            timeValidation.Add(UiTimeClientValidationModes.TimeGreaterThanEqualAttributeValidation, "TimeGreaterThanEqualAttributeValidation");

            return timeValidation;
        }

    }
}


//[TimeValidation(ErrorMessageResourceName = "TimeFormatValid", ErrorMessageResourceType = typeof(Resources.Validation))]
//[TimeGreaterThanEqualAttributeValidation("ServiceDeptOpen")]
//[TimeGreaterThanAttributeOrNullValidation("StartTime", ErrorMessageResourceName = "DateMustBeBeforeDateValid", ErrorMessageResourceType = typeof(Resources.Validation))]

//[DateValidation(ErrorMessageResourceName = "DateInvalid", ErrorMessageResourceType = typeof(Resources.Validation))]
//[DateNotInPastValidation(ErrorMessageResourceName = "DateNotInPastValid", ErrorMessageResourceType = typeof(Resources.Validation))]
//[DateNotInFutureValidation(ErrorMessageResourceName = "DateMustBeInThePastValid", ErrorMessageResourceType = typeof(Resources.Validation))]
//[DateGreaterThanAttributeOrNullValidation("StartDate", false, ErrorMessageResourceName = "DateMustBeAfterDateValid", ErrorMessageResourceType = typeof(Resources.Validation))]

//[RequiredIfNotAttributeValidation("IsAllDay", ErrorMessageResourceName = "LeadRoutingRuleCompleteTimeRange", ErrorMessageResourceType = typeof(Resources.Validation))]
//[RequiredIfAttributeValidation("StartTime", ErrorMessageResourceName = "LeadRoutingRuleCompleteTimeRange", ErrorMessageResourceType = typeof(Resources.Validation))]