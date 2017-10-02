using OpenTSDBApiWrapper.CustomAttributes;

namespace OpenTSDBApiWrapper.Test {
    public class DataToBeConvertedSingleTag {

        [IsMetricTag("opentag")]
        public string TagForMetric { get; set; }

    }
}