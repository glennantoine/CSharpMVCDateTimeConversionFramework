using System.Web.Mvc;

namespace CustomModelBindingWithDateTime.Controllers 
{
    public class HomeController : Controller 
    {
        public ActionResult Index() 
        {
            ViewBag.Message = "Custom Model Binder MVC application.";
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
