using System;
using System.Collections.Generic;

namespace OpenTSDBApiWrapper {
    public interface ITransform {
        MetricData Transform(object data);
        IEnumerable<MetricData> TransformToMany(object data);
        void AddConfiguration(Func<MetricData, MetricData> func);
    }
}