using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CustomModelBindingWithDateTime.Utilities;

namespace CustomModelBindingWithDateTime.Models.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class UiDateTimeGreaterThanDateAttributeOrNullValidation : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessage = "'{0}' must be greater than '{1}'";
        private readonly object _typeId = new object();
        private readonly string _basePropertyPath;
        private readonly string _comparisonPropertyPath;
        private readonly bool _allowEqual;
        private string _comparisonPropertyDisplayName;

        public UiDateTimeGreaterThanDateAttributeOrNullValidation(string basePropertyPath, string comparisonPropertyPath, bool allowEqual = true)
            : base(DefaultErrorMessage)
        {
            _basePropertyPath = basePropertyPath;
            _comparisonPropertyPath = comparisonPropertyPath;
            _allowEqual = allowEqual;
        }  
   
        //Override default FormatErrorMessage Method  
        public override string FormatErrorMessage(string name)  
        {
            return string.Format(ErrorMessageString, name, _comparisonPropertyDisplayName);  
        }  
   
        //Override IsValid  
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)  
        {  
            if (value != null)
            {
                var propValue = UiDateTimeUtilities.ChildObjectFromValidationContext(_basePropertyPath, validationContext);
                var comparisonValue = UiDateTimeUtilities.ChildObjectFromValidationContext(_comparisonPropertyPath, validationContext);

                var displayName = UiDateTimeUtilities.GetPropertyDisplayNameFromValidationContext(_basePropertyPath, validationContext);
                _comparisonPropertyDisplayName = UiDateTimeUtilities.GetPropertyDisplayNameFromValidationContext(_comparisonPropertyPath, validationContext);

                var propDate = new DateTime();
                if (propValue != null && propValue.ToString().Length > 0)
                {
                    propDate = DateTime.Parse(propValue.ToString());
                }

                var comparisonDate = new DateTime();
                if (comparisonValue != null && comparisonValue.ToString().Length > 0)
                {
                    comparisonDate = DateTime.Parse(comparisonValue.ToString());
                }


                //Actual comparision  
                if (propDate < comparisonDate || (!_allowEqual && propDate <= comparisonDate))
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
            _comparisonPropertyDisplayName = UiDateTimeUtilities.GetPropertyDisplayNameFromModelMetadata(_comparisonPropertyPath, metadata);

            var rule = new ModelClientValidationRule
                           {
                               ErrorMessage = FormatErrorMessage(UiDateTimeUtilities.GetPropertyDisplayNameFromModelMetadata(_basePropertyPath,metadata)),
                               ValidationType ="dategreaterthanattributeornull" + _basePropertyPath.ToLower().Replace(".", "")
                           };
            rule.ValidationParameters.Add("other", _comparisonPropertyPath);
            rule.ValidationParameters.Add("allowequal", _allowEqual);
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