using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CustomModelBindingWithDateTime.Models.ValidationAttributes;

namespace CustomModelBindingWithDateTime.Models 
{
    public class UiDateTimeRangeTestModel 
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [UiDateTimeDisplayAttribute(DisplayName = "Special Start Date", PropertyPath = "StartDateTime.LocalDate")]
        [UiDateTimeDisplayAttribute(DisplayName = "Start Time", PropertyPath = "StartDateTime.LocalTime")]
        [UiDateTimeDisplayAttribute(DisplayName = "End Time", PropertyPath = "EndDateTime.LocalTime")]
        //[UiDateTimeGreaterThanTimeAttributeOrNullValidation("EndDateTime.LocalTime", "StartDateTime.LocalTime", ErrorMessageResourceName = "TimeMustBeGreaterThanEqualTo", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        public UiDateTimeRangeModel UiDateTimeRange { get; set; }

        [UiDateTimeNotInPastValidation("StartDateTime.LocalDate", ErrorMessageResourceName = "DateNotInPast", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        [UiDateTimeDisplayAttribute(DisplayName = "From", PropertyPath = "StartDateTime.LocalDate")]
        [UiDateTimeGreaterThanDateAttributeOrNullValidation("EndDateTime.LocalDate", "StartDateTime.LocalDate", ErrorMessageResourceName = "DateMustBeGreaterThanEqualTo", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        [UiDateTimeNotInPastValidation("EndDateTime.LocalDate", ErrorMessageResourceName = "DateNotInPast", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        [UiDateTimeDisplayAttribute(DisplayName = "To", PropertyPath = "EndDateTime.LocalDate")]
        public UiDateTimeRangeModel BasicDateRange { get; set; }
    }
}
