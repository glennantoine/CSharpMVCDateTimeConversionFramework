using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CustomModelBindingWithDateTime.Models.ValidationAttributes;
using sCRM.Presentation.Web.Areas.Crm.Models.ValidationAttributes;

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

        [UiDateTimeDisplayAttribute(DisplayName = "From", PropertyPath = "StartDateTime.LocalDate")]
        [UiDateTimeDisplayAttribute(DisplayName = "To", PropertyPath = "EndDateTime.LocalDate")]
        [UiDateTimeNotInPastValidation("StartDateTime.LocalDate", ErrorMessageResourceName = "DateNotInPast", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        [UiDateTimeRequiredIfAttributeValueNotEqualsValidation("EndDateTime.LocalDate", "StartDateTime.LocalDate", "12/04/2012", ErrorMessageResourceName = "IsRequiredValid", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        [UiDateTimeGreaterThanDateAttributeOrNullValidation("EndDateTime.LocalDate", "StartDateTime.LocalDate", ErrorMessageResourceName = "DateMustBeGreaterThanEqualTo", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        [UiDateTimeNotInPastValidation("EndDateTime.LocalDate", ErrorMessageResourceName = "DateNotInPast", ErrorMessageResourceType = typeof(Resources.ValidationResource))]
        public UiDateTimeRangeModel UiDateTimeRange { get; set; }
    }
}
