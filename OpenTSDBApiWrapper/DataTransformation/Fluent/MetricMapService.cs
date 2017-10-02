using EnsureThat;
using System.Collections.Generic;

namespace OpenTSDBApiWrapper.DataTransformation.Fluent
{
    public class MetricMapService : IMetricMapService
    {
        public IEnumerable<MetricData> Map<T>(MetricMap<T> map, IEnumerable<T> source) where T : class
        {
            EnsureArg.IsNotNull(map, nameof(map));
            EnsureArg.IsNotNull(source, nameof(source));

            foreach (var item in source)
            {
                foreach(var metric in map.Evaluate(item))
                {
                    yield return metric;
                }
            }
        }
    }
}
