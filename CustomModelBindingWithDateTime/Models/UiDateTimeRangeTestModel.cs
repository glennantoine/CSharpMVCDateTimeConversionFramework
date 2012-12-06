using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CustomModelBindingWithDateTime.Enumerations;
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

        [TimeValidation(ErrorMessageResourceName = "TimeFormatValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        [UiDateTimeRangeValidator(new[] { UiDateTimeRangeValidationMode.StartDateNotInFuture }, ErrorMessageResourceName = "DateFormatValid", ErrorMessageResourceType = typeof(UiDateTimeRangeModel))]
        public UiDateTimeRangeModel UiDateTimeRange { get; set; }

        [UiDateTimeNotInPastValidation("StartDateTime.LocalDate", ErrorMessageResourceName = "DateNotInPast", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        [UiDateTimeDisplayAttribute(DisplayName = "From", PropertyPath = "StartDateTime.LocalDate")]
        [UiDateTimeNotInPastValidation("EndDateTime.LocalDate", ErrorMessageResourceName = "DateNotInPast", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        [UiDateTimeDisplayAttribute(DisplayName = "To", PropertyPath = "EndDateTime.LocalDate")]
        public UiDateTimeRangeModel BasicDateRange { get; set; }
    }
}
