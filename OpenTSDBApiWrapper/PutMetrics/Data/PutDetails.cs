using Newtonsoft.Json;

namespace OpenTSDBApiWrapper.PutMetrics.Data {
    public class PutDetails : PutSummary {
        [JsonProperty("errors")]
        public PutErrors[] Errors { get; set; }
    }
}