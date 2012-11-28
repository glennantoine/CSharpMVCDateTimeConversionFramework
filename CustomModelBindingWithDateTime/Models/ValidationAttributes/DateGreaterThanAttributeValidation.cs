using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CustomModelBindingWithDateTime.Models.ValidationAttributes
{
    public class DateGreaterThanAttributeOrNullValidation : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessage = "'{0}' must be greater than '{1}'";  
        private readonly string _basePropertyName;
        private readonly bool _allowEqual;
        private string _basePropertyDisplayName;

        public DateGreaterThanAttributeOrNullValidation(string basePropertyName, bool allowEqual = true)
            : base(DefaultErrorMessage)  
        {  
            _basePropertyName = basePropertyName;
            _allowEqual = allowEqual;
        }  
   
        //Override default FormatErrorMessage Method  
        public override string FormatErrorMessage(string name)  
        {
            return string.Format(ErrorMessageString, name, _basePropertyDisplayName);  
        }  
   
        //Override IsValid  
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)  
        {  
            if (value != null)
            {
                //Get PropertyInfo Object  
                var basePropertyInfo = validationContext.ObjectType.GetProperty(_basePropertyName);

                //Get Value of the property
                var propValue = basePropertyInfo.GetValue(validationContext.ObjectInstance, null);
                var startDate = new DateTime();
                if (propValue != null)
                {
                    startDate = DateTime.Parse(propValue.ToString());
                }

                var thisDate = DateTime.Parse(value.ToString());

                //Actual comparision  
                if (thisDate < startDate || (!_allowEqual && thisDate <= startDate))
                {
                    var message = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(message);
                }
            }

            //Default return - This means there were no validation error  
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            Type type = Type.GetType(metadata.ContainerType.FullName);
            var model = Activator.CreateInstance(type);

            var provider = new DataAnnotationsModelMetadataProvider();
            var otherMetaData = provider.GetMetadataForProperty(() => model, type, _basePropertyName);

            _basePropertyDisplayName = otherMetaData.DisplayName;

            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());

            //This string identifies which Javascript function to be executed to validate this   
            rule.ValidationType = "dategreaterthanattributeornull";
            rule.ValidationParameters.Add("other", _basePropertyName);
            rule.ValidationParameters.Add("allowequal", _allowEqual);
            yield return rule;
        }  

    }
}