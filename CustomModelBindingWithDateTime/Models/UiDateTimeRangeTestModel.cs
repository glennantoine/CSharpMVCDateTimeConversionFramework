using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
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

        [Required]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [UiDateTimeRangeValidator(true, false, false, false, ErrorMessageResourceName = "DateFormatValid", ErrorMessageResourceType = typeof(UiDateTimeRangeModel))]
        public UiDateTimeRangeModel UiDateTimeRange { get; set; }
    }
}

