using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CSharpMVCDateTimeConversionFramework.Models.ValidationAttributes;

namespace CSharpMVCDateTimeConversionFramework.Models 
{
    public class UiDateTimeEditorTemplatesTestModel 
    {
        [UiDateTimeDisplay("StartDateTime.LocalDate", "DateFrom", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeDisplay("EndDateTime.LocalDate", "DateTo", typeof(Resources.UiDateTimeResouce))]
        public UiDateTimeRangeModel RangeDateToDate { get; set; }

        [UiDateTimeDisplay("StartDateTime.LocalTime", "TimeFrom", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeDisplay("EndDateTime.LocalTime", "TimeTo", typeof(Resources.UiDateTimeResouce))]
        public UiDateTimeRangeModel RangeTimeToTime { get; set; }

        [UiDateTimeDisplay("StartDateTime.LocalDate", "DateFrom", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeDisplay("StartDateTime.LocalTime", "TimeFrom", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeDisplay("EndDateTime.LocalTime", "TimeTo", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeNotInPastValidation("StartDateTime.LocalDate", ErrorMessageResourceName = "DateNotInPast", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        [UiDateTimeRequiredIfAttributeValueNotEqualsValidation("EndDateTime.LocalDate", "StartDateTime.LocalDate", "12/04/2012", ErrorMessageResourceName = "IsRequiredValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        [UiDateTimeNotInPastValidation("EndDateTime.LocalDate", ErrorMessageResourceName = "DateNotInPast", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        public UiDateTimeRangeModel RangeDateTimeToTime { get; set; }

        [UiDateTimeDisplay("StartDateTime.LocalDate", "DateFrom", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeDisplay("EndDateTime.LocalDate", "DateTo", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeDisplay("StartDateTime.LocalTime", "TimeFrom", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeDisplay("EndDateTime.LocalTime", "TimeTo", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeRequiredValidation("StartDateTime.LocalDate", ErrorMessageResourceName = "IsRequiredValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        [UiDateTimeRequiredValidation("StartDateTime.LocalTime", ErrorMessageResourceName = "IsRequiredValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        [UiDateTimeGreaterThanDateAttributeOrNullValidation("EndDateTime.LocalDate", "StartDateTime.LocalDate", ErrorMessageResourceName = "DateMustBeGreaterThanEqualTo", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        public UiDateTimeRangeModel RangeDateTimeToDateTime { get; set; }


        [UiDateTimeDisplay("LocalDate", "Date", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeRequiredValidation("LocalDate", ErrorMessageResourceName = "IsRequiredValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        public UiDateTimeModel Date { get; set; }

        [UiDateTimeDisplay("LocalTime", "Time", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeRequiredValidation("LocalTime", ErrorMessageResourceName = "IsRequiredValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        public UiDateTimeModel Time { get; set; }

        [UiDateTimeDisplay("LocalDate", "Date", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeDisplay("LocalTime", "Time", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeNotInFutureValidation("LocalDate", ErrorMessageResourceName = "DateNotInFuture", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        public UiDateTimeModel DateTime { get; set; }

        [UiDateTimeDisplay("LocalDate", "Date", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeDisplay("LocalTime", "Time", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeDisplay("NoSetTime", "NoSetTime", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeRequiredIfAttributeValueNotEqualsValidation("LocalTime", "NoSetTime", "True", ErrorMessageResourceName = "IsRequiredValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        public UiDateTimeModel DateTimeNoSetTime { get; set; }

    }
}
