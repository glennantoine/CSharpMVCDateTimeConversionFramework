using System.Web.Mvc;

namespace CustomModelBindingWithDateTime.Models.Binders 
{

    public class UiDateTimeModelAttribute : CustomModelBinderAttribute 
    {
        private IModelBinder _binder;

        // The user cares about a full date structure and full
        // time structure, or one or the other.
        public UiDateTimeModelAttribute(string date, string time) 
        {
            _binder = new UiDateTimeModelBinder 
                                            {
                                                DateFieldName = date,
                                                TimeFieldName = time
                                            };
        }

        // The user wants to capture the date and time (or only one)
        // as individual portions.
        public UiDateTimeModelAttribute(string year, string month, string day,
                                    string hour, string minute, string second) 
        {
            _binder = new UiDateTimeModelBinder 
                                            {
                                                DayFieldName = day,
                                                MonthFieldName = month,
                                                YearFieldName = year,
                                                HourFieldName = hour,
                                                MinuteFieldName = minute,
                                                SecondFieldName = second
                                            };
        }

        // The user wants to capture the date and time (or only one)
        // as individual portions.
        public UiDateTimeModelAttribute(string date, string time, string year, string month, string day,
                                    string hour, string minute, string second) 
        {
            _binder = new UiDateTimeModelBinder 
                                            {
                                                DayFieldName = day,
                                                MonthFieldName = month,
                                                YearFieldName = year,
                                                HourFieldName = hour,
                                                MinuteFieldName = minute,
                                                SecondFieldName = second,
                                                DateFieldName = date,
                                                TimeFieldName = time
                                            };
        }

        public override IModelBinder GetBinder() { return _binder; }
    }
}