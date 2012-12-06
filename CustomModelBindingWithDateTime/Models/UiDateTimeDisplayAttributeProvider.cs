using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomModelBindingWithDateTime.Models
{
    public class UiDateTimeDisplayAttributeProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            var additionalValues = attributes.OfType<UiDateTimeDisplayAttribute>().ToList();
            if(additionalValues.Any())
            {
                metadata.AdditionalValues.Add("UiDateTimeDisplayAttribute", additionalValues);
            }
            return metadata;
        }
    }


    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class UiDateTimeDisplayAttribute : Attribute
    {
        public string DisplayName { get; set; }
        public string PropertyPath { get; set; }

        private readonly object _typeId = new object();
        public override object TypeId
        {
            get
            {
                return _typeId;
            }
        }
    }

}