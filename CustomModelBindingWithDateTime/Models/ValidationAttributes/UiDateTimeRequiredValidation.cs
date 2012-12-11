using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CustomModelBindingWithDateTime.Utilities;

namespace CustomModelBindingWithDateTime.Models.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class UiDateTimeRequiredValidation : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessage = "'{0}' is a required field.";
        private readonly object _typeId = new object();
        private readonly string _basePropertyPath;

        public UiDateTimeRequiredValidation(string basePropertyPath)
            : base(DefaultErrorMessage)  
        {
            _basePropertyPath = basePropertyPath;
        }  
   
        //Override default FormatErrorMessage Method  
        public override string FormatErrorMessage(string name)  
        {
            return string.Format(ErrorMessageString, name);  
        }  
   
        //Override IsValid  
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)  
        {
            var propValue = UiDateTimeUtilities.ChildObjectFromValidationContext(_basePropertyPath,
                                                                                    validationContext);

            var displayName = UiDateTimeUtilities.GetPropertyDisplayNameFromValidationContext(_basePropertyPath,
                                                                                                validationContext);
            if (propValue == null || propValue.ToString().Length == 0)
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
                ValidationType = "uidatetimerequired" + _basePropertyPath.ToLower().Replace(".", "")
            };
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