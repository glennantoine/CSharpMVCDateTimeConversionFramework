using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CSharpMVCDateTimeConversionFramework.Utilities;

namespace CSharpMVCDateTimeConversionFramework.Models.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class UiDateTimeRequiredIfAttributeValueNotEqualsValidation : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessage = "'{0}' is a required field.";
        private readonly object _typeId = new object();
        private readonly string _basePropertyPath;
        private readonly string _comparisonPropertyPath;
        private readonly string _attributeValue;
        private string _comparisonPropertyDisplayName;

        public UiDateTimeRequiredIfAttributeValueNotEqualsValidation(string basePropertyPath, string comparisonPropertyPath, string attributeValue)
            : base(DefaultErrorMessage)
        {
            _basePropertyPath = basePropertyPath;
            _comparisonPropertyPath = comparisonPropertyPath;
            _attributeValue = attributeValue;
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


            if (comparisonValue.ToString() != _attributeValue && (propValue == null || propValue.ToString().Length == 0))
            {
                var message = FormatErrorMessage(displayName);
                return new ValidationResult(message);
            }

            //Default return - This means there were no validation error  
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            _comparisonPropertyDisplayName = UiDateTimeUtilities.GetPropertyDisplayNameFromModelMetadata(_comparisonPropertyPath, metadata);

            var rule = new ModelClientValidationRule
                           {
                               ErrorMessage = FormatErrorMessage(UiDateTimeUtilities.GetPropertyDisplayNameFromModelMetadata(_basePropertyPath, metadata)),
                               ValidationType = "uidatetimerequiredifattributevaluenotequals" + _basePropertyPath.ToLower().Replace(".", "")
                           };
            rule.ValidationParameters.Add("other", _comparisonPropertyPath);
            rule.ValidationParameters.Add("othervalue", _attributeValue);
            rule.ValidationParameters.Add("basepropertyname", metadata.PropertyName);

            yield return rule;
        }

    }
}