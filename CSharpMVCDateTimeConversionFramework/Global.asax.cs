using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CSharpMVCDateTimeConversionFramework.Models;
using CSharpMVCDateTimeConversionFramework.Models.Binders;
using CSharpMVCDateTimeConversionFramework.Utilities;

namespace CSharpMVCDateTimeConversionFramework 
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
                                                                                            DateFieldName = StaticReflection.GetMemberName<UiDateTimeModel>(x => x.LocalDate),
                                                                                            TimeFieldName = StaticReflection.GetMemberName<UiDateTimeModel>(x => x.LocalTime),
                                                                                            TimeZoneFieldName = StaticReflection.GetMemberName<UiDateTimeModel>(x => x.TimeZoneName)
                                                                                        };
            ModelBinders.Binders.Add(typeof(UiDateTimeRangeModel), new UiDateTimeRangeModelBinder());

            ModelMetadataProviders.Current = new UiDateTimeDisplayAttributeProvider();

        }
    }
}