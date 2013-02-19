using System;
using CSharpMVCDateTimeConversionFramework.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpMVCDateTimeConversionFramework.Utilities;

namespace CSharpMVCDateTimeConversionFramework.Tests.Models
{
    [TestClass]
    public class UiDateTimeModelTest
    {
        [TestMethod]
        public void TestUiDateTimeModelWithLocalDateOnlyPasses()
        {
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
            var testUiDateTimeModel = new UiDateTimeModel(timeZoneId)
                                          {
                                              LocalDate = "02/18/2007"
                                          };
            Assert.AreEqual(DateTime.Parse("02/18/2007 12:00:00 AM"), testUiDateTimeModel.DateTimeLocalValue);
            Assert.AreEqual(DateTime.Parse("02/18/2007 12:00:00 AM").ToUniversalTime(timeZoneId), testUiDateTimeModel.DateTimeUtcValue);
        }


        [TestMethod]
        public void TestUiDateTimeModelWithLocalTimeOnlyPasses()
        {
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
            var testUiDateTimeModel = new UiDateTimeModel(timeZoneId)
                                          {
                                              LocalTime = "9:00:00 PM"
                                          };
            Assert.AreEqual(DateTime.Parse("01/01/1753 9:00:00 PM"), testUiDateTimeModel.DateTimeLocalValue);
            Assert.AreEqual(DateTime.Parse("01/01/1753 9:00:00 PM").ToUniversalTime(timeZoneId), testUiDateTimeModel.DateTimeUtcValue);
        }


        [TestMethod]
        public void TestUiDateTimeModelWithLocalDateAndLocalTimeSetPasses()
        {
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
            var testUiDateTimeModel = new UiDateTimeModel(timeZoneId)
                                          {
                                              LocalDate = "02/28/2013",
                                              LocalTime = "11:00:00 PM"
                                          };
            Assert.AreEqual(DateTime.Parse("02/28/2013 11:00:00 PM"), testUiDateTimeModel.DateTimeLocalValue);
            Assert.AreEqual(DateTime.Parse("02/28/2013 11:00:00 PM").ToUniversalTime(timeZoneId), testUiDateTimeModel.DateTimeUtcValue);
        }

        /// <summary>
        /// Setting LocalDate, LocalTime and DateTimeLocalValue is not allowed
        /// However, if this is done the DateTimeLocalValue is the value posted to the controller
        /// </summary>
        [TestMethod]
        public void TestUiDateTimeModelWithLocalDateAndLocalTimeAndDateTimeLocalValueSetFails()
        {
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
            var testUiDateTimeModel = new UiDateTimeModel(timeZoneId)
                                          {
                                              LocalDate = "02/28/2013",
                                              LocalTime = "11:00:00 PM",
                                              DateTimeLocalValue = DateTime.Parse("02/28/2013 5:00:00 PM")
                                          };

            Assert.AreNotEqual(DateTime.Parse("02/28/2013 11:00:00 PM"), testUiDateTimeModel.DateTimeLocalValue);
            Assert.AreNotEqual(DateTime.Parse("02/28/2013 11:00:00 PM").ToUniversalTime(timeZoneId), testUiDateTimeModel.DateTimeUtcValue);
        }

        [TestMethod]
        public void TestUiDateTimeModelWithDateTimeLocalValueSetPasses()
        {
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
            var dateTimeLocalValue = DateTime.Parse("02/28/2013 11:00:00 PM");

            var testUiDateTimeModel = new UiDateTimeModel(timeZoneId)
                                          {
                                              DateTimeLocalValue = dateTimeLocalValue
                                          };

            Assert.AreEqual(dateTimeLocalValue, testUiDateTimeModel.DateTimeLocalValue);
            Assert.AreEqual(dateTimeLocalValue.ToUniversalTime(timeZoneId), testUiDateTimeModel.DateTimeUtcValue);
        }

        [TestMethod]
        public void TestUiDateTimeModelWithUtcDateTimeSetPasses()
        {
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
            var dateTimeUtcValue = DateTime.Parse("02/28/2013 11:00:00 PM");

            var testUiDateTimeModel = new UiDateTimeModel(timeZoneId)
                                          {
                                              DateTimeUtcValue = dateTimeUtcValue
                                          };

            Assert.AreEqual(dateTimeUtcValue.ToLocalTime(timeZoneId), testUiDateTimeModel.DateTimeLocalValue);
            Assert.AreEqual(dateTimeUtcValue, testUiDateTimeModel.DateTimeUtcValue);
        }
    }
}
