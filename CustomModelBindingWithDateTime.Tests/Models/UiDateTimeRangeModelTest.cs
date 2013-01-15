using System;
using CustomModelBindingWithDateTime.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomModelBindingWithDateTime.Utilities;


namespace CustomModelBindingWithDateTime.Tests.Models
{
    [TestClass]
    public class UiDateTimeRangeModelTest
    {

        [TestMethod]
        public void TestUiDateTimeRangeModelWithStartDateEndDatePasses()
        {
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
            var startDateTimeLocalDate = "2/18/2010";
            var endDateTimeLocalDate = "2/22/2010";
            var startDateTimeDateTimeLocalValue = DateTime.Parse("02/18/2010 12:00 AM");
            var endDateTimeDateTimeLocalValue = DateTime.Parse("02/22/2010 12:00 AM");

            var testUiDateTimeRangeModel = new UiDateTimeRangeModel(timeZoneId)
                                               {
                                                   StartDateTime = new UiDateTimeModel(timeZoneId)
                                                                       {
                                                                           LocalDate = startDateTimeLocalDate
                                                                       },
                                                   EndDateTime = new UiDateTimeModel(timeZoneId)
                                                                     {
                                                                         LocalDate = endDateTimeLocalDate
                                                                     }
                                               };
            Assert.IsNull(testUiDateTimeRangeModel.StartDateTime.LocalTime);
            Assert.AreEqual(startDateTimeDateTimeLocalValue, testUiDateTimeRangeModel.StartDateTime.DateTimeLocalValue);
            Assert.AreEqual(startDateTimeDateTimeLocalValue.ToUniversalTime(timeZoneId), testUiDateTimeRangeModel.StartDateTime.DateTimeUtcValue);

            Assert.IsNull(testUiDateTimeRangeModel.EndDateTime.LocalTime);
            Assert.AreEqual(endDateTimeDateTimeLocalValue, testUiDateTimeRangeModel.EndDateTime.DateTimeLocalValue);
            Assert.AreEqual(endDateTimeDateTimeLocalValue.ToUniversalTime(timeZoneId), testUiDateTimeRangeModel.EndDateTime.DateTimeUtcValue);
        }

        [TestMethod]
        public void TestUiDateTimeRangeModelWithStartDateLocalTimeEndDateLocalTimePasses()
        {
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
            var startDateTimeLocalTime = "8:00 AM";
            var endDateTimeLocalTime = "11:00 AM";
            var startDateTimeDateTimeLocalValue = DateTime.Parse("01/01/1753 08:00 AM");
            var endDateTimeDateTimeLocalValue = DateTime.Parse("01/01/1753 11:00 AM");

            var testUiDateTimeRangeModel = new UiDateTimeRangeModel(timeZoneId)
                                               {
                                                   StartDateTime = new UiDateTimeModel(timeZoneId)
                                                                       {
                                                                           LocalTime = startDateTimeLocalTime
                                                                       },
                                                   EndDateTime = new UiDateTimeModel(timeZoneId)
                                                                     {
                                                                         LocalTime = endDateTimeLocalTime
                                                                     }
                                               };
            Assert.IsNull(testUiDateTimeRangeModel.StartDateTime.LocalDate);
            Assert.AreEqual(startDateTimeDateTimeLocalValue, testUiDateTimeRangeModel.StartDateTime.DateTimeLocalValue);
            Assert.AreEqual(startDateTimeDateTimeLocalValue.ToUniversalTime(timeZoneId), testUiDateTimeRangeModel.StartDateTime.DateTimeUtcValue);

            Assert.IsNull(testUiDateTimeRangeModel.EndDateTime.LocalDate);
            Assert.AreEqual(endDateTimeDateTimeLocalValue, testUiDateTimeRangeModel.EndDateTime.DateTimeLocalValue);
            Assert.AreEqual(endDateTimeDateTimeLocalValue.ToUniversalTime(timeZoneId), testUiDateTimeRangeModel.EndDateTime.DateTimeUtcValue);
        }

        [TestMethod]
        public void TestUiDateTimeRangeModelWithStartDateLocalDateStartDateLocalTimeAndEndDateLocalDateEndDateLocalTimePasses()
        {
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
            var startDateTimeLocalDate = "2/18/2010";
            var endDateTimeLocalDate = "2/22/2010";
            var startDateTimeLocalTime = "8:00 AM";
            var endDateTimeLocalTime = "11:00 AM";
            var startDateTimeDateTimeLocalValue = DateTime.Parse("02/18/2010 08:00 AM");
            var endDateTimeDateTimeLocalValue = DateTime.Parse("02/22/2010 11:00 AM");

            var testUiDateTimeRangeModel = new UiDateTimeRangeModel(timeZoneId)
                                               {
                                                   StartDateTime = new UiDateTimeModel(timeZoneId)
                                                                       {
                                                                           LocalDate = startDateTimeLocalDate,
                                                                           LocalTime = startDateTimeLocalTime
                                                                       },
                                                   EndDateTime = new UiDateTimeModel(timeZoneId)
                                                                     {
                                                                         LocalDate = endDateTimeLocalDate,
                                                                         LocalTime = endDateTimeLocalTime
                                                                     }
                                               };

            Assert.AreEqual(startDateTimeLocalDate, testUiDateTimeRangeModel.StartDateTime.LocalDate);
            Assert.AreEqual(startDateTimeLocalTime, testUiDateTimeRangeModel.StartDateTime.LocalTime);
            Assert.AreEqual(startDateTimeDateTimeLocalValue, testUiDateTimeRangeModel.StartDateTime.DateTimeLocalValue);
            Assert.AreEqual(startDateTimeDateTimeLocalValue.ToUniversalTime(timeZoneId), testUiDateTimeRangeModel.StartDateTime.DateTimeUtcValue);

            Assert.AreEqual(endDateTimeLocalDate, testUiDateTimeRangeModel.EndDateTime.LocalDate);
            Assert.AreEqual(endDateTimeLocalTime, testUiDateTimeRangeModel.EndDateTime.LocalTime);
            Assert.AreEqual(endDateTimeDateTimeLocalValue, testUiDateTimeRangeModel.EndDateTime.DateTimeLocalValue);
            Assert.AreEqual(endDateTimeDateTimeLocalValue.ToUniversalTime(timeZoneId), testUiDateTimeRangeModel.EndDateTime.DateTimeUtcValue);
        }

        [TestMethod]
        public void TestUiDateTimeRangeModelWithStartDateLocalDateStartDateDateTimeLocalValueAndEndDateLocalDateTimeLocalValuePasses()
        {
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
            var startDateTimeLocalDate = "2/18/2010";
            var endDateTimeLocalDate = "2/22/2010";
            var startDateTimeLocalTime = "8:00 AM";
            var endDateTimeLocalTime = "11:00 AM";
            var startDateTimeDateTimeLocalValue = DateTime.Parse("2/18/2010 8:00 AM");
            var endDateTimeDateTimeLocalValue = DateTime.Parse("2/22/2010 11:00 AM");

            var testUiDateTimeRangeModel = new UiDateTimeRangeModel(timeZoneId)
                                               {
                                                   StartDateTime = new UiDateTimeModel(timeZoneId)
                                                                       {
                                                                           DateTimeLocalValue = startDateTimeDateTimeLocalValue
                                                                       },
                                                   EndDateTime = new UiDateTimeModel(timeZoneId)
                                                                     {
                                                                         DateTimeLocalValue = endDateTimeDateTimeLocalValue
                                                                     }
                                               };

            Assert.AreEqual(startDateTimeLocalDate, testUiDateTimeRangeModel.StartDateTime.LocalDate);
            Assert.AreEqual(startDateTimeLocalTime, testUiDateTimeRangeModel.StartDateTime.LocalTime);
            Assert.AreEqual(startDateTimeDateTimeLocalValue, testUiDateTimeRangeModel.StartDateTime.DateTimeLocalValue);
            Assert.AreEqual(startDateTimeDateTimeLocalValue.ToUniversalTime(timeZoneId), testUiDateTimeRangeModel.StartDateTime.DateTimeUtcValue);

            Assert.AreEqual(endDateTimeLocalDate, testUiDateTimeRangeModel.EndDateTime.LocalDate);
            Assert.AreEqual(endDateTimeLocalTime, testUiDateTimeRangeModel.EndDateTime.LocalTime);
            Assert.AreEqual(endDateTimeDateTimeLocalValue, testUiDateTimeRangeModel.EndDateTime.DateTimeLocalValue);
            Assert.AreEqual(endDateTimeDateTimeLocalValue.ToUniversalTime(timeZoneId), testUiDateTimeRangeModel.EndDateTime.DateTimeUtcValue);
        }
    }
}