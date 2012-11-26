using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CustomModelBindingWithDateTime.Models.ValidationAttributes
{
    public class UiDateTimeRangeValidator : ValidationAttribute, IClientValidatable
    {
        private readonly bool _startDateNotInFuture;
        private readonly bool _startDateNotInPast;
        private readonly bool _endDateNotInFuture;
        private readonly bool _endDateNotInPast;
        private const string DefaultErrorMessage = "'{0}' does not exist or is improperly formated: MM/DD/YYYY.";

        public UiDateTimeRangeValidator(bool startDateNotInFuture, bool startDateNotInPast, bool endDateNotInFuture, bool endDateNotInPast)
            : base(DefaultErrorMessage)
        {
            _startDateNotInFuture = startDateNotInFuture;
            _startDateNotInPast = startDateNotInPast;
            _endDateNotInFuture = endDateNotInFuture;
            _endDateNotInPast = endDateNotInPast;
        }
   
        //Override IsValid  
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)  
        {  
            // Validate object value is UiDateTimeRangeModel
            UiDateTimeRangeModel valueDateTimeRange;
            if (value is UiDateTimeRangeModel)
            {
                valueDateTimeRange = value as UiDateTimeRangeModel;
            }
            else 
            {
                return new ValidationResult("Invalid UiDateTimeRangeModel object.");
            }

            if (_startDateNotInFuture) 
            {
                if (valueDateTimeRange.StartDateTime.DateTimeUtcValue > DateTime.UtcNow) 
                {
                    return new ValidationResult("Start Date cannot be in the future");
                }
            }

            if (_startDateNotInPast) 
            {
                if (valueDateTimeRange.StartDateTime.DateTimeUtcValue < DateTime.UtcNow) 
                {
                    return new ValidationResult("Start Date cannot be in the past");
                }
            }

            if (_endDateNotInFuture) 
            {
                if (valueDateTimeRange.EndDateTime.DateTimeUtcValue > DateTime.UtcNow)
                {
                    return new ValidationResult("End Date cannot be in the future");
                }
            }

            if (_endDateNotInPast) 
            {
                if (valueDateTimeRange.EndDateTime.DateTimeUtcValue < DateTime.UtcNow)
                {
                    return new ValidationResult("End Date cannot be in the past");
                }
            }

            //Validate start date is not greater than the end date
            {
                if (valueDateTimeRange.StartDateTime.DateTimeUtcValue > valueDateTimeRange.EndDateTime.DateTimeUtcValue) 
                {
                    return new ValidationResult("Start Date cannot be further in time than the End Date");
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