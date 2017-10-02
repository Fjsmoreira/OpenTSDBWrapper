using System.Collections.Generic;

namespace OpenTSDBApiWrapper.DataTransformation.Fluent
{
    public interface IMetricMapService
    {
        IEnumerable<MetricData> Map<T>(MetricMap<T> map, IEnumerable<T> source) where T : class;
    }
}