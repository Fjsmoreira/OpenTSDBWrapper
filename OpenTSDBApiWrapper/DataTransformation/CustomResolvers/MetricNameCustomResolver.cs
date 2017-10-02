using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;
using OpenTSDBApiWrapper.CustomAttributes;
using OpenTSDBApiWrapper.Exceptions;

namespace OpenTSDBApiWrapper
{
    public class MetricNameCustomResolver : IValueResolver<object, MetricData, string>
    {
        private static readonly ConcurrentDictionary<Type, MetricNameCustomCacheItem> CachedMetricValue = 
            new ConcurrentDictionary<Type, MetricNameCustomCacheItem>();

        public string Resolve(object source, MetricData destination, string destMember, ResolutionContext context)
        {
            Type sourceType = source.GetType();

            var cacheItem = GetUpdateCacheItem(sourceType);
            
            if (!cacheItem.HasMetricNames)
            {
                throw new MissingAttributeException(nameof(IsMetricName));
            }

            var orderedMetric = OrderMetricByOrderAttribute(source, cacheItem.MetricInfos);

            return GetMetricName(orderedMetric);
        }

        private static MetricNameCustomCacheItem GetUpdateCacheItem(Type sourceType)
        {
            MetricNameCustomCacheItem cacheItem;

            if (CachedMetricValue.TryGetValue(sourceType, out cacheItem))
            {
                return cacheItem;
            }
            
            cacheItem = new MetricNameCustomCacheItem()
            {
                MetricInfos = sourceType.GetProperties()
                    .Where(prop => Attribute.IsDefined(prop, typeof(IsMetricName)))
                    .Select((_) => new PropertyInfoWithFirstNameAttribute
                    {
                        MetricProperty = _,
                        FirstNameAttribute = _.GetCustomAttributes().OfType<IsMetricName>().First()
                    })
                    .ToList(),
            };

            cacheItem.HasMetricNames = cacheItem.MetricInfos.Any();

            CachedMetricValue.TryAdd(sourceType, cacheItem);

            return cacheItem;
        }

        private static string GetMetricName(IEnumerable<object> orderedMetric) {
            var metricName = new StringBuilder();

            foreach (var oMetric in orderedMetric) {
                metricName.Append($"{oMetric}.");
            }

            metricName.Length--;

            var metric = metricName.ToString();

            if(metric.Trim().Contains(" ")) {
                metric = ValidatorHelper.ToTitleCase(metric);
            }

            return ValidatorHelper.TagAndMetricNameValidator(metric);

        }

        private static IEnumerable<object> OrderMetricByOrderAttribute(
            object source, 
            List<PropertyInfoWithFirstNameAttribute> metricinfos) {

            var orderedMetric = metricinfos.OrderBy(p => p.FirstNameAttribute.Order)
                                            .ThenBy(p => p.MetricProperty.Name)
                                            .Select(p => p.MetricProperty.GetValue(source));
            return orderedMetric;
        }
    }
}