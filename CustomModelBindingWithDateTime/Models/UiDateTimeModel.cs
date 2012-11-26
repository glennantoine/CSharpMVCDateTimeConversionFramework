using System;
using System.ComponentModel.DataAnnotations;
using CustomModelBindingWithDateTime.Utils;

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

//        [Required]
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

//        [Required]
        public DateTime DateTimeLocalValue 
        {
            get 
            {
                var dateResult = DateTime.MinValue;
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

//        [Required]
        public string LocalDate { get; set; }

//        [Required]
        public string LocalTime { get; set; }


//        [Required]
        public string TimeZoneName { get; set; }

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