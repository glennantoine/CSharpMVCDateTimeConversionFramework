using System;
using System.ComponentModel.DataAnnotations;
using CustomModelBindingWithDateTime.Utilities;

namespace CustomModelBindingWithDateTime.Models 
{
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
        public DateTime DateTimeUtcValue
        {
            get
            {
                var dateResult = DateTime.MinValue;
                if (!string.IsNullOrWhiteSpace(LocalDate) && !string.IsNullOrWhiteSpace(LocalTime))
                {
                    if (DateTime.TryParse(LocalDate + " " + LocalTime, out dateResult))
                    {
                        dateResult = dateResult.ToUniversalTime(TimeZoneName);
                    }
                }
                return dateResult;
            }
            set
            {
                var date = value;
                DateTimeLocalValue = date.ToLocalTime(TimeZoneName);
            }
        }

        [Display(Name = "Date Time")]
        public DateTime DateTimeLocalValue 
        {
            get 
            {
                var dateResult = DateTime.MinValue;
                LocalDate = string.IsNullOrWhiteSpace(LocalDate) ? DateTime.MinValue.ToShortDateString() : LocalDate;
                LocalTime = string.IsNullOrWhiteSpace(LocalTime) ? DateTime.MinValue.ToShortTimeString() : LocalTime;
                if (!string.IsNullOrWhiteSpace(LocalDate) && !string.IsNullOrWhiteSpace(LocalTime))
                {
                    DateTime.TryParse(LocalDate + " " + LocalTime, out dateResult);
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

        private void SetLocalDateTimeFields(DateTime localDateTime)
        {
            if (localDateTime == DateTime.MinValue)
            {
                LocalDate = string.Empty;
                LocalTime = string.Empty;
            }
            else
            {
                var convertDate = localDateTime;
                LocalDate = convertDate.ToShortDateString();
                LocalTime = convertDate.ToShortTimeString();
            }
        }

    }
}
