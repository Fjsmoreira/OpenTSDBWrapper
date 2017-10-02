using Newtonsoft.Json;

namespace OpenTSDBApiWrapper {
    public class PutErrors {
        [JsonProperty("datapoint")]
        public MetricData DataPoint { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }
}