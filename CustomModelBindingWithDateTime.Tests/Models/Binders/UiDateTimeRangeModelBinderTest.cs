using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomModelBindingWithDateTime.Models;
using CustomModelBindingWithDateTime.Models.Binders;
using CustomModelBindingWithDateTime.Utils;

namespace CustomModelBindingWithDateTime.Tests.Models.Binders 
{
    [TestClass]
    public class UiDateTimeRangeModelBinderTest 
    {

        [TestMethod]
        [Ignore]
        public void DateTimeUtcValueIsSetCorrectlyFromDateTimeLocalValue()
        {
            const string timeZoneId = "Eastern Standard Time";
            var form = new FormCollection
                                        {
                                            { "StartDateTime.LocalDate","01/16/2012"},
                                            { "StartDateTime.LocalTime", "1:00"},
                                            { "EndDateTime.LocalTime", "3:00"},
                                            { "TimeZoneName", "Eastern Standard Time"}
                                        };
            var bindingContext = new ModelBindingContext() { ModelName = "test", ValueProvider = form.ToValueProvider() };

            var b = new UiDateTimeRangeModelBinder() {
                                                        StartLocalDate = "StartDateTime.LocalDate",
                                                        StartLocalTime = "StartDateTime.LocalTime",
                                                        EndLocalTime = "EndDateTime.LocalTime",
                                                        TimeZoneName = "TimeZoneName"
                                                    };

            var result = (UiDateTimeRangeModel)b.BindModel(null, bindingContext);
            Assert.AreEqual(DateTime.Parse("01/16/2012 01:00:00").ToUniversalTime(timeZoneId), result.StartDateTime);
        }
    }
}
