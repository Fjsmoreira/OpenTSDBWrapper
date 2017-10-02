using OpenTSDBApiWrapper.CustomAttributes;

namespace OpenTSDBApiWrapper.Test {
    public class DataToBeConvertedSingleValue {

        [IsMetricValue]
        public double Value { get; set; }

    }
}