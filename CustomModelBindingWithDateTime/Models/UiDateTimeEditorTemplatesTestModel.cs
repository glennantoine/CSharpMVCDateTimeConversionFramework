using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CustomModelBindingWithDateTime.Models.ValidationAttributes;

namespace CustomModelBindingWithDateTime.Models 
{
    public class UiDateTimeEditorTemplatesTestModel 
    {
        [UiDateTimeDisplayAttribute("StartDateTime.LocalDate", "DateFrom", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeDisplayAttribute("EndDateTime.LocalDate", "DateTo", typeof(Resources.UiDateTimeResouce))]
        public UiDateTimeRangeModel RangeDateToDate { get; set; }

        [UiDateTimeDisplayAttribute("StartDateTime.LocalTime", "TimeFrom", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeDisplayAttribute("EndDateTime.LocalTime", "TimeTo", typeof(Resources.UiDateTimeResouce))]
        public UiDateTimeRangeModel RangeTimeToTime { get; set; }

        [UiDateTimeDisplayAttribute("StartDateTime.LocalDate", "DateFrom", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeDisplayAttribute("StartDateTime.LocalTime", "TimeFrom", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeDisplayAttribute("EndDateTime.LocalTime", "TimeTo", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeNotInPastValidation("StartDateTime.LocalDate", ErrorMessageResourceName = "DateNotInPast", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        [UiDateTimeRequiredIfAttributeValueNotEqualsValidation("EndDateTime.LocalDate", "StartDateTime.LocalDate", "12/04/2012", ErrorMessageResourceName = "IsRequiredValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        [UiDateTimeNotInPastValidation("EndDateTime.LocalDate", ErrorMessageResourceName = "DateNotInPast", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        public UiDateTimeRangeModel RangeDateTimeToTime { get; set; }

        [UiDateTimeDisplayAttribute("StartDateTime.LocalDate", "DateFrom", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeDisplayAttribute("EndDateTime.LocalDate", "DateTo", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeDisplayAttribute("StartDateTime.LocalTime", "TimeFrom", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeDisplayAttribute("EndDateTime.LocalTime", "TimeTo", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeRequiredValidation("StartDateTime.LocalDate", ErrorMessageResourceName = "IsRequiredValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        [UiDateTimeRequiredValidation("StartDateTime.LocalTime", ErrorMessageResourceName = "IsRequiredValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        [UiDateTimeGreaterThanDateAttributeOrNullValidation("EndDateTime.LocalDate", "StartDateTime.LocalDate", ErrorMessageResourceName = "DateMustBeGreaterThanEqualTo", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        public UiDateTimeRangeModel RangeDateTimeToDateTime { get; set; }


        [UiDateTimeDisplayAttribute("LocalDate", "Date", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeRequiredValidation("LocalDate", ErrorMessageResourceName = "IsRequiredValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        public UiDateTimeModel Date { get; set; }

        [UiDateTimeDisplayAttribute("LocalTime", "Time", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeRequiredValidation("LocalTime", ErrorMessageResourceName = "IsRequiredValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        public UiDateTimeModel Time { get; set; }

        [UiDateTimeDisplayAttribute("LocalDate", "Date", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeDisplayAttribute("LocalTime", "Time", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeNotInFutureValidation("LocalDate", ErrorMessageResourceName = "DateNotInFuture", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        public UiDateTimeModel DateTime { get; set; }

        [UiDateTimeDisplayAttribute("LocalDate", "Date", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeDisplayAttribute("LocalTime", "Time", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeDisplayAttribute("NoSetTime", "NoSetTime", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeRequiredIfAttributeValueNotEqualsValidation("LocalTime", "NoSetTime", "True", ErrorMessageResourceName = "IsRequiredValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        public UiDateTimeModel DateTimeNoSetTime { get; set; }

    }
}
