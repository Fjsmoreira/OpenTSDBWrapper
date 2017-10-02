using System;
using System.Collections.Concurrent;
using System.Linq;
using AutoMapper;
using OpenTSDBApiWrapper.CustomAttributes;
using OpenTSDBApiWrapper.Exceptions;

namespace OpenTSDBApiWrapper
{
    public class MetricValueCustomResolver : IValueResolver<object, MetricData, double>
    {
        private static readonly ConcurrentDictionary<Type, MetricValueCustomCacheItem> CachedMetricValue =
            new ConcurrentDictionary<Type, MetricValueCustomCacheItem>();


        public double Resolve(object source, MetricData destination, double destMember, ResolutionContext context)
        {
            var sourceType = source.GetType();

            var cacheItem = GetUpdateCacheItem(sourceType);
            
            if (cacheItem.HasArrayFields) return double.NaN;

            if (cacheItem.FirstMetricValue == null)
            {
                throw new MissingAttributeException(nameof(IsMetricValue));
            }

            return double.Parse(cacheItem.FirstMetricValue.GetValue(source).ToString());
        }

        private static MetricValueCustomCacheItem GetUpdateCacheItem(Type sourceType)
        {
            MetricValueCustomCacheItem cacheItem;

            if (CachedMetricValue.TryGetValue(sourceType, out cacheItem))
            {
                return cacheItem;                
            }

            var properties = sourceType.GetProperties();

            cacheItem = new MetricValueCustomCacheItem()
            {
                FirstMetricValue = properties.FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(IsMetricValue))),
                HasArrayFields = properties.Any(prop => Attribute.IsDefined(prop, typeof(IsMetricValueArray))),
            };
            
            CachedMetricValue.TryAdd(sourceType, cacheItem);

            return cacheItem;
        }
    }
}