using System;
using System.Collections.Concurrent;
using System.Linq;
using AutoMapper;
using OpenTSDBApiWrapper.CustomAttributes;
using OpenTSDBApiWrapper.Exceptions;

namespace OpenTSDBApiWrapper
{
    public class MetricTimeCustomResolver : IValueResolver<object, MetricData, long>
    {
        private static readonly ConcurrentDictionary<Type, MetricTimeCustomCacheItem> CachedMetricValue =
            new ConcurrentDictionary<Type, MetricTimeCustomCacheItem>();


        public long Resolve(object source, MetricData destination, long destMember, ResolutionContext context)
        {
            Type sourceType = source.GetType();
            var cacheItem = GetUpdateCacheItem(sourceType);

            if (cacheItem.FirstMetricTime == null)
            {
                throw new MissingAttributeException(nameof(IsMetricTime));
            }

            var metricTime = DateTime.Parse(cacheItem.FirstMetricTime.GetValue(source).ToString());

            return GetUnixTimeStamp(metricTime);
        }

        private static MetricTimeCustomCacheItem GetUpdateCacheItem(Type sourceType)
        {
            MetricTimeCustomCacheItem cacheItem;

            if (CachedMetricValue.TryGetValue(sourceType, out cacheItem))
            {
                return cacheItem;
            }
        
            cacheItem = new MetricTimeCustomCacheItem
            {
                FirstMetricTime = sourceType.GetProperties()
                    .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(IsMetricTime)))
            };

            CachedMetricValue.TryAdd(sourceType, cacheItem);

            return cacheItem;
        }

        private static long GetUnixTimeStamp(DateTime timeToConvert)
        {
            var epochTicks = new TimeSpan(new DateTime(1970, 1, 1).Ticks);
            var unixTicks = new TimeSpan(timeToConvert.Ticks) - epochTicks;
            return (long) unixTicks.TotalSeconds;
        }
    }
}