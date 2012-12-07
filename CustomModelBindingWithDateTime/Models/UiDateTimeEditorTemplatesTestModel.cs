using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CustomModelBindingWithDateTime.Models.ValidationAttributes;
using sCRM.Presentation.Web.Areas.Crm.Models.ValidationAttributes;

namespace CustomModelBindingWithDateTime.Models 
{
    public class UiDateTimeEditorTemplatesTestModel 
    {
        [UiDateTimeDisplayAttribute(DisplayName = "Date From", PropertyPath = "StartDateTime.LocalDate")]
        [UiDateTimeDisplayAttribute(DisplayName = "Date To", PropertyPath = "EndDateTime.LocalDate")]
        public UiDateTimeRangeModel RangeDateToDate { get; set; }

        [UiDateTimeDisplayAttribute(DisplayName = "Time From", PropertyPath = "StartDateTime.LocalTime")]
        [UiDateTimeDisplayAttribute(DisplayName = "Time To", PropertyPath = "EndDateTime.LocalTime")]
        public UiDateTimeRangeModel RangeTimeToTime { get; set; }

        [UiDateTimeDisplayAttribute(DisplayName = "Date From", PropertyPath = "StartDateTime.LocalDate")]
        [UiDateTimeDisplayAttribute(DisplayName = "Time From", PropertyPath = "StartDateTime.LocalTime")]
        [UiDateTimeDisplayAttribute(DisplayName = "Time To", PropertyPath = "EndDateTime.LocalTime")]
        [UiDateTimeNotInPastValidation("StartDateTime.LocalDate", ErrorMessageResourceName = "DateNotInPast", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        [UiDateTimeRequiredIfAttributeValueNotEqualsValidation("EndDateTime.LocalDate", "StartDateTime.LocalDate", "12/04/2012", ErrorMessageResourceName = "IsRequiredValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        [UiDateTimeNotInPastValidation("EndDateTime.LocalDate", ErrorMessageResourceName = "DateNotInPast", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        public UiDateTimeRangeModel RangeDateTimeToTime { get; set; }

        [UiDateTimeDisplayAttribute(DisplayName = "DateTime From", PropertyPath = "StartDateTime.LocalDate")]
        [UiDateTimeDisplayAttribute(DisplayName = "Time From", PropertyPath = "StartDateTime.LocalTime")]
        [UiDateTimeDisplayAttribute(DisplayName = "Date To", PropertyPath = "EndDateTime.LocalDate")]
        [UiDateTimeDisplayAttribute(DisplayName = "Time To", PropertyPath = "EndDateTime.LocalTime")]
        [UiDateTimeRequiredValidation("StartDateTime.LocalDate", ErrorMessageResourceName = "IsRequiredValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        [UiDateTimeRequiredValidation("StartDateTime.LocalTime", ErrorMessageResourceName = "IsRequiredValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        [UiDateTimeGreaterThanDateAttributeOrNullValidation("EndDateTime.LocalDate", "StartDateTime.LocalDate", ErrorMessageResourceName = "DateMustBeGreaterThanEqualTo", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        public UiDateTimeRangeModel RangeDateTimeToDateTime { get; set; }


        [UiDateTimeDisplayAttribute(DisplayName = "Date", PropertyPath = "LocalDate")]
        [UiDateTimeRequiredValidation("LocalDate", ErrorMessageResourceName = "IsRequiredValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        public UiDateTimeModel Date { get; set; }

        [UiDateTimeDisplayAttribute(DisplayName = "Time", PropertyPath = "LocalTime")]
        [UiDateTimeRequiredValidation("LocalTime", ErrorMessageResourceName = "IsRequiredValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        public UiDateTimeModel Time { get; set; }

        [UiDateTimeDisplayAttribute(DisplayName = "Date", PropertyPath = "LocalDate")]
        [UiDateTimeDisplayAttribute(DisplayName = "Time", PropertyPath = "LocalTime")]
        [UiDateTimeNotInFutureValidation("LocalDate", ErrorMessageResourceName = "DateNotInFuture", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        public UiDateTimeModel DateTime { get; set; }

        [UiDateTimeDisplayAttribute(DisplayName = "Date", PropertyPath = "LocalDate")]
        [UiDateTimeDisplayAttribute(DisplayName = "Time", PropertyPath = "LocalTime")]
        [UiDateTimeDisplayAttribute(DisplayName = "No Set Time", PropertyPath = "NoSetTime")]
        [UiDateTimeRequiredIfAttributeValueNotEqualsValidation("LocalTime", "NoSetTime", "True", ErrorMessageResourceName = "IsRequiredValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        public UiDateTimeModel DateTimeNoSetTime { get; set; }

    }
}
