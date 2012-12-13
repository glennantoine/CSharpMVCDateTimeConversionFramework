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
        //You can override this on a per test basis 
        private string _timeZoneName = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;

        private class ValidationTestUiDateTimeRangeModel 
        {
            public UiDateTimeRangeModel BasicDateRange { get; set; }
        }

        private class ValidationTestUiDateTimeModel 
        {
            public UiDateTimeModel BasicDateTime { get; set; }
        }

        /// <summary>
        /// Date Validation Tests
        /// </summary>
        [TestMethod]
        public void UiDateTimeDateValidationPasses()
        {
            var model = new ValidationTestUiDateTimeModel 
                                    {
                                        BasicDateTime = new UiDateTimeModel(_timeZoneName)
                                                            {
                                                                LocalDate = "1/14/2100"
                                                            }
                                    };
            var testContext = new ValidationContext(model, null, null) { DisplayName = "BasicDateTime" };
            var attribute = new UiDateTimeFormatDateValidation("LocalDate");

            try 
            {
                attribute.Validate(model.BasicDateTime.LocalDate, testContext);
            } 
            catch (Exception ex) 
            {
                Assert.Fail("Unexpected Exception thrown: " + ex.Message);
            }
        }

        [TestMethod]
        public void UiDateTimeDateValidationFails() 
        {
            _timeZoneName = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time").StandardName;
            var model = new ValidationTestUiDateTimeModel 
                                        {
                                            BasicDateTime = new UiDateTimeModel(_timeZoneName) 
                                                                        {
                                                                            LocalDate = "1/15/"
                                                                        }
                                        };
            var testContext = new ValidationContext(model, null, null) { DisplayName = "BasicDateTime" };
            var attribute = new UiDateTimeFormatDateValidation("LocalDate");

            try 
            {
                attribute.Validate(model.BasicDateTime.LocalDate, testContext);
                Assert.Fail("Exception not thrown");
            } 
            catch (ValidationException ex) 
            {
                Assert.AreEqual("'Date' does not exist or is improperly formated: MM/DD/YYYY.", ex.Message);
            } 
            catch (Exception) 
            {
                Assert.Fail("Unexpected Exception thrown");
            }
        }

        /// <summary>
        /// DateTime Not in the Past Validation Tests
        /// </summary>
        [TestMethod]
        public void UiDateTimeNotInPastValidationTestPasses() 
        {
            var model = new ValidationTestUiDateTimeRangeModel 
                                    {
                                        BasicDateRange = new UiDateTimeRangeModel(_timeZoneName) 
                                                                {  
                                                                    StartDateTime = new UiDateTimeModel(_timeZoneName) { LocalDate = "1/14/2100" } 
                                                                }
                                    };
            var testContext = new ValidationContext(model, null, null) {DisplayName = "BasicDateRange"};
            var attribute = new UiDateTimeNotInPastValidation("StartDateTime.LocalDate");

            try 
            {
                attribute.Validate(model.BasicDateRange.StartDateTime.LocalDate, testContext);
            } 
            catch (Exception ex) 
            {
                Assert.Fail("Unexpected Exception thrown: " + ex.Message);
            }
        }

        [TestMethod]
        public void UiDateTimeNotInPastValidationTestFails()
        {
            var model = new ValidationTestUiDateTimeRangeModel 
                                {
                                    BasicDateRange = new UiDateTimeRangeModel(_timeZoneName) { StartDateTime = new UiDateTimeModel(_timeZoneName) { LocalDate = "12/14/2009" } }
                                };
            var testContext = new ValidationContext(model, null, null) {DisplayName = "BasicDateRange"};
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


        /// <summary>
        /// DateTime Not in the Future Validation
        /// </summary>
        [TestMethod]
        public void UiDateTimeNotInFutureValidationTestPasses() 
        {
            var model = new ValidationTestUiDateTimeRangeModel 
                                                {
                                                    BasicDateRange = new UiDateTimeRangeModel(_timeZoneName) { StartDateTime = new UiDateTimeModel(_timeZoneName) { LocalDate = "1/14/1900" } }
                                                };
            var testContext = new ValidationContext(model, null, null) {DisplayName = "BasicDateRange"};
            var attribute = new UiDateTimeNotInFutureValidation("StartDateTime.LocalDate");

            try 
            {
                attribute.Validate(model.BasicDateRange.StartDateTime.LocalDate, testContext);
            } 
            catch (Exception ex) 
            {
                Assert.Fail("Unexpected Exception thrown: " + ex.Message);
            }
        }


        [TestMethod]
        public void UiDateTimeNotInFutureValidationTestFails() 
        {
            var model = new ValidationTestUiDateTimeRangeModel 
                                        {
                                            BasicDateRange = new UiDateTimeRangeModel(_timeZoneName) { StartDateTime = new UiDateTimeModel(_timeZoneName) { LocalDate = "1/14/2100" } }
                                        };
            var testContext = new ValidationContext(model, null, null) {DisplayName = "BasicDateRange"};
            var attribute = new UiDateTimeNotInFutureValidation("StartDateTime.LocalDate");

            try 
            {
                attribute.Validate(model.BasicDateRange.StartDateTime.LocalDate, testContext);
                Assert.Fail("Exception not thrown");
            } 
            catch (ValidationException ex) 
            {
                Assert.AreEqual("'Date' must be in the past.", ex.Message);
            } 
            catch (Exception) 
            {
                Assert.Fail("Unexpected Exception thrown");
            }

        }

        /// <summary>
        /// DateTime Greater Than Date Attribute Or Null Validation Tests
        /// </summary>
        [TestMethod]
        public void UiDateTimeGreaterThanDateAttributeOrNullValidationTestPasses() 
        {
            var model = new ValidationTestUiDateTimeRangeModel 
                                            {
                                                BasicDateRange = new UiDateTimeRangeModel(_timeZoneName) 
                                                                        { 
                                                                            StartDateTime = new UiDateTimeModel(_timeZoneName) { LocalDate = "1/14/2012" } ,
                                                                            EndDateTime = new UiDateTimeModel(_timeZoneName) { LocalDate = "5/22/2012" } 
                                                                        }
                                            };
            var testContext = new ValidationContext(model, null, null) { DisplayName = "BasicDateRange" };
            var attribute = new UiDateTimeGreaterThanDateAttributeOrNullValidation("EndDateTime.LocalDate", "StartDateTime.LocalDate");

            try 
            {
                attribute.Validate(model.BasicDateRange.StartDateTime.LocalDate, testContext);
            } 
            catch (Exception ex) 
            {
                Assert.Fail("Unexpected Exception thrown: " + ex.Message);
            }
        }

        [TestMethod]
        public void UiDateTimeGreaterThanDateAttributeOrNullValidationEqualDatesTestPasses() 
        {
            var model = new ValidationTestUiDateTimeRangeModel 
                                                {
                                                    BasicDateRange = new UiDateTimeRangeModel(_timeZoneName) 
                                                                                {
                                                                                    StartDateTime = new UiDateTimeModel(_timeZoneName) { LocalDate = "1/14/2012" },
                                                                                    EndDateTime = new UiDateTimeModel(_timeZoneName) { LocalDate = "1/14/2012" }
                                                                                }
                                                };
            var testContext = new ValidationContext(model, null, null) { DisplayName = "BasicDateRange" };
            var attribute = new UiDateTimeGreaterThanDateAttributeOrNullValidation("EndDateTime.LocalDate", "StartDateTime.LocalDate");

            try 
            {
                attribute.Validate(model.BasicDateRange.StartDateTime.LocalDate, testContext);
            } 
            catch (Exception ex) 
            {
                Assert.Fail("Unexpected Exception thrown: " + ex.Message);
            }
        }


        [TestMethod]
        public void UiDateTimeGreaterThanDateAttributeOrNullValidationTestFails() 
        {
            _timeZoneName = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time").StandardName;
            var model = new ValidationTestUiDateTimeRangeModel 
                                                {
                                                    BasicDateRange = new UiDateTimeRangeModel(_timeZoneName) 
                                                                                    {
                                                                                        StartDateTime = new UiDateTimeModel(_timeZoneName) { LocalDate = "1/14/2012" },
                                                                                        EndDateTime = new UiDateTimeModel(_timeZoneName) { LocalDate = "5/22/2000" }
                                                                                    }
                                                };
            var testContext = new ValidationContext(model, null, null) { DisplayName = "BasicDateRange" };
            var attribute = new UiDateTimeGreaterThanDateAttributeOrNullValidation("EndDateTime.LocalDate", "StartDateTime.LocalDate");

            try 
            {
                attribute.Validate(model.BasicDateRange.StartDateTime.LocalDate, testContext);
                Assert.Fail("Exception not thrown");
            } 
            catch (ValidationException ex) 
            {
                Assert.AreEqual("'Date' must be greater than 'Date'", ex.Message);
            } 
            catch (Exception) 
            {
                Assert.Fail("Unexpected Exception thrown");
            }
        }

    }
}
