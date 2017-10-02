using OpenTSDBApiWrapper.CustomAttributes;

namespace OpenTSDBApiWrapper.Test {
    public class DataToBeConvertedMultipleMetricNamesOrdered {

        [IsMetricName(1)]
        public string NameOfMetric1 { get; set; }
        [IsMetricName(2)]
        public string NameOfMetric2 { get; set; }
        [IsMetricName(3)]
        public string NameOfMetric3 { get; set; }

    }
}
