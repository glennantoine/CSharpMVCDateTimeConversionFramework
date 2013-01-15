using System;
using CustomModelBindingWithDateTime.Models.ValidationAttributes;

namespace CustomModelBindingWithDateTime.Models
{
    [UiDateTimeFormatDateValidation("StartDateTime.LocalDate", ErrorMessageResourceName = "DateFormatValid", ErrorMessageResourceType = typeof (Resources.ValidationResource))]
    [UiDateTimeFormatDateValidation("EndDateTime.LocalDate", ErrorMessageResourceName = "DateFormatValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
    [UiDateTimeFormatTimeValidation("StartDateTime.LocalTime", ErrorMessageResourceName = "TimeFormatValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
    [UiDateTimeFormatTimeValidation("EndDateTime.LocalTime", ErrorMessageResourceName = "TimeFormatValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
    [UiDateTimeMaxLimitDateValidation("StartDateTime.LocalDate", 50, ErrorMessageResourceName = "DateMustBeLessThanYearsInFuture", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
    [UiDateTimeMaxLimitDateValidation("EndDateTime.LocalDate", 50, ErrorMessageResourceName = "DateMustBeLessThanYearsInFuture", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
    public class UiDateTimeRangeModel
    {
        private string _timeZoneName;

        public string TimeZoneName
        {
            get { return _timeZoneName; }
            set
            {
                _timeZoneName = value;
                if (StartDateTime != null)
                {
                    StartDateTime.TimeZoneName = _timeZoneName;
                }
                if (EndDateTime != null)
                {
                    EndDateTime.TimeZoneName = _timeZoneName;
                }
            }
        }

        /// <summary>
        /// Constructor for UiDateTimeModel that takes in the local time zone name to base the DateTime off of.
        /// </summary>
        /// <param name="timeZoneName">TimeZoneInfo name of the local time being represented.</param>
        public UiDateTimeRangeModel(string timeZoneName)
        {
            StartDateTime = new UiDateTimeModel(timeZoneName);
            EndDateTime = new UiDateTimeModel(timeZoneName);
            TimeZoneName = timeZoneName;
        }

        public UiDateTimeRangeModel(string timeZoneName, DateTime? startDateUtc, DateTime? endDateUtc)
        {
            StartDateTime = new UiDateTimeModel(timeZoneName) {DateTimeUtcValue = startDateUtc};
            EndDateTime = new UiDateTimeModel(timeZoneName) {DateTimeUtcValue = endDateUtc};
            TimeZoneName = timeZoneName;
        }

        public UiDateTimeRangeModel(string timeZoneName, DateTime? startDateUtc, DateTime? endDateUtc, bool isAllDay = false)
        {
            StartDateTime = new UiDateTimeModel(timeZoneName) {DateTimeUtcValue = startDateUtc, NoSetTime = isAllDay};
            EndDateTime = new UiDateTimeModel(timeZoneName) {DateTimeUtcValue = endDateUtc};
            TimeZoneName = timeZoneName;
        }

        public UiDateTimeModel StartDateTime { get; set; }
        public UiDateTimeModel EndDateTime { get; set; }

    }
}