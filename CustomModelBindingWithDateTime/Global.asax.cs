using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CustomModelBindingWithDateTime.Models;
using CustomModelBindingWithDateTime.Models.Binders;
using CustomModelBindingWithDateTime.Models.Managers;

namespace CustomModelBindingWithDateTime 
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication 
    {
        protected void Application_Start() 
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Setup Default Model Binder for UiDateTimeModel 
            ModelBinders.Binders[typeof(UiDateTimeModel)] = new UiDateTimeModelBinder() {
                                                                                            DateFieldName = "LocalDate",
                                                                                            TimeFieldName = "LocalTime",
                                                                                            TimeZoneFieldName = "TimeZoneName"
                                                                                        };
            ModelBinders.Binders.Add(typeof(UiDateTimeRangeModel), new UiDateTimeRangeModelBinder());

            // Add our custom model validator provider.
            ModelValidatorProviders.Providers.Add(new UiDateTimeModelValidatorProvider());
        }
    }
}