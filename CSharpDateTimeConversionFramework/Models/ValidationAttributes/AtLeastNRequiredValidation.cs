using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CSharpDateTimeConversionFramework.Models.ValidationAttributes
{
    public class AtLeastNRequiredValidation : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessage = "Check at least {1} {0}.";

        private int _requiredNumber;

        public AtLeastNRequiredValidation(int requiredNumber)
            : base(DefaultErrorMessage)
        {
            _requiredNumber = requiredNumber;
        }

        //Override default FormatErrorMessage Method  
        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, _requiredNumber);
        }

        //Override IsValid  
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // none of the elements are used in the cast, so a generic list is not required;
            // when used on a property of type List<string>, using IList<object> results
            // in items being null
            var items = value as IList; //<object>;
            if (items == null || items.Count <= 0)
            {
                var message = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(message);
            }

            //Default return - This means there were no validation error  
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());

            //This string identifies which Javascript function to be executed to validate this   
            rule.ValidationType = "atleastnrequired";
            rule.ValidationParameters.Add("count", _requiredNumber);
            yield return rule;
        }

    }
}