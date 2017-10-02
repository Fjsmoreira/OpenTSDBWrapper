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
    public class MetricValuesArrayCustomResolver : IValueResolver<object, MetricData, IEnumerable<KeyValuePair<string, double>>>
    {
        private static readonly ConcurrentDictionary<Type, MetricValuesArrayCacheItem> CacheItems =
            new ConcurrentDictionary<Type, MetricValuesArrayCacheItem>();

        public IEnumerable<KeyValuePair<string, double>> Resolve(object source, MetricData destination, IEnumerable<KeyValuePair<string, double>> destMember, ResolutionContext context)
        {
            Type sourceType = source.GetType();

            var cacheItem = GetUpdateCacheItem(sourceType);

            var value = cacheItem.MetricArrayProperties.FirstOrDefault()?.GetValue(source);

            if (value != null)
            {
                if (!cacheItem.MetricInfoFromArray.Any())
                {
                    throw new MissingAttributeException(nameof(IsMetricValue));
                }
                
                foreach (var metricInfo in cacheItem.MetricInfoFromArray)
                {                    
                    if (metricInfo.MetricName == null)
                    {
                        throw new MissingAttributeException(nameof(IsMetricName));
                    }

                    yield return new KeyValuePair<string, double>(metricInfo.MetricName.Name, double.Parse(metricInfo.Property.GetValue(value).ToString()));
                }                
            }
            else
            {
                value = source;

                if (!cacheItem.MetricValuesProperties.Any())
                {
                    throw new MissingAttributeException(nameof(IsMetricValue));
                }
                
                yield return new KeyValuePair<string, double>(string.Empty, double.Parse(cacheItem.MetricValuesProperties.First().GetValue(value).ToString()));                
            }
        }

        private static MetricValuesArrayCacheItem GetUpdateCacheItem(Type sourceType)
        {
            MetricValuesArrayCacheItem cacheItem;

            if (CacheItems.TryGetValue(sourceType, out cacheItem))
            {
                return cacheItem;
            }

            var properties = sourceType.GetProperties();

            cacheItem = new MetricValuesArrayCacheItem
            {
                MetricArrayProperties = properties.Where(prop => Attribute.IsDefined(prop, typeof(IsMetricValueArray)))
                    .ToList(),
                MetricValuesProperties =
                    properties.Where(prop => Attribute.IsDefined(prop, typeof(IsMetricValue))).ToList(),
            };

            cacheItem.MetricInfoFromArray = GetMetricPropertiesFromArrayProperty(cacheItem.MetricArrayProperties);

            CacheItems.TryAdd(sourceType, cacheItem);

            return cacheItem;
        }

        private static List<MetricValueInfo> GetMetricPropertiesFromArrayProperty(
            List<PropertyInfo> metricValuesArray)
        {
            var metricInfos = new List<MetricValueInfo>();
            
            foreach (var propertyInfo in metricValuesArray)
            {
                metricInfos.AddRange(propertyInfo.PropertyType.GetProperties()
                    .Where(prop => Attribute.IsDefined(prop, typeof(IsMetricValue)))
                    .Select(prop => new MetricValueInfo
                    {
                        Property = prop,
                        MetricName = (IsMetricName)prop.GetCustomAttribute(typeof(IsMetricName)),
                    }));
            }

            return metricInfos;
        }
    }
}