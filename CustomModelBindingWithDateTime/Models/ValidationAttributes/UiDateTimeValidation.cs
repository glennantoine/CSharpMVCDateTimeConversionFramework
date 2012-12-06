using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;

namespace CustomModelBindingWithDateTime.Models.ValidationAttributes
{
    public static class UiDateTimeValidation
    {
        public const string DateNotInPastDefaultErrorMessage = "'{0}' must be in the future.'";

        public static Object ChildObjectFromValidationContext(string propertyPath, ValidationContext validationContext)
        {
            var properties = propertyPath.Split('.');

            //Get PropertyInfo Object  
            var basePropertyInfo = validationContext.ObjectType.GetProperty(validationContext.DisplayName);

            //Get Value of the property
            var propValue = basePropertyInfo.GetValue(validationContext.ObjectInstance, null);

            return properties.Aggregate(propValue, (current, t) => DataBinder.Eval(current, t));
        }

        public static string GetPropertyDisplayNameFromValidationContext(string propertyPath, ValidationContext validationContext)
        {
            var properties = propertyPath.Split('.');

            var metadata = ModelMetadataProviders.Current.GetMetadataForProperty(null, validationContext.ObjectType, validationContext.DisplayName);

            //Attempt to query the UIDateTimeDisplayAttribute
            if (metadata != null && metadata.AdditionalValues.Any(q => q.Key == "UiDateTimeDisplayAttribute" && ((IList<UiDateTimeDisplayAttribute>)q.Value).Any(s => s.PropertyPath == propertyPath)))
            {
                var additionalValues = metadata.AdditionalValues.First(q => q.Key == "UiDateTimeDisplayAttribute");
                var displayName = ((IList<UiDateTimeDisplayAttribute>) additionalValues.Value).First(q => q.PropertyPath == propertyPath).DisplayName;
                return displayName;
            }

            //Find the display name on the property
            var metadataList = ModelMetadataProviders.Current.GetMetadataForProperties(null, metadata == null ? validationContext.ObjectType : metadata.ModelType).ToList();
            for (var i = 0; i < properties.Length && metadata != null; i++)
            {
                if (metadataList.Any(q => q.PropertyName == properties[i]))
                {
                    metadata = ModelMetadataProviders.Current.GetMetadataForProperty(null, metadata.ModelType, properties[i]);
                    metadataList = ModelMetadataProviders.Current.GetMetadataForProperties(null, metadata.ModelType).ToList();
                }
            }
            return metadata != null ? metadata.GetDisplayName() : properties.LastOrDefault();
        }
    }
}