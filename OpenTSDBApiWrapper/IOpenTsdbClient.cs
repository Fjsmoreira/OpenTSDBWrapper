using System.Collections.Generic;

namespace OpenTSDBApiWrapper
{
    public interface IOpenTsdbClient
    {
        IAggregator Aggregator { get; }
        IAnnotation Annotation { get; }
        IConfig Config { get; }
        IDropCaches DropCaches { get; }
        IPutMetric Put { get; }
        IQuery Query { get; }
        IRollup RollUp { get; }

        MetricData Transform(object data);
        IEnumerable<MetricData> TransformToMany(object data);
    }
}