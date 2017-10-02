using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using OpenTSDBApiWrapper.CustomAttributes;
using OpenTSDBApiWrapper.Exceptions;

namespace OpenTSDBApiWrapper
{
    public class MetricTagsCustomResolver : IValueResolver<object, MetricData, Dictionary<string, string>>
    {
        private static readonly ConcurrentDictionary<Type, List<MetricTagProperty>> CachedAccessors =
            new ConcurrentDictionary<Type, List<MetricTagProperty>>();
        
        public Dictionary<string, string> Resolve(
            object source, MetricData destination, 
            Dictionary<string, string> destMember,
            ResolutionContext context)
        {
            destMember = new Dictionary<string, string>();

            Type sourceType = source.GetType();
            var accessors = GetUpdateCacheItem(sourceType);

            if (accessors?.Any() != true) {
                throw new MissingAttributeException(nameof(IsMetricTag));
            }

            foreach (var item in accessors) {                
                var tagValue = item.Property.GetValue(source)?.ToString() ?? string.Empty;

                if (tagValue.Trim().Contains(" "))
                {
                    tagValue = ValidatorHelper.ToTitleCase(tagValue);
                }

                tagValue = ValidatorHelper.TagAndMetricNameValidator(tagValue);
                if (string.IsNullOrEmpty(tagValue)) continue;

                destMember.Add(item.CustomAttribute.Name, tagValue);
            }

            return destMember;
        }

        private static List<MetricTagProperty> GetUpdateCacheItem(Type sourceType)
        {
            List<MetricTagProperty> accessors;

            if (CachedAccessors.TryGetValue(sourceType, out accessors))
            {
                return accessors;
            }
            
            accessors = sourceType.GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(IsMetricTag)))
                .Select(propertyInfo => new MetricTagProperty
                {
                    Property = propertyInfo,
                    CustomAttribute = (IsMetricTag) propertyInfo.GetCustomAttribute(typeof(IsMetricTag)),
                })
                .ToList();

            CachedAccessors.TryAdd(sourceType, accessors);

            return accessors;
        }
    }

    public class MetricTagProperty
    {
        public PropertyInfo Property { get; set; }

        public IsMetricTag CustomAttribute { get; set; }        
    }    
}