using Newtonsoft.Json;

namespace OpenTSDBApiWrapper {
    public class AnnotationDeleteBulkResponse {
        [JsonProperty("tsuids")]
        public string[] Tsuids { get; set; }

        [JsonProperty("global")]
        public bool Global { get; set; }

        [JsonProperty("startTime")]
        public long StartTime { get; set; }

        [JsonProperty("endTime")]
        public long EndTime { get; set; }

        [JsonProperty("totalDeleted")]
        public int TotalDeleted { get; set; }
    }
}