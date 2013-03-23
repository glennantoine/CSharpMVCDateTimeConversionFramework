using System;
using System.Data.SqlTypes;
using System.Globalization;
using System.Web.Mvc;
using CSharpMVCDateTimeConversionFramework.Utilities;

namespace CSharpMVCDateTimeConversionFramework.Models.Binders
{
    public class UiDateTimeModelBinder : IModelBinder
    {
        public UiDateTimeModelBinder()
        {
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
			var implicitlySet = false;
			
            if (bindingContext == null)
                throw new ArgumentNullException("bindingContext");

            //******************************************************
            //The TimeZone is required for our implementation 
            //******************************************************
            var tzAttempt = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "." + TimeZoneFieldName);
            if (tzAttempt == null)
            {
                throw new NullReferenceException("tzAttempt");
            }
            var timeZoneAttempt = (string) tzAttempt.ConvertTo(typeof (string));

            //******************************************************
            //If they just want a full DateTime the handle that here 
            //and return model
            //******************************************************
            DateTime? dateTimeAttempt = GetA<DateTime>(bindingContext, StaticReflection.GetMemberName<UiDateTimeModel>(x => x.DateTimeLocalValue));

            if (dateTimeAttempt != null)
                return new UiDateTimeModel(timeZoneAttempt) {DateTimeLocalValue = DateTime.Parse(dateTimeAttempt.Value.ToString(CultureInfo.InvariantCulture))};

            //******************************************************
            //If they haven't set Month,Day,Year OR Date, set "date" 
            //and get ready for an attempt
            //******************************************************
            if (!MonthDayYearFieldNameSet && !DateFieldNameSet)
            {
                DateFieldName = StaticReflection.GetMemberName<UiDateTimeModel>(x => x.LocalDate);
            }

            //******************************************************
            //If they haven't set Hour, Minute, Second OR Time, set 
            //"time" and get ready for an attempt
            //******************************************************
            if (!HourMinuteSecondFieldNameSet && !TimeFieldNameSet)
            {
                TimeFieldName = StaticReflection.GetMemberName<UiDateTimeModel>(x => x.LocalTime);
            }

            //******************************************************
            //If the TimeZoneFieldName has not be set do so here
            //******************************************************
            if (!TimeZoneFieldNameSet)
            {
                TimeZoneFieldName = StaticReflection.GetMemberName<UiDateTimeModel>(x => x.TimeZoneName);
            }

            //******************************************************
            //If the NoSetTimeFieldName has not be set do so here
            //******************************************************
            if (!NoSetTimeFieldNameSet) 
            {
                NoSetTimeFieldName = StaticReflection.GetMemberName<UiDateTimeModel>(x => x.NoSetTime);
            }

            //******************************************************
            //Based on a number of checks we may have to implicitly
            //set the dateAttempt
            //******************************************************
            DateTime? dateAttempt = null;
            if (bindingContext.ModelMetadata != null && bindingContext.ModelMetadata.ContainerType.Name == typeof (UiDateTimeRangeModel).Name &&
                bindingContext.ModelMetadata.PropertyName == StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.EndDateTime) &&
                !bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName + "." + StaticReflection.GetMemberName<UiDateTimeModel>(x => x.LocalDate)))
            {
                dateAttempt = DateTime.Now;
                implicitlySet = true;
            }

            //Did they want the Date *and* Time?
            if (dateAttempt == null)
            {
                dateAttempt = GetA<DateTime>(bindingContext, DateFieldName);
            }

            //******************************************************
            //Check to see if they are allowing the user to specify
            //that they do not want to set a specific time
            //******************************************************
            var noSetTime = false;
            DateTime? timeAttempt = null;
            if (!string.IsNullOrWhiteSpace(NoSetTimeFieldName)) 
            {
                var noSetTimeAttempt = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "." + NoSetTimeFieldName);
                if (noSetTimeAttempt != null && bool.TryParse(noSetTimeAttempt.AttemptedValue.Split(',')[0], out noSetTime) && noSetTime) 
                {
                    timeAttempt = DateTime.MinValue;
                    implicitlySet = true;
                }
            }

            //******************************************************
            //If NoSetTime has been checked check the Time Field
            //******************************************************
            if (!noSetTime)
            {
               timeAttempt = GetA<DateTime>(bindingContext, TimeFieldName);
            }
            
            if (dateAttempt.HasValue || timeAttempt.HasValue)
            {
                dateAttempt = dateAttempt ?? SqlDateTime.MinValue.Value.Date;
                timeAttempt = timeAttempt ?? SqlDateTime.MinValue.Value;
            }

            //*****************************************************************************
            // The following three sections are added for a more rounded/complete solution.
            // However, this sections are not currently being used in the application.
            //*****************************************************************************

            //Time in parts
            if (HourMinuteSecondFieldNameSet && !MonthDayYearFieldNameSet)
            {
                timeAttempt = new DateTime(SqlDateTime.MinValue.Value.Year,
                                           SqlDateTime.MinValue.Value.Month,
                                           SqlDateTime.MinValue.Value.Day,
                                           GetA<int>(bindingContext, HourFieldName).Value,
                                           GetA<int>(bindingContext, MinuteFieldName).Value,
                                           GetA<int>(bindingContext, SecondFieldName).Value);
            }

            //Date in parts
            if (MonthDayYearFieldNameSet && !HourMinuteSecondFieldNameSet)
            {
                dateAttempt = new DateTime(GetA<int>(bindingContext, YearFieldName).Value,
                                           GetA<int>(bindingContext, MonthFieldName).Value,
                                           GetA<int>(bindingContext, DayFieldName).Value,
                                           SqlDateTime.MinValue.Value.Hour,
                                           SqlDateTime.MinValue.Value.Minute,
                                           SqlDateTime.MinValue.Value.Second);
            }

            //Date and Time in parts
            if (MonthDayYearFieldNameSet && HourMinuteSecondFieldNameSet)
            {
                dateAttempt = new DateTime(GetA<int>(bindingContext, YearFieldName).Value,
                                           GetA<int>(bindingContext, MonthFieldName).Value,
                                           GetA<int>(bindingContext, DayFieldName).Value,
                                           SqlDateTime.MinValue.Value.Hour,
                                           SqlDateTime.MinValue.Value.Minute,
                                           SqlDateTime.MinValue.Value.Second);

                timeAttempt = new DateTime(SqlDateTime.MinValue.Value.Year,
                                           SqlDateTime.MinValue.Value.Month,
                                           SqlDateTime.MinValue.Value.Day,
                                           GetA<int>(bindingContext, HourFieldName).Value,
                                           GetA<int>(bindingContext, MinuteFieldName).Value,
                                           GetA<int>(bindingContext, SecondFieldName).Value);
            }

            DateTime? dateTime;
            if (dateAttempt.HasValue && timeAttempt.HasValue)
            {
                dateTime = new DateTime(dateAttempt.Value.Year,
                                        dateAttempt.Value.Month,
                                        dateAttempt.Value.Day,
                                        timeAttempt.Value.Hour,
                                        timeAttempt.Value.Minute,
                                        timeAttempt.Value.Second);
            }
            else
            {
                dateTime = null;
            }


            if (!string.IsNullOrWhiteSpace(timeZoneAttempt))
            {
                return new UiDateTimeModel(timeZoneAttempt) {DateTimeLocalValue = dateTime, NoSetTime = noSetTime, ImplicitlySet = implicitlySet};
            }

            return null;
        }

        private T? GetA<T>(ModelBindingContext bindingContext, string key) where T : struct
        {
            if (String.IsNullOrEmpty(key))
                return null;

            ValueProviderResult valueResult;
            string modelName;

            //Try it with the prefix...
            if (bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName))
            {
                valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "." + key);
                modelName = bindingContext.ModelName + "." + key;
            }
            else //Didn't work? Try without the prefix if needed...
            {
                valueResult = bindingContext.ValueProvider.GetValue(key);
                modelName = key;
            }

            if (valueResult == null)
            {
                return null;
            }

            // Add the value to the model state 
            // when redisplaying the form with a not convertible value
            bindingContext.ModelState.SetModelValue(modelName, valueResult);
            try 
            {
                return (T?)valueResult.ConvertTo(typeof(T));
            } 
            catch (Exception ex)
            {
                bindingContext.ModelState.AddModelError(modelName, ex);
                return null;
            }
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
		public string NoSetTimeFieldName { get; set; }
		
        private bool NoSetTimeFieldNameSet { get { return !String.IsNullOrEmpty(NoSetTimeFieldName); } }
        private bool TimeZoneFieldNameSet
        {
            get { return !String.IsNullOrEmpty(TimeZoneFieldName); }
        }

        private bool DateFieldNameSet
        {
            get { return !String.IsNullOrEmpty(DateFieldName); }
        }

        private bool MonthDayYearFieldNameSet
        {
            get { return !(String.IsNullOrEmpty(MonthFieldName) && String.IsNullOrEmpty(DayFieldName) && String.IsNullOrEmpty(YearFieldName)); }
        }

        private bool TimeFieldNameSet
        {
            get { return !String.IsNullOrEmpty(TimeFieldName); }
        }

        private bool HourMinuteSecondFieldNameSet
        {
            get { return !(String.IsNullOrEmpty(HourFieldName) && String.IsNullOrEmpty(MinuteFieldName) && String.IsNullOrEmpty(SecondFieldName)); }
        }
    }

}