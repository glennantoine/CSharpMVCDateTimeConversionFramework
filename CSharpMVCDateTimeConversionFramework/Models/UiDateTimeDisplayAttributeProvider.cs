using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web.Mvc;

namespace CSharpMVCDateTimeConversionFramework.Models
{
    public class UiDateTimeDisplayAttributeProvider : DataAnnotationsModelMetadataProvider
    {
        public const string UiDateTimeDisplayAttributeKey = "UiDateTimeDisplayAttribute";

        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            var additionalValues = attributes.OfType<UiDateTimeDisplayAttribute>().ToList();
            if (additionalValues.Any())
            {
                metadata.AdditionalValues.Add(UiDateTimeDisplayAttributeKey, additionalValues);
            }
            return metadata;
        }
    }


    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class UiDateTimeDisplayAttribute : DisplayNameAttribute
    {
        private readonly ResourceManager _resourceManager;
        private readonly object _typeId = new object();

        public UiDateTimeDisplayAttribute(string propertyPath, string displayNameKey, Type resourceType)
            : base(displayNameKey)
        {
            PropertyPath = propertyPath;
            if (resourceType != null)
            {
                _resourceManager = new ResourceManager(resourceType);
            }
        }

        public override string DisplayName
        {
            get
            {
                if (_resourceManager == null)
                {
                    return base.DisplayName;
                }
                return _resourceManager.GetString(base.DisplayName);
            }
        }

        public string PropertyPath { get; set; }

        public override object TypeId
        {
            get { return _typeId; }
        }
    }
}