using System.Reflection;

namespace OpenTSDBApiWrapper
{
    public class MetricValueCustomCacheItem
    {
        public PropertyInfo FirstMetricValue { get; set; }

        public bool HasArrayFields { get; set; }
    }
}