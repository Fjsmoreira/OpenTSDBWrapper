using OpenTSDBApiWrapper.CustomAttributes;

namespace OpenTSDBApiWrapper.Test
{
    public class DataToBeConvertedNoOrderInMetricNames
    {

        [IsMetricName]
        public string NameOfMetric1 { get; set; }
        [IsMetricName]
        public string NameOfMetric2 { get; set; }
        [IsMetricName]
        public string NameOfMetric3 { get; set; }

    }
}
