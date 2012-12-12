using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using CustomModelBindingWithDateTime.Models.ValidationAttributes;
using CustomModelBindingWithDateTime.Utilities;

namespace CustomModelBindingWithDateTime.Models 
{
    [UiDateTimeFormatDateValidation("LocalDate", ErrorMessageResourceName = "DateFormat", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
    [UiDateTimeFormatTimeValidation("LocalTime", ErrorMessageResourceName = "TimeFormatValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
    [UiDateTimeMaxLimitDateValidation("LocalDate", 50, ErrorMessageResourceName = "DateMustBeLessThanYearsInFuture", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
    public class UiDateTimeModel
    {

        /// <summary>
        /// Constructor for UiDateTimeModel that takes in the local time zone name to base the DateTime off of.
        /// </summary>
        /// <param name="timeZoneName">TimeZoneInfo name of the local time being represented.</param>
        public UiDateTimeModel(string timeZoneName)
        {
            TimeZoneName = timeZoneName;
        }

        [Display(Name = "UTC Date Time")]
        public DateTime? DateTimeUtcValue
        {
            get
            {
                DateTime dateResult;
                if (string.IsNullOrWhiteSpace(LocalDate) && string.IsNullOrWhiteSpace(LocalTime))
                {
                    return null;
                }

                if (DateTime.TryParse(ModelLocalDateValue + " " + ModelLocalTimeValue, out dateResult)) 
                {
                    dateResult = dateResult.ToUniversalTime(TimeZoneName);
                }else
                {
                    return null;
                }

                return dateResult;
            }
            set
            {
                var date = value;
                if(date.HasValue)
                {
                    DateTime temp;
                    DateTime.TryParse(date.Value.ToString(CultureInfo.InvariantCulture), out temp);
                    DateTimeLocalValue = temp.ToLocalTime(TimeZoneName);
                }
                else
                {
                    DateTimeLocalValue = null;
                }
                
            }
        }

        /// <summary>
        /// DateTimeLocalValue is optionally available to be used in 
        /// Forms/Throughout the view layer in the event a complete
        /// DateTime object is required
        /// </summary>
        [Display(Name = "Date Time")]
        public DateTime? DateTimeLocalValue 
        {
            get 
            {
                DateTime dateResult;
                if (!string.IsNullOrWhiteSpace(LocalDate) && !string.IsNullOrWhiteSpace(LocalTime))
                {
                    DateTime.TryParse(LocalDate + " " + LocalTime, out dateResult);
                } 
                else if (string.IsNullOrWhiteSpace(LocalDate) ^ string.IsNullOrWhiteSpace(LocalTime))
                {
                    if(!DateTime.TryParse(ModelLocalDateValue + " " + ModelLocalTimeValue, out dateResult))
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                } 
                return dateResult;
            }
            set 
            {
                var date = value;
                SetLocalDateTimeFields(date);               
            }
        }

        /// <summary>
        /// The following 4 fields are available to be used in 
        /// Forms/Throughout the view layer
        /// </summary>
        [Display(Name = "Date")]
        public string LocalDate { get; set; }

        [Display(Name = "Time")]
        public string LocalTime { get; set; }

        [Display(Name = "TimeZone")]
        public string TimeZoneName { get; set; }

        [Display(Name = "No Set Time")]
        public bool NoSetTime { get; set; }

        /// <summary>
        /// ******DO NOT USE THESE PROPERTIES IN FORMS******
        /// The following properties are for use in the Views only as 
        /// they provide DateTime formats in the most commonly used formats
        /// throughout the application. 
        /// LocalDateTime: 6/15/2009 1:09 AM 
        /// </summary>
        public string LocalDateTime 
        {
            get
            {
                return SetDateTimeFormat("g");
            }
        }

        /// <summary>
        /// Abbreviated Month Name: Jun, Jul, etc
        /// </summary>
        public string LocalDateTimeAbreviatedMonthName 
        {
            get
            {
                return SetDateTimeFormat("MMM");
            }
        }

        /// <summary>
        /// Numeric Day of the Month
        /// </summary>
        public string LocalDateTimeDayOfMonth 
        {
            get { return SetDateTimeFormat("%d"); }
        }

        /// <summary>
        /// Day Month Numeric Day, Year
        /// Thursday January 15, 2012
        /// </summary>
        public string LocalDateTimeDayWithFullDate
        {
            get
            {
                return SetDateTimeFormat("dddd, MMMM d, yyyy");
            }
        }		
		
        /// <summary>
        /// For Model Use Only
        /// Used to keep all fields in sync
        /// </summary>
        private string ModelLocalDateValue { get; set; }
        private string ModelLocalTimeValue { get; set; }

        private string SetDateTimeFormat(string format)
        {
            if(!string.IsNullOrWhiteSpace(LocalDate) && !string.IsNullOrWhiteSpace(LocalTime))
            {
               return DateTime.Parse(LocalDate + " " + LocalTime).ToString(format);
            }
            return string.IsNullOrWhiteSpace(LocalDate) ? String.Empty : DateTime.Parse(LocalDate).ToString(format);
        }		
		
		
        private void SetLocalDateTimeFields(DateTime? localDateTime)
        {
                DateTime temp;
                if (localDateTime.HasValue && DateTime.TryParse(localDateTime.Value.ToString(CultureInfo.InvariantCulture), out temp))
                {
                    if (temp.Date == DateTime.MinValue.Date || temp.TimeOfDay == DateTime.MinValue.TimeOfDay) 
                    {
                        ModelLocalDateValue = temp.ToShortDateString();
                        ModelLocalTimeValue = temp.ToShortTimeString();
                        LocalDate = temp.Date == DateTime.MinValue.Date ? String.Empty : temp.ToShortDateString();

                        //DateTime.MinValue.TimeOfDay cannot be assumed to equal no time set so for now this is commented out
                        //LocalTime = temp.TimeOfDay == DateTime.MinValue.TimeOfDay ? String.Empty : temp.ToShortTimeString();
                        LocalTime = temp.ToShortTimeString();
                    }else
                    {
                        ModelLocalDateValue = temp.ToShortDateString();
                        LocalDate = temp.ToShortDateString();
                        ModelLocalTimeValue = temp.ToShortTimeString();
                        LocalTime = temp.ToShortTimeString();                     
                    }
                }else
                {
                    LocalDate = string.Empty;
                    LocalTime = string.Empty;   
                }
        }

    }
}
