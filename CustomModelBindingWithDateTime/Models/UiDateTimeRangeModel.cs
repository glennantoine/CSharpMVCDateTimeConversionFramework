
using CustomModelBindingWithDateTime.Models.ValidationAttributes;

namespace CustomModelBindingWithDateTime.Models 
{
    [UiDateTimeFormatDateValidation("StartDateTime.LocalDate", ErrorMessageResourceName = "DateFormat", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
    [UiDateTimeFormatDateValidation("EndDateTime.LocalDate", ErrorMessageResourceName = "DateFormat", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
    [UiDateTimeFormatTimeValidation("StartDateTime.LocalTime", ErrorMessageResourceName = "TimeFormatValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
    [UiDateTimeFormatTimeValidation("EndDateTime.LocalTime", ErrorMessageResourceName = "TimeFormatValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
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

        public UiDateTimeModel StartDateTime { get; set; }
        public UiDateTimeModel EndDateTime { get; set; }

    }
}