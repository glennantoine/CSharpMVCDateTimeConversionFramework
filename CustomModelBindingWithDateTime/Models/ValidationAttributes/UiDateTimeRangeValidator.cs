using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CustomModelBindingWithDateTime.Enumerations;

namespace CustomModelBindingWithDateTime.Models.ValidationAttributes
{
    public class UiDateTimeRangeValidator : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessage = "'{0}' does not exist or is improperly formated: MM/DD/YYYY.";
        private readonly List<UiDateTimeRangeValidationMode> _valadators = new List<UiDateTimeRangeValidationMode>();

        public UiDateTimeRangeValidator(UiDateTimeRangeValidationMode[] valadators = null)
            : base(DefaultErrorMessage)
        {
            if(valadators != null) _valadators.AddRange(valadators);
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

            //Validate start date is not greater than the end date
            {
                if (valueDateTimeRange.StartDateTime.DateTimeUtcValue > valueDateTimeRange.EndDateTime.DateTimeUtcValue) {
                    return new ValidationResult("Start Date cannot be further in time than the End Date");
                }
            }

            if (_valadators.Count > 0)
            {
                foreach (UiDateTimeRangeValidationMode valadator in _valadators)
                {
                    switch (valadator)
                    {
                            case UiDateTimeRangeValidationMode.StartDateNotInFuture:
                                if (valueDateTimeRange.StartDateTime.DateTimeUtcValue > DateTime.UtcNow)
                                        return new ValidationResult("Start Date cannot be in the future");
                            break;

                            case UiDateTimeRangeValidationMode.StartDateNotInPast:
                                if (valueDateTimeRange.StartDateTime.DateTimeUtcValue < DateTime.UtcNow)
                                    return new ValidationResult("Start Date cannot be in the past");
                            break;

                            case UiDateTimeRangeValidationMode.EndDateNotInFuture:
                                if (valueDateTimeRange.EndDateTime.DateTimeUtcValue > DateTime.UtcNow)
                                    return new ValidationResult("End Date cannot be in the future");
                            break;

                            case UiDateTimeRangeValidationMode.EndDateNotInPast:
                                if (valueDateTimeRange.EndDateTime.DateTimeUtcValue < DateTime.UtcNow)
                                    return new ValidationResult("End Date cannot be in the past");
                            break;
                    }
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