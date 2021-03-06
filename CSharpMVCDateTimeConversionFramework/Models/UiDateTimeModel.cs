﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Globalization;
using CSharpMVCDateTimeConversionFramework.Models.ValidationAttributes;
using CSharpMVCDateTimeConversionFramework.Utilities;

namespace CSharpMVCDateTimeConversionFramework.Models
{
    [UiDateTimeFormatDateValidation("LocalDate", ErrorMessageResourceName = "DateFormatValid", ErrorMessageResourceType = typeof (Resources.ValidationResource))]
    [UiDateTimeFormatTimeValidation("LocalTime", ErrorMessageResourceName = "TimeFormatValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
    [UiDateTimeMaxLimitDateValidation("LocalDate", 50, ErrorMessageResourceName = "DateMustBeLessThanYearsInFuture", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
    public class UiDateTimeModel : IComparable
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
                if (!string.IsNullOrWhiteSpace(LocalDate) && !string.IsNullOrWhiteSpace(LocalTime))
                {
                    DateTime.TryParse(LocalDate + " " + LocalTime, out dateResult);
                }
                else if (string.IsNullOrWhiteSpace(LocalDate) ^ string.IsNullOrWhiteSpace(LocalTime))
                {
                    if (!DateTime.TryParse(ModelLocalDateValue + " " + ModelLocalTimeValue, out dateResult))
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
                return dateResult.ToUniversalTime(TimeZoneName);
            }
            set
            {
                var date = value;
                if (date.HasValue)
                {
                    DateTime temp;
                    if (DateTime.TryParse(date.Value.ToString(CultureInfo.InvariantCulture), out temp))
                    {
                        DateTimeLocalValue = temp.ToLocalTime(TimeZoneName);
                    }
                    else
                    {
                        DateTimeLocalValue = null;
                    }

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
                    ModelLocalDateValue = string.IsNullOrWhiteSpace(LocalDate) ? SqlDateTime.MinValue.Value.ToShortDateString() : LocalDate;
                    ModelLocalTimeValue = string.IsNullOrWhiteSpace(LocalTime) ? SqlDateTime.MinValue.Value.ToShortTimeString() : LocalTime;
                    if (!DateTime.TryParse(ModelLocalDateValue + " " + ModelLocalTimeValue, out dateResult))
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

        /// <summary>
        /// This property MUST be used in forms in order
        /// to convert from local to utc on post
        /// </summary>
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
            get { return SetDateTimeFormat("g"); }
        }

        /// <summary>
        /// Abbreviated Month Name: Jun, Jul, etc
        /// </summary>
        public string LocalDateTimeAbreviatedMonthName
        {
            get { return SetDateTimeFormat("MMM"); }
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
            get { return SetDateTimeFormat("dddd, MMMM d, yyyy"); }
        }

        /// <summary>
        /// For Model Use Only
        /// Used to keep all fields in sync
        /// </summary>
        private string ModelLocalDateValue { get; set; }

        private string ModelLocalTimeValue { get; set; }

        /// <summary>
        /// NOT for use by UI (Model Binding Only)
        /// This property is to be used to indicate that 
        /// the datetime value was set through model binding 
        /// and not directly by the user
        /// </summary>
        public bool ImplicitlySet { get; set; }

        /// <summary>
        /// Used to set specially formated datetime values for the UI
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        private string SetDateTimeFormat(string format)
        {
            if (!string.IsNullOrWhiteSpace(LocalDate) && !string.IsNullOrWhiteSpace(LocalTime))
            {
                return DateTime.Parse(LocalDate + " " + LocalTime).ToString(format);
            }
            return string.IsNullOrWhiteSpace(LocalDate) ? String.Empty : DateTime.Parse(LocalDate).ToString(format);
        }


        /// <summary>
        /// Used to keep string value properties in sync with the setting of the DateTimeLocalValue DateTime property
        /// </summary>
        /// <param name="localDateTime"></param>
        private void SetLocalDateTimeFields(DateTime? localDateTime)
        {
            DateTime temp;
            if (localDateTime.HasValue && DateTime.TryParse(localDateTime.Value.ToString(CultureInfo.InvariantCulture), out temp))
            {
                if (temp.Date == SqlDateTime.MinValue.Value.Date || temp.TimeOfDay == SqlDateTime.MinValue.Value.TimeOfDay)
                {
                    ModelLocalDateValue = temp.ToShortDateString();
                    ModelLocalTimeValue = temp.ToShortTimeString();
                    LocalDate = temp.Date == SqlDateTime.MinValue.Value.Date ? String.Empty : temp.ToShortDateString();
                    LocalTime = temp.ToShortTimeString();
                }
                else
                {
                    ModelLocalDateValue = temp.ToShortDateString();
                    LocalDate = temp.ToShortDateString();
                    ModelLocalTimeValue = temp.ToShortTimeString();
                    LocalTime = temp.ToShortTimeString();
                }
            }
            else
            {
                LocalDate = string.Empty;
                LocalTime = string.Empty;
                ModelLocalDateValue = string.Empty;
                ModelLocalTimeValue = string.Empty;
            }
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates 
        /// whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: 
        /// Value Meaning Less than zero This instance is less than <paramref name="obj"/>. Zero 
        /// This instance is equal to <paramref name="obj"/>. Greater than zero This instance is greater than <paramref name="obj"/>. 
        /// </returns>
        /// <param name="obj">An object to compare with this instance. </param><exception cref="T:System.ArgumentException"><paramref name="obj"/> is not the same type as this instance. </exception><filterpriority>2</filterpriority>
        public int CompareTo(object obj)
        {
            var other = obj as UiDateTimeModel;
            if (other != null)
            {
                if (this.DateTimeUtcValue == other.DateTimeUtcValue)
                {
                    return 0;
                }
                else if (this.DateTimeUtcValue < other.DateTimeUtcValue)
                {
                    return -1;
                }
            }
            return 1;
        }
    }
}