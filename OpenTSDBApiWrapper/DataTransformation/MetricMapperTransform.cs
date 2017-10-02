using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace OpenTSDBApiWrapper {
    public class MetricMapperTransform : ITransform {
        private readonly List<Func<MetricData, MetricData>> dataConfiguration;

        public MetricMapperTransform() {
            dataConfiguration = new List<Func<MetricData, MetricData>>();
        }

        public void AddConfiguration(Func<MetricData, MetricData> func) {
            dataConfiguration.Add(func);
        }

        public MetricData Transform(object data) {
            var metricData = Mapper.Map<object, MetricData>(data);
            if (!dataConfiguration.Any()) {
                return metricData;
            }
            return ConfigureMetric(metricData);
        }

        public IEnumerable<MetricData> TransformToMany(object data) {
            var metricDataCollection = Mapper.Map<object, IEnumerable<MetricData>>(data);

            foreach (var metricData in metricDataCollection) {
                ConfigureMetric(metricData);
                yield return metricData;
            }
        }

        private MetricData ConfigureMetric(MetricData metricData) {
            foreach (var func in dataConfiguration) {
                return func.Invoke(metricData);
            }
            return metricData;
        }
    }
}