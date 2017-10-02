using Newtonsoft.Json;

namespace OpenTSDBApiWrapper {
    public class DropCachesData {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
    }
}