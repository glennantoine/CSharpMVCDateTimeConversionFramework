using System;
using System.Data.SqlTypes;
using System.Web.Mvc;
using CSharpMVCDateTimeConversionFramework.Utilities;

namespace CSharpMVCDateTimeConversionFramework.Models.Binders
{
    //public class UiDateTimeRangeModelBinder : DefaultModelBinder 
    public class UiDateTimeRangeModelBinder : IModelBinder
    {
        public UiDateTimeRangeModelBinder()
        {
        }

        //public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) 
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            //Used to determine if values where intentionally set by the application
            //or as a result of model binding to keep values in sync
            StartImplicitlySet = false;

            //Use Reflection to get the associated property names from the UiDateTimeRangeModel
            var startDateTime = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.StartDateTime);
            var startDateTimeLocalDate = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.StartDateTime.LocalDate);
            var startDateTimeLocalTime = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.StartDateTime.LocalTime);
            var startDateTimeLocalDateTime = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.StartDateTime.DateTimeLocalValue);
            var startDateTimeNoSetTime = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.StartDateTime.NoSetTime);

            var endDateTime = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.EndDateTime);
            var endDateTimeLocalDate = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.StartDateTime.LocalDate);
            var endDateTimeLocalTime = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.StartDateTime.LocalTime);
            var endDateTimeLocalDateTime = StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.StartDateTime.DateTimeLocalValue);

            //Get the model values for binding
            TimeZoneName = getValues(bindingContext, StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.TimeZoneName));

            StartLocalDate = getValues(bindingContext, startDateTime + "." + startDateTimeLocalDate);
            StartLocalTime = getValues(bindingContext, startDateTime + "." + startDateTimeLocalTime);
            StartLocalDateTime = getValues(bindingContext, startDateTime + "." + startDateTimeLocalDateTime);
            StartNoSetTime = getValues(bindingContext, startDateTime + "." + startDateTimeNoSetTime);

            var noSetTime = false;
            if (!string.IsNullOrWhiteSpace(StartNoSetTime))
            {
                if (bool.TryParse(StartNoSetTime.Split(',')[0], out noSetTime) && noSetTime)
                {
                    StartLocalTime = SqlDateTime.MinValue.Value.Date.ToShortTimeString();
                    StartImplicitlySet = true;
                }
            }

            //*************************************************************
            //Setup the StartDateTime property of the UiDateTimeRangeModel
            if (!String.IsNullOrEmpty(StartLocalDateTime))
            {
                StartDateTime = new UiDateTimeModel(TimeZoneName)
                                         {
                                             DateTimeLocalValue = DateTime.Parse(StartLocalDateTime),
                                             NoSetTime = noSetTime,
                                             ImplicitlySet = StartImplicitlySet
                                         };
            }
            else
            {
                //Both StartLocalDate and StartLocalTime so we will set the StartDateTime.DateTimeLocalValue to null
                if (String.IsNullOrEmpty(StartLocalDate) && String.IsNullOrEmpty(StartLocalTime))
                {
                    StartDateTime = new UiDateTimeModel(TimeZoneName) {DateTimeLocalValue = null, NoSetTime = noSetTime, ImplicitlySet = StartImplicitlySet};
                }
                else
                {
                    if (String.IsNullOrEmpty(StartLocalDate))
                    {
                        StartImplicitlySet = true;
                        StartLocalDate = SqlDateTime.MinValue.Value.Date.ToShortDateString();
                    }

                    if (!noSetTime && String.IsNullOrEmpty(StartLocalTime))
                    {
                        StartImplicitlySet = true;
                    }

                    StartDateTime = new UiDateTimeModel(TimeZoneName)
                                             {
                                                 DateTimeLocalValue = DateTime.Parse(StartLocalDate + " " + StartLocalTime),
                                                 NoSetTime = noSetTime,
                                                 ImplicitlySet = StartImplicitlySet
                                             };
                }
            }


            EndLocalDate = getValues(bindingContext, endDateTime + "." + endDateTimeLocalDate);
            EndLocalTime = getValues(bindingContext, endDateTime + "." + endDateTimeLocalTime);
            EndLocalDateTime = getValues(bindingContext, endDateTime + "." + endDateTimeLocalDateTime);

            //*************************************************************
            //Setup the EndDateTime property of the UiDateTimeRangeModel
            if (!String.IsNullOrEmpty(EndLocalDateTime))
            {
                EndDateTime = new UiDateTimeModel(TimeZoneName) {DateTimeLocalValue = DateTime.Parse(EndLocalDateTime), ImplicitlySet = EndImplicitlySet};
            }
            else
            {
                //Both EndLocalDate and EndLocalTime so we will set the EndDateTime.DateTimeLocalValue to null
                if (String.IsNullOrEmpty(EndLocalDate) && String.IsNullOrEmpty(EndLocalTime))
                {
                    EndDateTime = new UiDateTimeModel(TimeZoneName) {DateTimeLocalValue = null, NoSetTime = false, ImplicitlySet = EndImplicitlySet};
                }
                else
                {
                    if (String.IsNullOrEmpty(EndLocalDate))
                    {
                        EndLocalDate = StartLocalDate;
                        EndImplicitlySet = true;
                    }

                    if (String.IsNullOrEmpty(EndLocalTime))
                    {
                        EndImplicitlySet = true;
                    }
                    EndDateTime = new UiDateTimeModel(TimeZoneName)
                                           {
                                               DateTimeLocalValue = DateTime.Parse(EndLocalDate + " " + EndLocalTime),
                                               ImplicitlySet = EndImplicitlySet
                                           };
                }

            }

            return new UiDateTimeRangeModel(TimeZoneName)
                       {
                           StartDateTime = StartDateTime,
                           EndDateTime = EndDateTime,
                       };
        }


        /// <summary>
        /// Bind properties from the post to the underlying model
        /// </summary>
        /// <param name="bindingContext"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string getValues(ModelBindingContext bindingContext, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            ValueProviderResult result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "." + key);

            if (result == null && bindingContext.FallbackToEmptyPrefix)
            {
                result = bindingContext.ValueProvider.GetValue(key);
            }

            if (result == null)
            {
                return null;
            }
            return result.AttemptedValue;
        }

        //*************************************************************
        //Properties to be used by the UiDateTimeRangeModelBinder to 
        //Bind/Set values on the model
        public string TimeZoneName { get; set; }

        public string StartLocalDate { get; set; }
        public string StartLocalTime { get; set; }
        public string StartLocalDateTime { get; set; }
        public string StartLocalTimeZone { get; set; }
        public string StartNoSetTime { get; set; }
        public bool StartImplicitlySet { get; set; }

        public string EndLocalDate { get; set; }
        public string EndLocalTime { get; set; }
        public string EndLocalDateTime { get; set; }
        public string EndLocalTimeZone { get; set; }
        public bool EndImplicitlySet { get; set; }

        public UiDateTimeModel StartDateTime { get; set; }
        public UiDateTimeModel EndDateTime { get; set; }
    }
}
