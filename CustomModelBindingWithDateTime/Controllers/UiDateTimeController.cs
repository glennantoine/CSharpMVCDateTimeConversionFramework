using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using CustomModelBindingWithDateTime.Models;

namespace CustomModelBindingWithDateTime.Controllers
{
    public class UiDateTimeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UiDateTimeTest() 
        {
            UiDateTimeTestModel model = null;
            model = new UiDateTimeTestModel {
                                                UiDateTime = new UiDateTimeModel(TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName) { }
                                            };
            return View(model);
        }

        [HttpPost]
        public ActionResult UiDateTimeTest(UiDateTimeTestModel model) 
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            }
            var localDate = model.UiDateTime.LocalDate;
            var localTime = model.UiDateTime.LocalTime;
            var localDateTime = model.UiDateTime.DateTimeLocalValue;
            var localTimeZone = model.UiDateTime.TimeZoneName;
            var utcDateTime = model.UiDateTime.DateTimeUtcValue;

            return View("UiDateTimeTestResults", model);
        }


        [HttpGet]
        public ActionResult UiDateTimeRangeTest() 
        {
            UiDateTimeRangeTestModel model = null;
            model = new UiDateTimeRangeTestModel 
                                                {
                                                    UiDateTimeRange = new UiDateTimeRangeModel(TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName) { },
                                                    BasicDateRange = new UiDateTimeRangeModel("Eastern Standard Time") { }
                                                };
            return View(model);
        }

        [HttpPost]
        public ActionResult UiDateTimeRangeTest(UiDateTimeRangeTestModel model) 
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            }

            var startDateTimeModel = model.UiDateTimeRange.StartDateTime;
            var startDateValue = model.UiDateTimeRange.StartDateTime.LocalDate;
            var startTimeValue = model.UiDateTimeRange.StartDateTime.LocalTime;
            var startLocalDateTime = model.UiDateTimeRange.StartDateTime.DateTimeLocalValue;
            var startUtcDateTime = model.UiDateTimeRange.StartDateTime.DateTimeUtcValue;

            var endDateTimeModel = model.UiDateTimeRange.EndDateTime;
            var endDateValue = model.UiDateTimeRange.EndDateTime.LocalDate;
            var endTimeValue = model.UiDateTimeRange.EndDateTime.LocalTime;
            var endLocalDateTime = model.UiDateTimeRange.EndDateTime.DateTimeLocalValue;
            var endUtcDateTime = model.UiDateTimeRange.EndDateTime.DateTimeUtcValue;

            var uiDateTimeRangeModel = model.UiDateTimeRange;

            return View("UiDateTimeRangeTestResults", model);
        }

        private IEnumerable<string> GetErrorsFromModelState() 
        {
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus) 
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus) {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }
}
