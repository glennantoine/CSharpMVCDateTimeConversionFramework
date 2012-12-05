using System;
using System.Collections.Generic;

namespace CustomModelBindingWithDateTime.Enumerations 
{

    public enum UiDateTimeValidationMode 
    {
        DateNotInFuture,
        DateNotInPast
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class UiDateTimeValidationModeAttribute : Attribute 
    {
        public ICollection<UiDateTimeValidationMode> UiDateTimeRangeValidationMode { get; private set; }

        public UiDateTimeValidationModeAttribute(params UiDateTimeValidationMode[] uiDateTimeRangeValidation) 
        {
            UiDateTimeRangeValidationMode = uiDateTimeRangeValidation;
        }

    }

}



