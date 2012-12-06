using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomModelBindingWithDateTime.Models;
using CustomModelBindingWithDateTime.Models.Binders;
using CustomModelBindingWithDateTime.Utilities;

namespace CustomModelBindingWithDateTime.Tests.Models.Binders 
{
    [TestClass]
    public class UiDateTimeModelBinderTest 
    {

        [TestMethod]
        public void DateTimeUtcValueIsSetCorrectlyFromDateTimeLocalValue()
        {
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
            var form = new FormCollection
                                        {
                                            { "test.LocalDate","01/16/2012"},
                                            { "test.LocalTime", "1:00"},
                                            { "test.TimeZoneName", "Eastern Standard Time"}
                                        };
            var bindingContext = new ModelBindingContext() { ModelName = "test", ValueProvider = form.ToValueProvider() };

            var b = new UiDateTimeModelBinder() {
                                                    DateFieldName = StaticReflection.GetMemberName<UiDateTimeModel>(x => x.LocalDate),
                                                    TimeFieldName = StaticReflection.GetMemberName<UiDateTimeModel>(x => x.LocalTime),
                                                    TimeZoneFieldName = StaticReflection.GetMemberName<UiDateTimeModel>(x => x.TimeZoneName)
                                                };

            var result = (UiDateTimeModel)b.BindModel(null, bindingContext);
            Assert.AreEqual(DateTime.Parse("01/16/2012 01:00:00").ToUniversalTime(timeZoneId), result.DateTimeUtcValue);
        }

        [TestMethod]
        public void DateTimeUtcValueAndDateTimeLocalValueSetCorrectlyFromLocalDate() 
        {
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
            var form = new FormCollection
                                        {
                                            { "test.LocalDate","01/16/2012"},
                                            { "test.TimeZoneName", "Eastern Standard Time"}
                                        };
            var bindingContext = new ModelBindingContext() { ModelName = "test", ValueProvider = form.ToValueProvider() };

            var b = new UiDateTimeModelBinder() {
                                                    DateFieldName = StaticReflection.GetMemberName<UiDateTimeModel>(x => x.LocalDate),
                                                    TimeFieldName = StaticReflection.GetMemberName<UiDateTimeModel>(x => x.LocalTime),
                                                    TimeZoneFieldName = StaticReflection.GetMemberName<UiDateTimeModel>(x => x.TimeZoneName)
                                                };

            var result = (UiDateTimeModel)b.BindModel(null, bindingContext);
            Assert.AreEqual(DateTime.Parse("01/16/2012 00:00:00").ToUniversalTime(timeZoneId), result.DateTimeUtcValue);
            Assert.AreEqual(DateTime.Parse("01/16/2012 00:00:00"), result.DateTimeLocalValue);
        }

        [TestMethod]
        public void DateTimeUtcValueAndDateTimeLocalValueSetCorrectlyFromLocalTime() 
        {
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
            var form = new FormCollection
                                        {
                                            { "test.LocalTime", "1:00"},
                                            { "test.TimeZoneName", "Eastern Standard Time"}
                                        };
            var bindingContext = new ModelBindingContext() { ModelName = "test", ValueProvider = form.ToValueProvider() };

            var b = new UiDateTimeModelBinder() {
                                                    DateFieldName = StaticReflection.GetMemberName<UiDateTimeModel>(x => x.LocalDate),
                                                    TimeFieldName = StaticReflection.GetMemberName<UiDateTimeModel>(x => x.LocalTime),
                                                    TimeZoneFieldName = StaticReflection.GetMemberName<UiDateTimeModel>(x => x.TimeZoneName)
                                                };

            var result = (UiDateTimeModel)b.BindModel(null, bindingContext);
            Assert.AreEqual(DateTime.Parse("01/01/0001 01:00:00").ToUniversalTime(timeZoneId), result.DateTimeUtcValue);
            Assert.AreEqual(DateTime.Parse("01/01/0001 01:00:00"), result.DateTimeLocalValue);
        }


        /// <summary>
        /// The Unit tests beyond this point can be ignored until such time that we have a need to 
        /// implement the associated functionality -
        /// </summary>
        [TestMethod]
        [Ignore]
        public void DateCanBePulledViaProvidedMonthDayYear() 
        {
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
            var form = new FormCollection
                                        {
                                            { "test.month","2"},
                                            { "test.day", "12"},
                                            { "test.year", "1964"},
                                            { "test.TimeZoneName", "Eastern Standard Time"}
                                        };
            var bindingContext = new ModelBindingContext() { ModelName = "test", ValueProvider = form.ToValueProvider() };

            var b = new UiDateTimeModelBinder() { MonthFieldName = "month", DayFieldName = "day", YearFieldName = "year" };

            var result = (UiDateTimeModel)b.BindModel(null, bindingContext);
            Assert.AreEqual(DateTime.Parse("1964-02-12 12:00:00 am"), result.DateTimeLocalValue);
            Assert.AreEqual(DateTime.Parse("1964-02-12 12:00:00 am").ToUniversalTime(timeZoneId), result.DateTimeUtcValue);
        }

        [TestMethod]
        [Ignore]
        public void DateTimeCanBePulledViaProvidedMonthDayYearHourMinuteSecondAlternateNames()
        {
            var form = new FormCollection
                                        {
                                            { "test.month","2"},
                                            { "test.day", "12"},
                                            { "test.year", "1964"},
                                            { "test.hour", "13"},
                                            { "test.minute", "44"},
                                            { "test.second", "00"},
                                            { "test.TimeZoneName", "Eastern Standard Time"}
                                        };
            var bindingContext = new ModelBindingContext() { ModelName = "test", ValueProvider = form.ToValueProvider() };


            var b = new UiDateTimeModelBinder() { MonthFieldName = "month", DayFieldName = "day", YearFieldName = "year", HourFieldName = "hour", MinuteFieldName = "minute", SecondFieldName = "second" };

            var result = (UiDateTimeModel)b.BindModel(null, bindingContext);
            Assert.AreEqual(DateTime.Parse("1964-02-12 13:44:00"), result.DateTimeLocalValue);            
        }

    }
}
