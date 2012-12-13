using System;
using System.Web.Mvc;
using CustomModelBindingWithDateTime.Models;
using CustomModelBindingWithDateTime.Models.Binders;
using CustomModelBindingWithDateTime.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CustomModelBindingWithDateTime.Tests.Models.Binders 
{
    [TestClass]
    public class UiDateTimeRangeModelBinderTest 
    {

        [TestMethod]
        public void EndDateTimeDateTimeUtcValueIsSetCorrectlyFromStartDateTimeLocalDateLocalTimeValues()
        {
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
            var form = new FormCollection
                                        {
                                            { "test.StartDateTime.LocalDate","01/16/2012"},
                                            { "test.StartDateTime.LocalTime", "1:00"},
                                            { "test.EndDateTime.LocalTime", "3:00"},
                                            { "test.TimeZoneName", "Eastern Standard Time"}
                                        };
            var bindingContext = new ModelBindingContext() { ModelName = "test", ValueProvider = form.ToValueProvider() };

            var b = new UiDateTimeRangeModelBinder() {
                                                        StartLocalDate = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.StartDateTime.LocalDate),
                                                        StartLocalTime = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.StartDateTime.LocalTime),
                                                        EndLocalTime = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.EndDateTime.LocalTime),
                                                        TimeZoneName = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.TimeZoneName)
                                                    };

            var result = (UiDateTimeRangeModel)b.BindModel(null, bindingContext);
            Assert.AreEqual(DateTime.Parse("01/16/2012 1:00"), result.StartDateTime.DateTimeLocalValue);
            Assert.AreEqual(DateTime.Parse("01/16/2012 3:00"), result.EndDateTime.DateTimeLocalValue);
            Assert.AreEqual(DateTime.Parse("01/16/2012 1:00").ToUniversalTime(timeZoneId), result.StartDateTime.DateTimeUtcValue);
            Assert.AreEqual(DateTime.Parse("01/16/2012 3:00").ToUniversalTime(timeZoneId), result.EndDateTime.DateTimeUtcValue);
        }


        [TestMethod]
        public void StartDateTimeDateTimeUtcValueAndEndDateTimeDateTimeUtcValueSetCorrectlyFromLocalDateLocalTime() 
        {
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
            var form = new FormCollection
                                        {
                                            { "test.StartDateTime.LocalDate","01/16/2012"},
                                            { "test.StartDateTime.LocalTime", "3:00 PM"},
                                            { "test.EndDateTime.LocalDate","01/20/2012"},
                                            { "test.EndDateTime.LocalTime", "8:00 PM"},
                                            { "test.TimeZoneName", "Eastern Standard Time"}
                                        };
            var bindingContext = new ModelBindingContext() { ModelName = "test", ValueProvider = form.ToValueProvider() };

            var b = new UiDateTimeRangeModelBinder() {
                                                        StartLocalDate = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.StartDateTime.LocalDate),
                                                        StartLocalTime = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.StartDateTime.LocalTime),
                                                        EndLocalDate = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.EndDateTime.LocalDate),
                                                        EndLocalTime = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.EndDateTime.LocalTime),
                                                        TimeZoneName = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.TimeZoneName)
                                                    };

            var result = (UiDateTimeRangeModel)b.BindModel(null, bindingContext);
            Assert.AreEqual(DateTime.Parse("01/16/2012 3:00 PM"), result.StartDateTime.DateTimeLocalValue);
            Assert.AreEqual(DateTime.Parse("01/20/2012 8:00 PM"), result.EndDateTime.DateTimeLocalValue);
            Assert.AreEqual(DateTime.Parse("01/16/2012 3:00 PM").ToUniversalTime(timeZoneId), result.StartDateTime.DateTimeUtcValue);
            Assert.AreEqual(DateTime.Parse("01/20/2012 8:00 PM").ToUniversalTime(timeZoneId), result.EndDateTime.DateTimeUtcValue); ;
        }

        [TestMethod]
        public void StartDateTimeDateTimeUtcValueAndEndDateTimeDateTimeUtcValueSetCorrectlyFromDateTimeLocalValues() 
        {
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
            var form = new FormCollection
                                        {
                                            { "test.StartDateTime.DateTimeLocalValue","01/16/2012 3:00 PM"},
                                            { "test.EndDateTime.DateTimeLocalValue","01/20/2012 8:00 PM"},
                                            { "test.TimeZoneName", "Eastern Standard Time"}
                                        };
            var bindingContext = new ModelBindingContext() { ModelName = "test", ValueProvider = form.ToValueProvider() };

            var b = new UiDateTimeRangeModelBinder() {
                StartLocalDate = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.StartDateTime.LocalDate),
                StartLocalTime = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.StartDateTime.LocalTime),
                EndLocalDate = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.EndDateTime.LocalDate),
                EndLocalTime = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.EndDateTime.LocalTime),
                TimeZoneName = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.TimeZoneName)
            };

            var result = (UiDateTimeRangeModel)b.BindModel(null, bindingContext);
            Assert.AreEqual(DateTime.Parse("01/16/2012 3:00 PM"), result.StartDateTime.DateTimeLocalValue);
            Assert.AreEqual(DateTime.Parse("01/20/2012 8:00 PM"), result.EndDateTime.DateTimeLocalValue);
            Assert.AreEqual(DateTime.Parse("01/16/2012 3:00 PM").ToUniversalTime(timeZoneId), result.StartDateTime.DateTimeUtcValue);
            Assert.AreEqual(DateTime.Parse("01/20/2012 8:00 PM").ToUniversalTime(timeZoneId), result.EndDateTime.DateTimeUtcValue); ;
        }

    }
}
