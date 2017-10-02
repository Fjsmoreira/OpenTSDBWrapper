using OpenTSDBApiWrapper.CustomAttributes;

namespace OpenTSDBApiWrapper.Test {
    public class DataToBeConvertedMultipleValues {

        [IsMetricValue]
        public double Value { get; set; }

        [IsMetricValue]
        public double Value1 { get; set; }

        [IsMetricValue]
        public double Value2 { get;set; }
    }
}