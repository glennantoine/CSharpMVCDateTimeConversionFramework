using System;
using System.Web.Mvc;

namespace CustomModelBindingWithDateTime.Models.Binders 
{
    //public class UiDateTimeRangeModelBinder : DefaultModelBinder 
    public class UiDateTimeRangeModelBinder : IModelBinder 
    {
        public UiDateTimeRangeModelBinder(){}

        //public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) 
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) 
        {
            this.TimeZoneName = getValues(bindingContext, "TimeZoneName");

            this.StartLocalDate = getValues(bindingContext, "StartDateTime.LocalDate");
            this.StartLocalTime = getValues(bindingContext, "StartDateTime.LocalTime");
            this.StartLocalDateTime = getValues(bindingContext, "StartDateTime.DateTimeLocalValue");

            if (String.IsNullOrEmpty(this.StartLocalDateTime)) 
            {
                this.StartDateTime = new UiDateTimeModel(this.TimeZoneName) { LocalDate = this.StartLocalDate, LocalTime = this.StartLocalTime };
            } else 
            {
                this.StartDateTime = new UiDateTimeModel(this.TimeZoneName) { DateTimeLocalValue = DateTime.Parse(this.StartLocalDateTime) };
            }

            this.EndLocalDate = getValues(bindingContext, "EndDateTime.LocalDate");
            this.EndLocalTime = getValues(bindingContext, "EndDateTime.LocalTime");
            this.EndLocalDateTime = getValues(bindingContext, "EndDateTime.DateTimeLocalValue");

            if (String.IsNullOrEmpty(this.EndLocalDate) && String.IsNullOrEmpty(this.EndLocalDateTime)) 
            {
                this.EndLocalDate = this.StartLocalDate;
            }

            if (String.IsNullOrEmpty(this.EndLocalDateTime)) 
            {
                this.EndDateTime = new UiDateTimeModel(this.TimeZoneName) { LocalDate = this.EndLocalDate, LocalTime = this.EndLocalTime };
            } else {
                this.EndDateTime = new UiDateTimeModel(this.TimeZoneName) {DateTimeLocalValue = DateTime.Parse(this.EndLocalDateTime) };
            }
            return new UiDateTimeRangeModel(this.TimeZoneName) {
                                                                StartDateTime = this.StartDateTime,
                                                                EndDateTime = this.EndDateTime,
                                                            };
        }

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

        public string TimeZoneName { get; set; }

        public string StartLocalDate { get; set; }
        public string StartLocalTime { get; set; }
        public string StartLocalDateTime { get; set; }
        public string StartLocalTimeZone { get; set; }

        public string EndLocalDate { get; set; }
        public string EndLocalTime { get; set; }
        public string EndLocalDateTime { get; set; }
        public string EndLocalTimeZone { get; set; }

        public UiDateTimeModel StartDateTime { get; set; }
        public UiDateTimeModel EndDateTime { get; set; }
    }
}