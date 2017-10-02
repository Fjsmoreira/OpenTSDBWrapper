using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenTSDBApiWrapper {
    public class AnnotationData {
        [JsonProperty("startTime")]
        public long StartTime { get; set; }

        [JsonProperty("endTime")]
        public long EndTime { get; set; }

        [JsonProperty("tsuid")]
        public string Tsuid { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("custom")]
        public Dictionary<string, string> Custom { get; set; }
    }
}