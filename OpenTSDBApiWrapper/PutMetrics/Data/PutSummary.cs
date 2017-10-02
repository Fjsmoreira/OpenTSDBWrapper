using Newtonsoft.Json;

namespace OpenTSDBApiWrapper.PutMetrics.Data {
    public class PutSummary {
        [JsonProperty("success")]
        public int Success { get; set; }

        [JsonProperty("failed")]
        public int Failed { get; set; }
    }
}