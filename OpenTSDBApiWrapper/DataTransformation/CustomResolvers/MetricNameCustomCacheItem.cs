using System.Collections.Generic;
using System.Reflection;
using OpenTSDBApiWrapper.CustomAttributes;

namespace OpenTSDBApiWrapper
{
    public class MetricNameCustomCacheItem
    {
        public bool HasMetricNames { get; set; }

        public List<PropertyInfoWithFirstNameAttribute> MetricInfos { get; set; }
    }

    public class PropertyInfoWithFirstNameAttribute
    {
        public PropertyInfo MetricProperty { get; set; }

        public IsMetricName FirstNameAttribute { get; set; }

    }
}