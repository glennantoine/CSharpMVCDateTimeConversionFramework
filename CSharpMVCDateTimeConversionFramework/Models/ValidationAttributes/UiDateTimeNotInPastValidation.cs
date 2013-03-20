using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CSharpMVCDateTimeConversionFramework.Utilities;

namespace CSharpMVCDateTimeConversionFramework.Models.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class UiDateTimeNotInPastValidation : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessage = "'{0}' must be in the future.";
        private readonly object _typeId = new object();
        private readonly string _basePropertyPath;

        public UiDateTimeNotInPastValidation(string basePropertyPath)
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
            if (value != null)
            {
                var propValue = UiDateTimeUtilities.ChildObjectFromValidationContext(_basePropertyPath, validationContext);

                var displayName = UiDateTimeUtilities.GetPropertyDisplayNameFromValidationContext(_basePropertyPath, validationContext);

                //Actual comparision 
                var dateObj = new DateTime();
                if (propValue != null && propValue.ToString().Length > 0)
                {
                    dateObj = DateTime.Parse(propValue.ToString());
                }

                //Actual comparision  
                if (DateTime.UtcNow.Date > dateObj.Date)
                {
                    var message = FormatErrorMessage(displayName);
                    return new ValidationResult(message);
                }
            }

            //Default return - This means there were no validation errors
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
                           {
                               ErrorMessage = FormatErrorMessage(UiDateTimeUtilities.GetPropertyDisplayNameFromModelMetadata(_basePropertyPath, metadata)),
                               ValidationType = "uidatetimenotinpast" + _basePropertyPath.ToLower().Replace(".", "")
                           };

            //This string identifies which Javascript function to be executed to validate this   
            yield return rule;
        }

        public override object TypeId
        {
            get { return _typeId; }
        }

    }
}