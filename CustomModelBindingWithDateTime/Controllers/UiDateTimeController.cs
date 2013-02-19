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
            ViewBag.PageTitle = "C# MVC DateTime Conversion | UiDateTimeModel Test Form";
            UiDateTimeTestModel model = null;
            try {
                var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
                model = new UiDateTimeTestModel {
                                                    UiDateTime = new UiDateTimeModel(timeZone) { }
                                                };
            } 
            catch (Exception e) 
            {
                throw new Exception("UiDateTimeTestModel Error: " + e.Message);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult UiDateTimeTest(UiDateTimeTestModel model) 
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            }

            return View("UiDateTimeTestResults", model);
        }


        [HttpGet]
        public ActionResult UiDateTimeRangeTest()
        {
            ViewBag.PageTitle = "C# MVC DateTime Conversion | UiDateTimeRangeModel Test Form";
            UiDateTimeRangeTestModel model = null;
            try
            {
                var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
                model= new UiDateTimeRangeTestModel 
                                                    {
                                                        UiDateTimeRange = new UiDateTimeRangeModel(timeZone) { }
                                                    };
            }
            catch (Exception e)
            {
                throw new Exception("UiDateTimeRangeModel Error: " + e.Message);
            }
            
            return View(model);
        }

        [HttpPost]
        public ActionResult UiDateTimeRangeTest(UiDateTimeRangeTestModel model) 
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            }
            return View("UiDateTimeRangeTestResults", model);
        }


        [HttpGet]
        public ActionResult UiDateTimeEditorTemplatesTest()
        {
            ViewBag.PageTitle = "C# MVC DateTime Conversion | UiDateTimeModel Editor Templates";
            UiDateTimeEditorTemplatesTestModel model = null;
            try
            {
                var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").StandardName;
                model = new UiDateTimeEditorTemplatesTestModel
                                                        {
                                                            RangeDateToDate = new UiDateTimeRangeModel(timeZone) { },
                                                            RangeTimeToTime = new UiDateTimeRangeModel(timeZone) { },
                                                            RangeDateTimeToTime = new UiDateTimeRangeModel(timeZone) { },
                                                            RangeDateTimeToDateTime = new UiDateTimeRangeModel(timeZone) { }
                                                        };
            }
            catch (Exception e)
            {
                throw new Exception("UiDateTimeRangeModel Error: " + e.Message);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult UiDateTimeEditorTemplatesTest(UiDateTimeEditorTemplatesTestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return View(model);
        }

        public struct TestModel {
            public UiDateTimeModel Model { get; set; }
            public UiDateTimeModel Model2 { get; set; }
        }

        [HttpGet]
        public ActionResult TestUiDateTimeModelAsParameter() {
            var model = new TestModel {
                Model = new UiDateTimeModel("US Eastern Standard Time"),
                Model2 = new UiDateTimeModel("Pacific Standard Time")
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult TestUiDateTimeModelAsParameter(UiDateTimeModel model, UiDateTimeModel model2) {
            return Json(new {
                Date1 = model.DateTimeUtcValue.GetValueOrDefault().ToString("s"),
                Date2 = model2.DateTimeUtcValue.GetValueOrDefault().ToString("s")
            });
        }

        [HttpGet]
        public ActionResult TestUiDateTimeModelAsParameterWithGet(UiDateTimeModel model, UiDateTimeModel model2, string anotherProperty) {
            return Json(new {
                Date1 = model.DateTimeUtcValue.GetValueOrDefault().ToString("s"),
                Date2 = model2.DateTimeUtcValue.GetValueOrDefault().ToString("s"),
                Other = anotherProperty
            },
            JsonRequestBehavior.AllowGet);
        }

    }
}
