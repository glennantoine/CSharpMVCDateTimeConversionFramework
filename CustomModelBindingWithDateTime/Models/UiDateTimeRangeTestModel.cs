﻿using System.Collections.Generic;
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

        [TimeValidation(ErrorMessageResourceName = "TimeFormatValid", ErrorMessageResourceType = typeof(Resources.Validation))]
        [UiDateTimeRangeValidator(new[] { UiDateTimeRangeValidationMode.StartDateNotInFuture }, ErrorMessageResourceName = "DateFormatValid", ErrorMessageResourceType = typeof(UiDateTimeRangeModel))]
        public UiDateTimeRangeModel UiDateTimeRange { get; set; }
    }
}
