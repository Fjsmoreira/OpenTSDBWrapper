using System;
using OpenTSDBApiWrapper.CustomAttributes;

namespace OpenTSDBApiWrapper.Test {
    public class DataToBeConvertedSingleMetricName {
        [IsMetricName(2)]
        public string NameOfMetric { get; set; }

        [IsMetricTag("opentsdbTag")]
        public string TagForMetric { get; set; }

        [IsMetricValue]
        public double ValueOfMetric { get; set; }

        [IsMetricTime]
        public DateTime Timestamp { get; set; }
    }
}