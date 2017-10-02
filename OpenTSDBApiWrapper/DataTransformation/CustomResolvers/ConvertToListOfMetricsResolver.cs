using System.Collections.Generic;
using AutoMapper;

namespace OpenTSDBApiWrapper {
    public class ConvertToListOfMetricsResolver  {
        public IEnumerable<MetricData> Resolve(object source, IEnumerable<MetricData> destination, object destMember, ResolutionContext context) {
            var values = new MetricValuesArrayCustomResolver();
            var keyValuePairOfVAlues = values.Resolve(source, null, null, context);
            var metricDataList = new List<MetricData>();
            foreach (var keyValuePairOfVAlue in keyValuePairOfVAlues)
            {
                var metricData = Mapper.Map<object, MetricData>(source);
                metricData.Metric = metricData.Metric.Insert(0, $"{keyValuePairOfVAlue.Key}.");
                metricData.Value = keyValuePairOfVAlue.Value;
                metricDataList.Add(metricData);
            }

            return metricDataList;
        }
    }
}