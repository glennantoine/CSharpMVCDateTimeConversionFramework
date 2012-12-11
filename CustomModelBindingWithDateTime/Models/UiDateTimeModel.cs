using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using CustomModelBindingWithDateTime.Models.ValidationAttributes;
using CustomModelBindingWithDateTime.Utilities;

namespace CustomModelBindingWithDateTime.Models 
{
    [UiDateTimeFormatDateValidation("LocalDate", ErrorMessageResourceName = "DateFormat", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
    //[UiDateTimeFormatTimeValidation("LocalTime", ErrorMessageResourceName = "TimeFormatValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
    //[UiDateTimeMaxLimitDateValidation("LocalDate", 50, ErrorMessageResourceName = "DateMustBeLessThanYearsInFuture", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
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

                if (DateTime.TryParse(LocalDateValue + " " + LocalTimeValue, out dateResult)) 
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
                    if(!DateTime.TryParse(LocalDateValue + " " + LocalTimeValue, out dateResult))
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

        [Display(Name = "Date")]
        public string LocalDate { get; set; }

        [Display(Name = "Time")]
        public string LocalTime { get; set; }

        [Display(Name = "TimeZone")]
        public string TimeZoneName { get; set; }

        [Display(Name = "No Set Time")]
        public bool NoSetTime { get; set; }

        //For Model Use Only
        private string LocalDateValue { get; set; }
        private string LocalTimeValue { get; set; }

        private void SetLocalDateTimeFields(DateTime? localDateTime)
        {
                DateTime temp;
                if (localDateTime.HasValue && DateTime.TryParse(localDateTime.Value.ToString(CultureInfo.InvariantCulture), out temp))
                {
                    if (temp.Date == DateTime.MinValue.Date || temp.TimeOfDay == DateTime.MinValue.TimeOfDay) 
                    {
                        LocalDateValue = temp.ToShortDateString();
                        LocalTimeValue = temp.ToShortTimeString();
                        LocalDate = temp.Date == DateTime.MinValue.Date ? String.Empty : temp.ToShortDateString();

                        //DateTime.MinValue.TimeOfDay cannot be assumed to equal no time set so for now this is commented out
                        //LocalTime = temp.TimeOfDay == DateTime.MinValue.TimeOfDay ? String.Empty : temp.ToShortTimeString();
                        LocalTime = temp.ToShortTimeString();
                    }else
                    {
                        LocalDateValue = temp.ToShortDateString();
                        LocalDate = temp.ToShortDateString();
                        LocalTimeValue = temp.ToShortTimeString();
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
