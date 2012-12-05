using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CustomModelBindingWithDateTime.Enumerations;

namespace CustomModelBindingWithDateTime.Models.ValidationAttributes {
    public class UiDateTimeValidator : ValidationAttribute {
        private readonly List<UiDateTimeValidationMode> _valadators = new List<UiDateTimeValidationMode>();
        private const string DefaultErrorMessage = "'{0}' does not exist or is improperly formated: MM/DD/YYYY.";

        public UiDateTimeValidator(UiDateTimeValidationMode[] valadators = null)
            : base(DefaultErrorMessage) {
            if (valadators != null) _valadators.AddRange(valadators);
        }

        //Override IsValid  
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            // Validate object value is UiDateTimeModel
            UiDateTimeModel valueDateTime;
            if (value is UiDateTimeModel) {
                valueDateTime = value as UiDateTimeModel;
            } else {
                return new ValidationResult("Invalid UiDateTimeModel object");
            }


            if (_valadators.Count > 0) {
                foreach (UiDateTimeValidationMode valadator in _valadators) {
                    switch (valadator) {
                        case UiDateTimeValidationMode.DateNotInFuture:
                            if (valueDateTime.DateTimeUtcValue > DateTime.UtcNow)
                                return new ValidationResult("Start Date cannot be in the future");
                            break;

                        case UiDateTimeValidationMode.DateNotInPast:
                            if (valueDateTime.DateTimeUtcValue < DateTime.UtcNow)
                                return new ValidationResult("Start Date cannot be in the past");
                            break;
                        default:
                            throw new Exception("Invalid UiDateTimeValidationMode");
                    }
                }

            }

            return ValidationResult.Success;
        }

        //Override default FormatErrorMessage Method  
        public override string FormatErrorMessage(string name) {
            return string.Format(ErrorMessageString, name);
        }

    }
}