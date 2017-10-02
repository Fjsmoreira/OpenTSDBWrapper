using Newtonsoft.Json;

namespace OpenTSDBApiWrapper {
    public class AnnotationDeleteData {
        [JsonProperty("start_time")]
        public string StartTime { get; set; }

        [JsonProperty("end_time")]
        public string EndTime { get; set; }

        [JsonProperty("method_override")]
        public string MethodOverride { get; set; }

        [JsonProperty("tsuids")]
        public string[] Tsuid { get; set; }
    }
}