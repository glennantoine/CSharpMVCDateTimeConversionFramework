using System.Web.Mvc;

namespace CSharpMVCDateTimeConversionFramework.Controllers 
{
    public class HomeController : Controller 
    {
        public ActionResult Index()
        {
            ViewBag.PageTitle = " Date Time Conversion w/ Custom Model Binder";
            ViewBag.Message = "Date Time Conversions & Custom Model Binding";
            return View();
        }

        public ActionResult UiDateTimeModel() 
        {
            ViewBag.PageTitle = "C# MVC DateTime Conversions | Custom Model Binding w/ UiDateTimeModel";
            return View();
        }

        public ActionResult UiDateTimeModelBinder() 
        {
            ViewBag.PageTitle = "C# MVC DateTime Conversions | Custom Model Binder w/ UiDateTimeModel";
            return View();
        }

        public ActionResult UiDateTimeRangeModel() {
            ViewBag.PageTitle = "C# MVC DateTime Conversions | Custom Model Binding w/ UiDateTimeRangeModel";
            return View();
        }

        public ActionResult UiDateTimeRangeModelBinder() {
            ViewBag.PageTitle = "C# MVC DateTime Conversions | Custom Model Binder w/ UiDateTimeRangeModel";
            return View();
        }

        public ActionResult SourceCode()
        {
            ViewBag.PageTitle = "C# MVC DateTime Conversions | Custom Model Binding";
            return View();
        }

        public ActionResult Usage() 
        {
            ViewBag.PageTitle = "C# MVC DateTime Conversions | Examples";
            return View();
        }
    }
}
