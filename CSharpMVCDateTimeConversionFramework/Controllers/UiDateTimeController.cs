﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using CSharpMVCDateTimeConversionFramework.Models;

namespace CSharpMVCDateTimeConversionFramework.Controllers
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
            public UiDateTimeModel ModelOne { get; set; }
            public UiDateTimeModel ModelTwo { get; set; }
        }

        [HttpGet]
        public ActionResult TestUiDateTimeModelAsParameter() {
            var model = new TestModel {
                ModelOne = new UiDateTimeModel("US Eastern Standard Time"),
                ModelTwo = new UiDateTimeModel("Pacific Standard Time")
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult TestUiDateTimeModelAsParameter(UiDateTimeModel modelOne, UiDateTimeModel modelTwo) {
            return Json(new {
                modelOneUtcDate = modelOne.DateTimeUtcValue.GetValueOrDefault().ToString("s"),
                modelTwoUtcDate = modelTwo.DateTimeUtcValue.GetValueOrDefault().ToString("s")
            });
        }

        [HttpGet]
        public ActionResult TestUiDateTimeModelAsParameterWithGet(UiDateTimeModel modelOne, UiDateTimeModel modelTwo, string anotherProperty) {
            return Json(new {
                modelOneUtcDate = modelOne.DateTimeUtcValue.GetValueOrDefault().ToString("s"),
                modelTwoUtcDate = modelTwo.DateTimeUtcValue.GetValueOrDefault().ToString("s"),
                Other = anotherProperty
            },
            JsonRequestBehavior.AllowGet);
        }

    }
}
