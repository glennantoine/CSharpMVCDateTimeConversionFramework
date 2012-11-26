using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CustomModelBindingWithDateTime.Models.ValidationAttributes
{
    public class UiDateTimeValidator : ValidationAttribute, IClientValidatable
    {
        private readonly bool _dateNotInFuture;
        private readonly bool _dateNotInPast;
        private const string DefaultErrorMessage = "'{0}' does not exist or is improperly formated: MM/DD/YYYY.";

        public UiDateTimeValidator(bool dateNotInFuture, bool dateNotInPast)
            : base(DefaultErrorMessage)
        {
            _dateNotInFuture = dateNotInFuture;
            _dateNotInPast = dateNotInPast;
        }
   
        //Override IsValid  
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)  
        {  
            // Validate object value is UiDateTimeModel
            UiDateTimeModel valueDateTimeRange;
            if (value is UiDateTimeModel)
            {
                valueDateTimeRange = value as UiDateTimeModel;
            }
            else 
            {
                return new ValidationResult("Invalid UiDateTimeModel object");
            }

            if (_dateNotInFuture) 
            {
                if (valueDateTimeRange.DateTimeUtcValue > DateTime.UtcNow) 
                {
                    return new ValidationResult("Start Date cannot be in the future");
                }
            }

            if (_dateNotInPast) 
            {
                if (valueDateTimeRange.DateTimeUtcValue < DateTime.UtcNow) 
                {
                    return new ValidationResult("Start Date cannot be in the past");
                }
            }

            return ValidationResult.Success;
        }

        //Override default FormatErrorMessage Method  
        public override string FormatErrorMessage(string name) 
        {
            return string.Format(ErrorMessageString, name);
        }  

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());

            //This string identifies which Javascript function to be executed to validate this   
            rule.ValidationType = "date";
            yield return rule;
        }  

    }
}