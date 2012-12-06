using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomModelBindingWithDateTime.Models;
using CustomModelBindingWithDateTime.Models.Binders;
using CustomModelBindingWithDateTime.Utilities;

namespace CustomModelBindingWithDateTime.Tests.Models.Binders 
{
    [TestClass]
    public class UiDateTimeRangeModelBinderTest 
    {

        [TestMethod]
        [Ignore]
        public void DateTimeUtcValueIsSetCorrectlyFromDateTimeLocalValue()
        {
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
            var form = new FormCollection
                                        {
                                            { "StartDateTime.LocalDate","01/16/2012"},
                                            { "StartDateTime.LocalTime", "1:00"},
                                            { "EndDateTime.LocalTime", "3:00"},
                                            { "TimeZoneName", "Eastern Standard Time"}
                                        };
            var bindingContext = new ModelBindingContext() { ModelName = "test", ValueProvider = form.ToValueProvider() };

            var b = new UiDateTimeRangeModelBinder() {
                                                        StartLocalDate = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.StartDateTime.LocalDate),
                                                        StartLocalTime = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.StartDateTime.LocalTime),
                                                        EndLocalTime = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.EndDateTime.LocalTime),
                                                        TimeZoneName = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.TimeZoneName)
                                                    };

            var result = (UiDateTimeRangeModel)b.BindModel(null, bindingContext);
            Assert.AreEqual(DateTime.Parse("01/16/2012 01:00:00").ToUniversalTime(timeZoneId), result.StartDateTime);
        }
    }
}
