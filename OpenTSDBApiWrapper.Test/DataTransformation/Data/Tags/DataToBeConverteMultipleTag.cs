using OpenTSDBApiWrapper.CustomAttributes;

namespace OpenTSDBApiWrapper.Test {
    public class DataToBeConverteMultipleTag {

        [IsMetricTag("opentag")]
        public string TagForMetric { get; set; }

        [IsMetricTag("opentag1")]
        public string TagForMetric1 { get; set; }
    }
}