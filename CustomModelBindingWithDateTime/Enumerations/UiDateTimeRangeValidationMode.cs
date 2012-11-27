using System;
using System.Collections.Generic;

namespace CustomModelBindingWithDateTime.Enumerations 
{
    public enum UiDateTimeRangeValidationMode 
    {
        StartDateNotInFuture,
        StartDateNotInPast,
        EndDateNotInFuture,
        EndDateNotInPast,
        StartDateNotGreaterThanEndDate
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class UiDateTimeRangeValidationModeAttribute : Attribute 
    {
        public ICollection<UiDateTimeRangeValidationMode> UiDateTimeRangeValidationMode { get; private set; }

        public UiDateTimeRangeValidationModeAttribute(params UiDateTimeRangeValidationMode[] uiDateTimeRangeValidation) 
        {
            UiDateTimeRangeValidationMode = uiDateTimeRangeValidation;
        }

    }
}