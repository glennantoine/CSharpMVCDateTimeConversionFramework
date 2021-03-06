﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CSharpMVCDateTimeConversionFramework.Utilities;

namespace CSharpMVCDateTimeConversionFramework.Models.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class UiDateTimeNotInFutureValidation : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessage = "'{0}' must be in the past.";
        private readonly object _typeId = new object();
        private readonly string _basePropertyPath;

        public UiDateTimeNotInFutureValidation(string basePropertyPath)
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

                if (propValue != null && !String.IsNullOrEmpty(propValue.ToString()))
                {

                    var thisDate = DateTime.Parse(propValue.ToString());

                    //Actual comparision  
                    if (DateTime.UtcNow.Date < thisDate)
                    {
                        var displayName = UiDateTimeUtilities.GetPropertyDisplayNameFromValidationContext(_basePropertyPath, validationContext);
                        var message = FormatErrorMessage(displayName);
                        return new ValidationResult(message);
                    }
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
                               ValidationType = "uidatetimenotinfuture" + _basePropertyPath.ToLower().Replace(".", "")
                           };

            //This string identifies which Javascript function to be executed to validate this   
            yield return rule;
        }

    }
}
