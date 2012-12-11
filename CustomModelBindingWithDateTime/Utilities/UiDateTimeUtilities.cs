using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using CustomModelBindingWithDateTime.Models;

namespace CustomModelBindingWithDateTime.Utilities
{
    public class UiDateHelperValidationAttribute
    {
        public string PropertyName { get; set; }
        public ModelMetadata Metadata { get; set; }
        public IDictionary<string, object> Attributes { get; set; }
    }

    public static class UiDateTimeUtilities
    {
        private static readonly IList<UiDateHelperValidationAttribute> StoredValidationAttributes = new List<UiDateHelperValidationAttribute>();

        public static IList<KeyValuePair<string, object>> ChildValidationAttributes(HtmlHelper htmlHelper, string basePropertyName, string targetPropertyName, ModelMetadata metadata)
        {
            var attributeKeyPropertyPath = targetPropertyName.ToLower().Replace(".", "");

            IDictionary<string, object> validationAttributes;
            if (StoredValidationAttributes.Any(q => q.PropertyName == basePropertyName && q.Metadata == metadata))
            {
                validationAttributes = StoredValidationAttributes.First(q => q.PropertyName == basePropertyName && q.Metadata == metadata).Attributes;
            }
            else
            {
                validationAttributes = htmlHelper.GetUnobtrusiveValidationAttributes(basePropertyName, metadata);
                StoredValidationAttributes.Add(new UiDateHelperValidationAttribute { PropertyName = basePropertyName, Metadata = metadata, Attributes = validationAttributes });
            }
            var associatedValidationAttributes = validationAttributes.Where(attr => attr.Key.EndsWith(attributeKeyPropertyPath) || attr.Key.Contains(attributeKeyPropertyPath + "-")).ToList();
            if (associatedValidationAttributes.Any())
            {
                var dataValAttribute = validationAttributes.FirstOrDefault(attr => attr.Key == "data-val");
                associatedValidationAttributes.Add(dataValAttribute);
            }

            return associatedValidationAttributes;
        }

        public static RouteValueDictionary AddViewDataHtmlAttributes(HtmlHelper htmlHelper, string basePropertyName, RouteValueDictionary attributes)
        {
            var objectNotationPropertyPath = basePropertyName.Replace(".", "_");
            var viewDataAttributes = htmlHelper.ViewData[objectNotationPropertyPath];
            if (viewDataAttributes != null)
            {
                foreach (System.ComponentModel.PropertyDescriptor property in System.ComponentModel.TypeDescriptor.GetProperties(viewDataAttributes))
                {
                    if (attributes[property.Name] != null)
                    {
                        attributes[property.Name] = (String)attributes[property.Name] + " " + (String)property.GetValue(viewDataAttributes);
                    }
                    else
                    {
                        attributes.Add(property.Name, property.GetValue(viewDataAttributes));
                    }
                }
            }
            return attributes;
        }

        public static Object ChildObjectFromValidationContext(string propertyPath, ValidationContext validationContext)
        {
            var properties = propertyPath.Split('.');

            var memberName = validationContext.ObjectType.GetProperties()
                                                .Where(p => p.GetCustomAttributes(false)
                                                .OfType<UiDateTimeDisplayAttribute>().Any(a => a.DisplayName == validationContext.DisplayName)).Select(p => p.Name).FirstOrDefault();

            //Get PropertyInfo Object  
            var basePropertyInfo = validationContext.ObjectType.GetProperty(memberName);

            //Get Value of the property
            var propValue = basePropertyInfo.GetValue(validationContext.ObjectInstance, null);

            return properties.Aggregate(propValue, DataBinder.Eval);
        }

        public static string GetPropertyDisplayNameFromValidationContext(string propertyPath, ValidationContext validationContext)
        {
            var properties = propertyPath.Split('.');

            var metadata = ModelMetadataProviders.Current.GetMetadataForProperty(null, validationContext.ObjectType, validationContext.DisplayName);

            //Attempt to query the UIDateTimeDisplayAttribute
            if (metadata != null && metadata.AdditionalValues.Any(q => q.Key == UiDateTimeDisplayAttributeProvider.UiDateTimeDisplayAttributeKey && ((IList<UiDateTimeDisplayAttribute>)q.Value).Any(s => s.PropertyPath == propertyPath)))
            {
                var additionalValues = metadata.AdditionalValues.First(q => q.Key == UiDateTimeDisplayAttributeProvider.UiDateTimeDisplayAttributeKey);
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

        public static string GetPropertyDisplayNameFromModelMetadata(string propertyPath, ModelMetadata metadata)
        {
            var properties = propertyPath.Split('.');

            //Attempt to query the UIDateTimeDisplayAttribute
            if (metadata != null && metadata.AdditionalValues.Any(q => q.Key == UiDateTimeDisplayAttributeProvider.UiDateTimeDisplayAttributeKey && ((IList<UiDateTimeDisplayAttribute>)q.Value).Any(s => s.PropertyPath == propertyPath)))
            {
                var additionalValues = metadata.AdditionalValues.First(q => q.Key == UiDateTimeDisplayAttributeProvider.UiDateTimeDisplayAttributeKey);
                var displayName = ((IList<UiDateTimeDisplayAttribute>)additionalValues.Value).First(q => q.PropertyPath == propertyPath).DisplayName;
                return displayName;
            }

            //Find the display name on the property
            var metadataList = ModelMetadataProviders.Current.GetMetadataForProperties(null, metadata.ModelType).ToList();
            foreach (var prop in properties)
            {
                if (metadataList.Any(q => q.PropertyName == prop))
                {
                    metadata = ModelMetadataProviders.Current.GetMetadataForProperty(null, metadata.ModelType, prop);
                    metadataList = ModelMetadataProviders.Current.GetMetadataForProperties(null, metadata.ModelType).ToList();
                }
            }
            return metadata.GetDisplayName();
        }
    }
}