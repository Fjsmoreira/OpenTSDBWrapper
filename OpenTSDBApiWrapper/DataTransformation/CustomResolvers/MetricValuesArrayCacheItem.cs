using System.Collections.Generic;
using System.Reflection;
using OpenTSDBApiWrapper.CustomAttributes;

namespace OpenTSDBApiWrapper
{
    public class MetricValuesArrayCacheItem
    {
        public List<PropertyInfo> MetricArrayProperties { get; set; }

        public List<PropertyInfo> MetricValuesProperties { get; set; }

        public List<MetricValueInfo> MetricInfoFromArray { get; set; }
    }

    public class MetricValueInfo
    {
        public PropertyInfo Property { get; set; }

        public IsMetricName MetricName { get; set; }
    }
}