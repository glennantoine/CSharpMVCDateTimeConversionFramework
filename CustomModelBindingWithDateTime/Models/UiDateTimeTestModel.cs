using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CustomModelBindingWithDateTime.Models 
{
    public class UiDateTimeTestModel 
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

        [UiDateTimeDisplayAttribute("LocalDate", "BeginDate", typeof(Resources.UiDateTimeResouce))]
        [UiDateTimeDisplayAttribute("LocalTime", "BeginTime", typeof(Resources.UiDateTimeResouce))]
        public UiDateTimeModel UiDateTime { get; set; }
    }
}

