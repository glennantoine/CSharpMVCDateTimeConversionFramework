using System;
using System.Web.Mvc;
using CustomModelBindingWithDateTime.Utilities;

namespace CustomModelBindingWithDateTime.Models.Binders 
{
    //public class UiDateTimeRangeModelBinder : DefaultModelBinder 
    public class UiDateTimeRangeModelBinder : IModelBinder 
    {
        public UiDateTimeRangeModelBinder() { }

        //public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) 
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) 
        {
            //Used to determine if values where intentionally set by the application
            //or as a result of model binding to keep values in sync
            this.ImplicitylySet = false;

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
            this.TimeZoneName = getValues(bindingContext, StaticReflection.GetMemberName<UiDateTimeRangeModel>(x => x.TimeZoneName));

            this.StartLocalDate = getValues(bindingContext, startDateTime + "." + startDateTimeLocalDate);
            this.StartLocalTime = getValues(bindingContext, startDateTime + "." + startDateTimeLocalTime);
            this.StartLocalDateTime = getValues(bindingContext, startDateTime + "." + startDateTimeLocalDateTime);
            this.StartNoSetTime = getValues(bindingContext, startDateTime + "." + startDateTimeNoSetTime);

            var noSetTime = false;
            if (!string.IsNullOrWhiteSpace(this.StartNoSetTime)) 
            {
                if (bool.TryParse(this.StartNoSetTime.Split(',')[0], out noSetTime) && noSetTime) 
                {
                    this.StartLocalTime = SqlDateTime.MinValue.Value.Date.ToShortTimeString();
                }
            }

            //*************************************************************
            //Setup the StartDateTime property of the UiDateTimeRangeModel
            if (!String.IsNullOrEmpty(this.StartLocalDateTime)) 
            {
                this.StartDateTime = new UiDateTimeModel(this.TimeZoneName) 
                {
                    DateTimeLocalValue = DateTime.Parse(this.StartLocalDateTime),
                    NoSetTime = noSetTime,
                    ImplicitylySet = this.ImplicitylySet
                };
            } else 
            {
                //Both StartLocalDate and StartLocalTime so we will set the StartDateTime.DateTimeLocalValue to null
                if (String.IsNullOrEmpty(this.StartLocalDate) && String.IsNullOrEmpty(this.StartLocalTime)) 
                {
                    this.StartDateTime = new UiDateTimeModel(this.TimeZoneName) { DateTimeLocalValue = null, NoSetTime = noSetTime, ImplicitylySet = this.ImplicitylySet };
                } else 
                {
                    if (String.IsNullOrEmpty(this.StartLocalDate)) 
                    {
                        this.ImplicitylySet = true;
                        this.StartLocalDate = SqlDateTime.MinValue.Value.Date.ToShortDateString();
                    }
                    this.StartDateTime = new UiDateTimeModel(this.TimeZoneName) 
                    {
                        DateTimeLocalValue = DateTime.Parse(this.StartLocalDate + " " + this.StartLocalTime),
                        NoSetTime = noSetTime,
                        ImplicitylySet = this.ImplicitylySet
                    };
                }
            }


            this.EndLocalDate = getValues(bindingContext, endDateTime + "." + endDateTimeLocalDate);
            this.EndLocalTime = getValues(bindingContext, endDateTime + "." + endDateTimeLocalTime);
            this.EndLocalDateTime = getValues(bindingContext, endDateTime + "." + endDateTimeLocalDateTime);

            //*************************************************************
            //Setup the EndDateTime property of the UiDateTimeRangeModel
            if (!String.IsNullOrEmpty(this.EndLocalDateTime)) 
            {
                this.EndDateTime = new UiDateTimeModel(this.TimeZoneName) { DateTimeLocalValue = DateTime.Parse(this.EndLocalDateTime), ImplicitylySet = this.ImplicitylySet };
            } else 
            {
                //Both EndLocalDate and EndLocalTime so we will set the EndDateTime.DateTimeLocalValue to null
                if (String.IsNullOrEmpty(this.EndLocalDate) && String.IsNullOrEmpty(this.EndLocalTime)) 
                {
                    this.EndDateTime = new UiDateTimeModel(this.TimeZoneName) { DateTimeLocalValue = null, NoSetTime = noSetTime, ImplicitylySet = this.ImplicitylySet };
                } else 
                {
                    if (String.IsNullOrEmpty(this.EndLocalDate)) 
                    {
                        this.EndLocalDate = this.StartLocalDate;
                        this.ImplicitylySet = true;
                    }
                    this.EndDateTime = new UiDateTimeModel(this.TimeZoneName) 
                                                                        {
                                                                            DateTimeLocalValue = DateTime.Parse(this.EndLocalDate + " " + this.EndLocalTime),
                                                                            ImplicitylySet = this.ImplicitylySet
                                                                        };
                }

            }

            return new UiDateTimeRangeModel(this.TimeZoneName) 
                                                            {
                                                                StartDateTime = this.StartDateTime,
                                                                EndDateTime = this.EndDateTime,
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

        public string EndLocalDate { get; set; }
        public string EndLocalTime { get; set; }
        public string EndLocalDateTime { get; set; }
        public string EndLocalTimeZone { get; set; }
        public bool ImplicitylySet { get; set; }

        public UiDateTimeModel StartDateTime { get; set; }
        public UiDateTimeModel EndDateTime { get; set; }
    }
}