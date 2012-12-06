using System;
using System.Web.Mvc;
using CustomModelBindingWithDateTime.Utilities;
using CustomModelBindingWithDateTime.Resources;

namespace CustomModelBindingWithDateTime.Models.Binders 
{
    public class UiDateTimeModelBinder : IModelBinder 
    {
        public UiDateTimeModelBinder() { }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) 
        {
            if (bindingContext == null) 
            {
                throw new ArgumentNullException(ValidationResource.BindingContext);
            }

            //Maybe we're lucky and they just want a DateTime the regular way.
            DateTime? dateTimeAttempt = GetA<DateTime>(bindingContext, StaticReflection.GetMemberName<UiDateTimeModel>(x => x.DateTimeLocalValue));
            
            if (dateTimeAttempt != null) 
            {
                return dateTimeAttempt.Value;
            }

            string timeZoneAttempt = string.Empty;

            //If they haven't set Month,Day,Year OR Date, set "date" and get ready for an attempt
            if (!this.MonthDayYearFieldNameSet && !this.DateFieldNameSet) 
            {
                this.DateFieldName = StaticReflection.GetMemberName<UiDateTimeModel>(x => x.LocalDate);
            }

            //If they haven't set Hour, Minute, Second OR Time, set "time" and get ready for an attempt
            if (!this.HourMinuteSecondFieldNameSet && !this.TimeFieldNameSet) 
            {
                this.TimeFieldName = StaticReflection.GetMemberName<UiDateTimeModel>(x => x.LocalTime);
            }

            if (!this.TimeZoneFieldNameSet)
            {
                this.TimeZoneFieldName = StaticReflection.GetMemberName<UiDateTimeModel>(x => x.TimeZoneName);
            }

            DateTime? dateAttempt = null;
            if (bindingContext.ModelMetadata != null && bindingContext.ModelMetadata.ContainerType.Name == typeof(UiDateTimeRangeModel).Name &&
                bindingContext.ModelMetadata.PropertyName == StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.EndDateTime) &&
                !bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName + "." + StaticReflection.GetMemberName<UiDateTimeModel>(x => x.LocalDate))) 
            {
                var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
                dateAttempt = DateTime.Now;
            }

            //Did they want the Date *and* Time?
            if (dateAttempt == null)
            {
                dateAttempt = GetA<DateTime>(bindingContext, this.DateFieldName);
            }
            DateTime? timeAttempt = GetA<DateTime>(bindingContext, this.TimeFieldName);

            dateAttempt = dateAttempt ?? DateTime.MinValue;
            timeAttempt = timeAttempt ?? DateTime.MinValue;

            var tzAttempt = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "." + this.TimeZoneFieldName);
            if (tzAttempt == null)
            {
                throw new ApplicationException(this.TimeZoneFieldName + ValidationResource.UiDateTimeModelTimeZoneRequired);
            }
            timeZoneAttempt = (string)tzAttempt.ConvertTo(typeof(string));

            //Time in parts
            if (this.HourMinuteSecondFieldNameSet && !this.MonthDayYearFieldNameSet) 
            {
                timeAttempt = new DateTime( DateTime.MinValue.Year, 
                                            DateTime.MinValue.Month, 
                                            DateTime.MinValue.Day,
                                            GetA<int>(bindingContext, this.HourFieldName).Value,
                                            GetA<int>(bindingContext, this.MinuteFieldName).Value,
                                            GetA<int>(bindingContext, this.SecondFieldName).Value);
            }

            //Date in parts
            if (this.MonthDayYearFieldNameSet && !this.HourMinuteSecondFieldNameSet) 
            {
                dateAttempt = new DateTime(GetA<int>(bindingContext, this.YearFieldName).Value,
                                            GetA<int>(bindingContext, this.MonthFieldName).Value,
                                            GetA<int>(bindingContext, this.DayFieldName).Value,
                                            DateTime.MinValue.Hour, 
                                            DateTime.MinValue.Minute, 
                                            DateTime.MinValue.Second);
            }

            //Date and Time in parts
            if (this.MonthDayYearFieldNameSet && this.HourMinuteSecondFieldNameSet) 
            {
                dateAttempt = new DateTime(GetA<int>(bindingContext, this.YearFieldName).Value,
                                            GetA<int>(bindingContext, this.MonthFieldName).Value,
                                            GetA<int>(bindingContext, this.DayFieldName).Value,
                                            DateTime.MinValue.Hour,
                                            DateTime.MinValue.Minute,
                                            DateTime.MinValue.Second);

                timeAttempt = new DateTime(DateTime.MinValue.Year,
                                            DateTime.MinValue.Month,
                                            DateTime.MinValue.Day,
                                            GetA<int>(bindingContext, this.HourFieldName).Value,
                                            GetA<int>(bindingContext, this.MinuteFieldName).Value,
                                            GetA<int>(bindingContext, this.SecondFieldName).Value);
            }


            var dateTime = new DateTime(dateAttempt.Value.Year,
                                        dateAttempt.Value.Month,
                                        dateAttempt.Value.Day,
                                        timeAttempt.Value.Hour,
                                        timeAttempt.Value.Minute,
                                        timeAttempt.Value.Second);

            if (!string.IsNullOrWhiteSpace(timeZoneAttempt))
            {
                return new UiDateTimeModel(timeZoneAttempt)
                            {
                                DateTimeLocalValue = dateTime
                            };
            }

            return null;
        }

        private Nullable<T> GetA<T>(ModelBindingContext bindingContext, string key) where T : struct
        {
            if (String.IsNullOrEmpty(key))
                return null;

            ValueProviderResult valueResult;

            //Try it with the prefix...
            if (bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName))
            {
                valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "." + key);
            }
                //Didn't work? Try without the prefix if needed...
            else
            {
                valueResult = bindingContext.ValueProvider.GetValue(key);
            }

            if (valueResult == null)
            {
                return null;
            }

            return (Nullable<T>) valueResult.ConvertTo(typeof (T));
        }

        public string DateFieldName { get; set; }
        public string TimeFieldName { get; set; }

        public string MonthFieldName { get; set; }
        public string DayFieldName { get; set; }
        public string YearFieldName { get; set; }

        public string HourFieldName { get; set; }
        public string MinuteFieldName { get; set; }
        public string SecondFieldName { get; set; }
        public string TimeZoneFieldName { get; set; }

        private bool TimeZoneFieldNameSet { get { return !String.IsNullOrEmpty(TimeZoneFieldName); } }

        private bool DateFieldNameSet { get { return !String.IsNullOrEmpty(DateFieldName); } }
        private bool MonthDayYearFieldNameSet { get { return !(String.IsNullOrEmpty(MonthFieldName) && String.IsNullOrEmpty(DayFieldName) && String.IsNullOrEmpty(YearFieldName)); } }

        private bool TimeFieldNameSet { get { return !String.IsNullOrEmpty(TimeFieldName); } }
        private bool HourMinuteSecondFieldNameSet { get { return !(String.IsNullOrEmpty(HourFieldName) && String.IsNullOrEmpty(MinuteFieldName) && String.IsNullOrEmpty(SecondFieldName)); } }
    }

}