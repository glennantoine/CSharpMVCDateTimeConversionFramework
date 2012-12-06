using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.UI;

namespace CustomModelBindingWithDateTime.Models.ValidationAttributes
{
    public class DateNotInPastValidation : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessage = "'{0}' must be in the future.'";
        private readonly string _basePropertyName;
        private string _basePropertyDisplayName;

        public DateNotInPastValidation(string basePropertyName)
            : base(DefaultErrorMessage)
        {
            _basePropertyName = basePropertyName;
        }

        //Override default FormatErrorMessage Method  
        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name);
        }

        //Override IsValid  
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {

                var properties = _basePropertyName.Split('.');

                //Get PropertyInfo Object  
                var basePropertyInfo = validationContext.ObjectType.GetProperty(properties[0]);

                //Get Value of the property
                var propValue = basePropertyInfo.GetValue(validationContext.ObjectInstance, null);

                for(var i = 1; i<properties.Length; i++)
                {
                    propValue = DataBinder.Eval(propValue, properties[i]);
                }
                var propDate = new DateTime();
                if (propValue != null)
                {
                    propDate = DateTime.Parse(propValue.ToString());
                    DisplayAttribute attr = (DisplayAttribute)GetCustomAttribute(propValue.GetType(), typeof(DisplayAttribute));
                    _basePropertyDisplayName = attr.Name;
                }

                //Actual comparision  
                if (DateTime.UtcNow.Date > propDate.Date)
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
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());


            Type type = Type.GetType(metadata.ContainerType.FullName);

            var model = Activator.CreateInstance(type);
            var provider = new DataAnnotationsModelMetadataProvider();
            var otherMetaData = provider.GetMetadataForProperty(() => model, type, _basePropertyName);

            _basePropertyDisplayName = otherMetaData.DisplayName;

            //This string identifies which Javascript function to be executed to validate this   
            rule.ValidationType = "datenotinpast";
            yield return rule;
        }

    }
}