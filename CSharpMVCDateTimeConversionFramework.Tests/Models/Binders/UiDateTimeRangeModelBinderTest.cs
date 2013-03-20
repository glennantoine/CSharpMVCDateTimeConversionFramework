using System;
using System.Web.Mvc;
using CSharpMVCDateTimeConversionFramework.Models;
using CSharpMVCDateTimeConversionFramework.Models.Binders;
using CSharpMVCDateTimeConversionFramework.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpMVCDateTimeConversionFramework.Tests.Models.Binders
{
    [TestClass]
    public class UiDateTimeRangeModelBinderTest
    {

        [TestMethod]
        public void EndDateTimeDateTimeUtcValueIsSetCorrectlyFromStartDateTimeLocalDateLocalTimeValues()
        {
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
            var startDateTimeLocalDate = "01/16/2012";
            var startDateTimeLocalTime = "1:00";
            var startDateTimeDateTimeLocalValue = startDateTimeLocalDate + " " + startDateTimeLocalTime;

            var endDateTimeLocalDate = "01/16/2012";
            var endDateTimeLocalTime = "3:00";
            var endDateTimeDateTimeLocalValue = endDateTimeLocalDate + " " + endDateTimeLocalTime;

            var form = new FormCollection
                           {
                               {"test.StartDateTime.LocalDate", startDateTimeLocalDate},
                               {"test.StartDateTime.LocalTime", startDateTimeLocalTime},
                               {"test.EndDateTime.LocalTime", endDateTimeLocalTime},
                               {"test.TimeZoneName", timeZoneId}
                           };
            var result = BindModelByFormCollection(form);

            Assert.AreEqual(DateTime.Parse(startDateTimeDateTimeLocalValue), result.StartDateTime.DateTimeLocalValue);
            Assert.AreEqual(DateTime.Parse(startDateTimeDateTimeLocalValue).ToUniversalTime(timeZoneId), result.StartDateTime.DateTimeUtcValue);

            Assert.AreEqual(DateTime.Parse(endDateTimeDateTimeLocalValue), result.EndDateTime.DateTimeLocalValue);
            Assert.AreEqual(DateTime.Parse(endDateTimeDateTimeLocalValue).ToUniversalTime(timeZoneId), result.EndDateTime.DateTimeUtcValue);
        }


        [TestMethod]
        public void StartDateTimeDateTimeUtcValueAndEndDateTimeDateTimeUtcValueSetCorrectlyFromLocalDateLocalTime()
        {
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
            var startDateTimeLocalDate = "01/16/2012";
            var startDateTimeNoSetTime = "true,false";

            var form = new FormCollection
                           {
                               {"test.StartDateTime.LocalDate", startDateTimeLocalDate},
                               {"test.StartDateTime.NoSetTime", startDateTimeNoSetTime},
                               {"test.TimeZoneName", timeZoneId}
                           };
            var result = BindModelByFormCollection(form);

            //StartDateTime
            Assert.AreEqual(DateTime.Parse(startDateTimeLocalDate), result.StartDateTime.DateTimeLocalValue);
            Assert.AreEqual(DateTime.Parse(startDateTimeLocalDate).ToUniversalTime(timeZoneId), result.StartDateTime.DateTimeUtcValue);
            Assert.AreEqual(true, result.StartDateTime.NoSetTime);
            Assert.IsTrue(result.StartDateTime.ImplicitlySet);

            //EndDateTime
            Assert.IsNull(result.EndDateTime.DateTimeLocalValue);
            Assert.IsNull(result.EndDateTime.DateTimeUtcValue);
            Assert.AreEqual(string.Empty, result.EndDateTime.LocalDateTime);
            Assert.AreEqual(string.Empty, result.EndDateTime.LocalDate);
            Assert.AreEqual(string.Empty, result.EndDateTime.LocalTime);
            Assert.AreEqual(string.Empty, result.EndDateTime.LocalDateTimeAbreviatedMonthName);
            Assert.AreEqual(string.Empty, result.EndDateTime.LocalDateTimeDayOfMonth);
            Assert.AreEqual(string.Empty, result.EndDateTime.LocalDateTimeDayWithFullDate);
            Assert.IsFalse(result.EndDateTime.ImplicitlySet);


        }

        [TestMethod]
        public void StartDateTimeDateTimeUtcValueAndEndDateTimeDateTimeUtcValueSetCorrectlyFromDateTimeLocalValues()
        {
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
            var startDateTimeDateTimeLocalValue = "01/16/2012 3:00 PM";
            var endDateTimeDateTimeLocalValue = "01/20/2012 8:00 PM";

            var form = new FormCollection
                           {
                               {"test.StartDateTime.DateTimeLocalValue", startDateTimeDateTimeLocalValue},
                               {"test.EndDateTime.DateTimeLocalValue", endDateTimeDateTimeLocalValue},
                               {"test.TimeZoneName", timeZoneId}
                           };
            var result = BindModelByFormCollection(form);

            //StartDateTime
            Assert.AreEqual(DateTime.Parse(startDateTimeDateTimeLocalValue), result.StartDateTime.DateTimeLocalValue);
            Assert.AreEqual(DateTime.Parse(startDateTimeDateTimeLocalValue).ToUniversalTime(timeZoneId), result.StartDateTime.DateTimeUtcValue);
            Assert.IsFalse(result.StartDateTime.ImplicitlySet);

            //EndDateTime
            Assert.AreEqual(DateTime.Parse(endDateTimeDateTimeLocalValue), result.EndDateTime.DateTimeLocalValue);
            Assert.AreEqual(DateTime.Parse(endDateTimeDateTimeLocalValue).ToUniversalTime(timeZoneId), result.EndDateTime.DateTimeUtcValue);
            Assert.IsFalse(result.EndDateTime.ImplicitlySet);
        }

        [TestMethod]
        public void StartDateTimeDateTimeUtcValueIsNullWhenNotSetByPost()
        {
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
            var startDateTimeDateTimeLocalValue = string.Empty;
            var endDateTimeDateTimeLocalValue = "1/20/2012 8:00 PM";

            var form = new FormCollection
                           {
                               {"test.StartDateTime.DateTimeLocalValue", startDateTimeDateTimeLocalValue},
                               {"test.EndDateTime.DateTimeLocalValue", endDateTimeDateTimeLocalValue},
                               {"test.TimeZoneName", timeZoneId}
                           };

            var result = BindModelByFormCollection(form);

            //StartDateTime
            Assert.IsNull(result.StartDateTime.DateTimeLocalValue);
            Assert.IsNull(result.StartDateTime.DateTimeUtcValue);
            Assert.AreEqual(string.Empty, result.StartDateTime.LocalDateTime);
            Assert.AreEqual(string.Empty, result.StartDateTime.LocalDate);
            Assert.AreEqual(string.Empty, result.StartDateTime.LocalTime);
            Assert.AreEqual(string.Empty, result.StartDateTime.LocalDateTimeAbreviatedMonthName);
            Assert.AreEqual(string.Empty, result.StartDateTime.LocalDateTimeDayOfMonth);
            Assert.AreEqual(string.Empty, result.StartDateTime.LocalDateTimeDayWithFullDate);
            Assert.IsFalse(result.StartDateTime.ImplicitlySet);

            //EndDateTime
            Assert.AreEqual(DateTime.Parse(endDateTimeDateTimeLocalValue), result.EndDateTime.DateTimeLocalValue);
            Assert.AreEqual(DateTime.Parse(endDateTimeDateTimeLocalValue).ToUniversalTime(timeZoneId), result.EndDateTime.DateTimeUtcValue);
            Assert.AreEqual(DateTime.Parse(endDateTimeDateTimeLocalValue).ToShortDateString(), result.EndDateTime.LocalDate);
            Assert.AreEqual(DateTime.Parse(endDateTimeDateTimeLocalValue).ToShortTimeString(), result.EndDateTime.LocalTime);
            Assert.AreEqual(endDateTimeDateTimeLocalValue, result.EndDateTime.LocalDateTime);
            Assert.IsFalse(result.EndDateTime.ImplicitlySet);
        }


        [TestMethod]
        public void EndDateTimeDateTimeUtcValueIsNullWhenNotSetByPost()
        {
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
            var startDateTimeDateTimeLocalValue = "1/20/2012 8:00 PM";
            var endDateTimeDateTimeLocalValue = string.Empty;

            var form = new FormCollection
                           {
                               {"test.StartDateTime.DateTimeLocalValue", startDateTimeDateTimeLocalValue},
                               {"test.EndDateTime.DateTimeLocalValue", endDateTimeDateTimeLocalValue},
                               {"test.TimeZoneName", timeZoneId}
                           };

            var result = BindModelByFormCollection(form);

            //StartDateTime
            Assert.AreEqual(DateTime.Parse(startDateTimeDateTimeLocalValue), result.StartDateTime.DateTimeLocalValue);
            Assert.AreEqual(DateTime.Parse(startDateTimeDateTimeLocalValue).ToUniversalTime(timeZoneId), result.StartDateTime.DateTimeUtcValue);
            Assert.AreEqual(DateTime.Parse(startDateTimeDateTimeLocalValue).ToShortDateString(), result.StartDateTime.LocalDate);
            Assert.AreEqual(DateTime.Parse(startDateTimeDateTimeLocalValue).ToShortTimeString(), result.StartDateTime.LocalTime);
            Assert.AreEqual(startDateTimeDateTimeLocalValue, result.StartDateTime.LocalDateTime);
            Assert.IsFalse(result.StartDateTime.ImplicitlySet);

            //EndDateTime
            Assert.IsNull(result.EndDateTime.DateTimeLocalValue);
            Assert.IsNull(result.EndDateTime.DateTimeUtcValue);
            Assert.AreEqual(string.Empty, result.EndDateTime.LocalDateTime);
            Assert.AreEqual(string.Empty, result.EndDateTime.LocalDate);
            Assert.AreEqual(string.Empty, result.EndDateTime.LocalTime);
            Assert.AreEqual(string.Empty, result.EndDateTime.LocalDateTimeAbreviatedMonthName);
            Assert.AreEqual(string.Empty, result.EndDateTime.LocalDateTimeDayOfMonth);
            Assert.AreEqual(string.Empty, result.EndDateTime.LocalDateTimeDayWithFullDate);
            Assert.IsFalse(result.EndDateTime.ImplicitlySet);
        }


        [TestMethod]
        public void StartDateTimeDateTimeUtcValueAndEndDateTimeDateTimeUtcValueAreNullWhenNotSetByPost()
        {
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
            var startDateTimeDateTimeLocalValue = string.Empty;
            var endDateTimeDateTimeLocalValue = string.Empty;

            var form = new FormCollection
                           {
                               {"test.StartDateTime.DateTimeLocalValue", startDateTimeDateTimeLocalValue},
                               {"test.EndDateTime.DateTimeLocalValue", endDateTimeDateTimeLocalValue},
                               {"test.TimeZoneName", timeZoneId}
                           };

            var result = BindModelByFormCollection(form);

            //StartDateTime
            Assert.IsNull(result.StartDateTime.DateTimeLocalValue);
            Assert.IsNull(result.StartDateTime.DateTimeUtcValue);
            Assert.AreEqual(string.Empty, result.StartDateTime.LocalDateTime);
            Assert.AreEqual(string.Empty, result.StartDateTime.LocalDate);
            Assert.AreEqual(string.Empty, result.StartDateTime.LocalTime);
            Assert.AreEqual(string.Empty, result.StartDateTime.LocalDateTimeAbreviatedMonthName);
            Assert.AreEqual(string.Empty, result.StartDateTime.LocalDateTimeDayOfMonth);
            Assert.AreEqual(string.Empty, result.StartDateTime.LocalDateTimeDayWithFullDate);
            Assert.IsFalse(result.StartDateTime.ImplicitlySet);

            //EndDateTime
            Assert.IsNull(result.EndDateTime.DateTimeLocalValue);
            Assert.IsNull(result.EndDateTime.DateTimeUtcValue);
            Assert.AreEqual(string.Empty, result.EndDateTime.LocalDateTime);
            Assert.AreEqual(string.Empty, result.EndDateTime.LocalDate);
            Assert.AreEqual(string.Empty, result.EndDateTime.LocalTime);
            Assert.AreEqual(string.Empty, result.EndDateTime.LocalDateTimeAbreviatedMonthName);
            Assert.AreEqual(string.Empty, result.EndDateTime.LocalDateTimeDayOfMonth);
            Assert.AreEqual(string.Empty, result.EndDateTime.LocalDateTimeDayWithFullDate);
            Assert.IsFalse(result.EndDateTime.ImplicitlySet);
        }



        private UiDateTimeRangeModel BindModelByFormCollection(FormCollection form)
        {

            var bindingContext = new ModelBindingContext() {ModelName = "test", ValueProvider = form.ToValueProvider()};
            var b = new UiDateTimeRangeModelBinder()
                        {
                            StartLocalDate = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.StartDateTime.LocalDate),
                            StartLocalTime = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.StartDateTime.LocalTime),
                            EndLocalDate = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.EndDateTime.LocalDate),
                            EndLocalTime = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.EndDateTime.LocalTime),
                            TimeZoneName = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.TimeZoneName)
                        };
            return (UiDateTimeRangeModel) b.BindModel(null, bindingContext);
        }


    }
}