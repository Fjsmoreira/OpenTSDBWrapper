using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenTSDBApiWrapper {
    public class RollupData {
        [JsonProperty("metric")]
        public string Metric { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("tags")]
        public Dictionary<string, string> Tags { get; set; }

        [JsonProperty("interval")]
        public string Interval { get; set; }

        [JsonProperty("aggregator")]
        public string Aggregator { get; set; }

        [JsonProperty("groupByAggregator")]
        public string GroupByAggregator { get; set; }
    }
}