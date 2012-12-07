using System;
using System.ComponentModel.DataAnnotations;
using CustomModelBindingWithDateTime.Models;
using CustomModelBindingWithDateTime.Models.ValidationAttributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CustomModelBindingWithDateTime.Tests.Models.ValidationAttributes 
{
    [TestClass]
    public class ValidationAttributesTests 
    {

        private class UiDateTimeRangeTestModel 
        {
            public UiDateTimeRangeModel BasicDateRange { get; set; }
        }

        [TestMethod]
        public void UiDateTimeNotInPastValidationTest()
        {
            var timeZoneName = "Eastern Standard Time";
            var model = new UiDateTimeRangeTestModel 
                                {
                                    BasicDateRange = new UiDateTimeRangeModel(timeZoneName) { StartDateTime = new UiDateTimeModel(timeZoneName) { LocalDate = "12/14/2009" } }
                                };
            var testContext = new ValidationContext(model, null, null);
            testContext.DisplayName = "BasicDateRange";
            var attribute = new UiDateTimeNotInPastValidation("StartDateTime.LocalDate");

            try
            {
                attribute.Validate(model.BasicDateRange.StartDateTime.LocalDate, testContext);

                Assert.Fail("Exception not thrown");
            } 
            catch (ValidationException ex)
            {
                Assert.AreEqual("'Date' must be in the future.", ex.Message);
            }
            catch(Exception)
            {
                Assert.Fail("Unexpected Exception thrown");
            }
            
        }
    }
}
