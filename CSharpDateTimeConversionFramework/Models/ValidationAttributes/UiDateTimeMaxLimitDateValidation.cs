using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using CSharpDateTimeConversionFramework.Utilities;

namespace CSharpDateTimeConversionFramework.Models.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class UiDateTimeMaxLimitDateValidation : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessage = "{0} must be less than {1} years in the future.";
        private readonly object _typeId = new object();
        private readonly string _basePropertyPath;
        private readonly int _maximumYearsInFuture;

        public UiDateTimeMaxLimitDateValidation(string basePropertyPath, int maximumYearsInFuture)
            : base(DefaultErrorMessage)
        {
            _basePropertyPath = basePropertyPath;
            _maximumYearsInFuture = maximumYearsInFuture;
        }

        //Override default FormatErrorMessage Method  
        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, _maximumYearsInFuture.ToString(CultureInfo.InvariantCulture));
        }

        //Override IsValid  
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propValue = UiDateTimeUtilities.ChildObjectFromValidationContext(_basePropertyPath, validationContext);
            var displayName = UiDateTimeUtilities.GetPropertyDisplayNameFromValidationContext(_basePropertyPath, validationContext);

            var futureDate = DateTime.UtcNow.AddYears(_maximumYearsInFuture);
            if (propValue != null && !String.IsNullOrWhiteSpace(propValue.ToString()))
            {
                var propDate = DateTime.Parse(propValue.ToString());
                if (propDate > futureDate)
                {
                    var message = FormatErrorMessage(displayName);
                    return new ValidationResult(message);
                }
            }

            //Default return - This means there were no validation error  
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
                           {
                               ErrorMessage = FormatErrorMessage(UiDateTimeUtilities.GetPropertyDisplayNameFromModelMetadata(_basePropertyPath, metadata)),
                               ValidationType = "uidatetimemaxlimitdate" + _basePropertyPath.ToLower().Replace(".", "")
                           };
            rule.ValidationParameters.Add("maxyears", _maximumYearsInFuture);
            yield return rule;
        }

        public override object TypeId
        {
            get { return _typeId; }
        }
    }
}