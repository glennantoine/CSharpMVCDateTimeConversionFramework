using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CustomModelBindingWithDateTime.Utilities;

namespace CustomModelBindingWithDateTime.Models.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class UiDateTimeRequiredIfNotAttributeValidation : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessage = "'{0}' or '{1}' is a required field.";
        private readonly object _typeId = new object();
        private readonly string _basePropertyPath;
        private readonly string _comparisonPropertyPath;
        private string _comparisonPropertyDisplayName;

        public UiDateTimeRequiredIfNotAttributeValidation(string basePropertyPath, string comparisonPropertyPath)
            : base(DefaultErrorMessage)  
        {
            _basePropertyPath = basePropertyPath;
            _comparisonPropertyPath = comparisonPropertyPath;
        }  
   
        //Override default FormatErrorMessage Method  
        public override string FormatErrorMessage(string name)  
        {
            return string.Format(ErrorMessageString, name, _comparisonPropertyDisplayName);  
        }  
   
        //Override IsValid  
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)  
        {

            var propValue = UiDateTimeUtilities.ChildObjectFromValidationContext(_basePropertyPath, validationContext);
            var comparisonValue = UiDateTimeUtilities.ChildObjectFromValidationContext(_comparisonPropertyPath, validationContext);

            var displayName = UiDateTimeUtilities.GetPropertyDisplayNameFromValidationContext(_basePropertyPath, validationContext);
            _comparisonPropertyDisplayName = UiDateTimeUtilities.GetPropertyDisplayNameFromValidationContext(_comparisonPropertyPath, validationContext);

            //Actual comparision  
            if ((comparisonValue == null && comparisonValue.ToString().Length == 0) && (propValue == null || propValue.ToString().Length == 0))
            {
                var message = FormatErrorMessage(displayName);
                return new ValidationResult(message);
            }

            //Default return - This means there were no validation error  
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(UiDateTimeUtilities.GetPropertyDisplayNameFromModelMetadata(_basePropertyPath, metadata)),
                ValidationType = "uidatetimerequiredifnotattribute" + _basePropertyPath.ToLower().Replace(".", "")
            };
            rule.ValidationParameters.Add("other", _comparisonPropertyPath);
            rule.ValidationParameters.Add("basepropertyname", metadata.PropertyName);

            yield return rule;
        }

        public override object TypeId
        {
            get
            {
                return _typeId;
            }
        }
    }
}