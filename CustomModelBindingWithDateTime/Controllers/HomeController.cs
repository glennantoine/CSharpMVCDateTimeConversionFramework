using System.Web.Mvc;

namespace CustomModelBindingWithDateTime.Controllers 
{
    public class HomeController : Controller 
    {
        public ActionResult Index()
        {
            ViewBag.PageTitle = "Custom Model Binder w/ Date Time Conversion";
            ViewBag.Message = "Custom Model Binder for Date Time Conversion Test MVC Application.";
            return View();
        }

        public ActionResult UiDateTimeModel() 
        {
            return View();
        }

        public ActionResult DateAndTimeModelBinder() 
        {
            return View();
        }

        public ActionResult Usage() 
        {
            return View();
        }
    }
}
