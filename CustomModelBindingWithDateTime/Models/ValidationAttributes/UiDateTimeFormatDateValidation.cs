using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CustomModelBindingWithDateTime.Utilities;

namespace CustomModelBindingWithDateTime.Models.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class UiDateTimeFormatDateValidation : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessage = "'{0}' does not exist or is improperly formated: MM/DD/YYYY.";
        private readonly object _typeId = new object();
        private readonly string _basePropertyPath;

        public UiDateTimeFormatDateValidation(string basePropertyPath)
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
            var propValue = UiDateTimeUtilities.ChildObjectFromValidationContext(_basePropertyPath, validationContext);

            var displayName = UiDateTimeUtilities.GetPropertyDisplayNameFromValidationContext(_basePropertyPath, validationContext);
            try
            {
                var temp = DateTime.Parse(propValue.ToString());
            }
            catch (FormatException)
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
                ValidationType = "uidatetimeformatdate" + _basePropertyPath.ToLower().Replace(".", "")
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