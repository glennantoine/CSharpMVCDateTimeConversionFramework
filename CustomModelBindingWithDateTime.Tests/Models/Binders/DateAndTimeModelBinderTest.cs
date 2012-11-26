using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomModelBindingWithDateTime.Models;
using CustomModelBindingWithDateTime.Models.Binders;
using CustomModelBindingWithDateTime.Utils;

namespace CustomModelBindingWithDateTime.Tests.Models.Binders 
{
    [TestClass]
    public class DateAndTimeModelBinderTest 
    {

        [TestMethod]
        public void DateTimeUtcValueIsSetCorrectlyFromDateTimeLocalValue()
        {
            var timeZoneId = "Eastern Standard Time";
            var dict = new ValueProviderDictionary(null) 
                                                    {
                                                        { "test.LocalDate", new ValueProviderResult("01/16/2012","01/16/2012",null) },
                                                        { "test.LocalTime", new ValueProviderResult("1:00", "1:00", null) },
                                                        { "test.TimeZoneName", new ValueProviderResult("Eastern Standard Time", "Eastern Standard Time", null) }
                                                     };

            var bindingContext = new ModelBindingContext() { ModelName = "test", ValueProvider = dict };

            var b = new UiDateTimeModelBinder() {
                                                    DateFieldName = "LocalDate",
                                                    TimeFieldName = "LocalTime",
                                                    TimeZoneFieldName = "TimeZoneName"
                                                };

            var result = (UiDateTimeModel)b.BindModel(null, bindingContext);
            Assert.AreEqual(DateTime.Parse("01/16/2012 01:00:00").ToUniversalTime(timeZoneId), result.DateTimeUtcValue);
        }

        [TestMethod]
        public void DateTimeUtcValueAndDateTimeLocalValueSetCorrectlyFromLocalDate() 
        {
            var timeZoneId = "Eastern Standard Time";
            var dict = new ValueProviderDictionary(null) 
                                                    {
                                                        { "test.LocalDate", new ValueProviderResult("01/16/2012","01/16/2012",null) },
                                                        { "test.TimeZoneName", new ValueProviderResult("Eastern Standard Time", "Eastern Standard Time", null) }
                                                     };

            var bindingContext = new ModelBindingContext() { ModelName = "test", ValueProvider = dict };

            var b = new UiDateTimeModelBinder() {
                                                    DateFieldName = "LocalDate",
                                                    TimeFieldName = "LocalTime",
                                                    TimeZoneFieldName = "TimeZoneName"
                                                };

            var result = (UiDateTimeModel)b.BindModel(null, bindingContext);
            Assert.AreEqual(DateTime.Parse("01/16/2012 00:00:00").ToUniversalTime(timeZoneId), result.DateTimeUtcValue);
            Assert.AreEqual(DateTime.Parse("01/16/2012 00:00:00"), result.DateTimeLocalValue);
        }

        [TestMethod]
        public void DateTimeUtcValueAndDateTimeLocalValueSetCorrectlyFromLocalTime() 
        {
            var timeZoneId = "Eastern Standard Time";
            var dict = new ValueProviderDictionary(null) 
                                                    {
                                                        { "test.LocalTime", new ValueProviderResult("1:00", "1:00", null) },
                                                        { "test.TimeZoneName", new ValueProviderResult("Eastern Standard Time", "Eastern Standard Time", null) }
                                                     };

            var bindingContext = new ModelBindingContext() { ModelName = "test", ValueProvider = dict };

            var b = new UiDateTimeModelBinder() {
                                                    DateFieldName = "LocalDate",
                                                    TimeFieldName = "LocalTime",
                                                    TimeZoneFieldName = "TimeZoneName"
                                                };

            var result = (UiDateTimeModel)b.BindModel(null, bindingContext);
            Assert.AreEqual(DateTime.Parse("01/01/0001 01:00:00").ToUniversalTime(timeZoneId), result.DateTimeUtcValue);
            Assert.AreEqual(DateTime.Parse("01/01/0001 01:00:00"), result.DateTimeLocalValue);
        }

        [TestMethod]
        public void DateCanBePulledViaProvidedMonthDayYear() 
        {
            var timeZoneId = "Eastern Standard Time";
            var dict = new ValueProviderDictionary(null) {
                                                        { "test.month", new ValueProviderResult("2","2",null) },
                                                        { "test.day", new ValueProviderResult("12", "12", null) },
                                                        { "test.year", new ValueProviderResult("1964", "1964", null) },
                                                        { "test.TimeZoneName", new ValueProviderResult("Eastern Standard Time", "Eastern Standard Time", null) }
                                                    };

            var bindingContext = new ModelBindingContext() { ModelName = "test", ValueProvider = dict };

            var b = new UiDateTimeModelBinder() { MonthFieldName = "month", DayFieldName = "day", YearFieldName = "year" };

            var result = (UiDateTimeModel)b.BindModel(null, bindingContext);
            Assert.AreEqual(DateTime.Parse("1964-02-12 12:00:00 am"), result.DateTimeLocalValue);
            Assert.AreEqual(DateTime.Parse("1964-02-12 12:00:00 am").ToUniversalTime(timeZoneId), result.DateTimeUtcValue);
        }

        [TestMethod]
        public void DateTimeCanBePulledViaProvidedMonthDayYearHourMinuteSecondAlternateNames()
        {
            var dict = new ValueProviderDictionary(null) 
                                                    {
                                                        { "test.month", new ValueProviderResult("2","2",null) },
                                                        { "test.day", new ValueProviderResult("12", "12", null) },
                                                        { "test.year", new ValueProviderResult("1964", "1964", null) },
                                                        { "test.hour", new ValueProviderResult("13","13",null) },
                                                        { "test.minute", new ValueProviderResult("44", "44", null) },
                                                        { "test.second", new ValueProviderResult("00", "00", null) },
                                                        { "test.TimeZoneName", new ValueProviderResult("Eastern Standard Time", "Eastern Standard Time", null) }
                                                     };

            var bindingContext = new ModelBindingContext() { ModelName = "test", ValueProvider = dict };

            var b = new UiDateTimeModelBinder() { MonthFieldName = "month", DayFieldName = "day", YearFieldName = "year", HourFieldName = "hour", MinuteFieldName = "minute", SecondFieldName = "second" };

            var result = (UiDateTimeModel)b.BindModel(null, bindingContext);
            Assert.AreEqual(DateTime.Parse("1964-02-12 13:44:00"), result.DateTimeLocalValue);            
        }

    }
}
