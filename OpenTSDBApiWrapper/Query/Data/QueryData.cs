using Newtonsoft.Json;

namespace OpenTSDBApiWrapper.Query
{
    /// <summary>
    /// Details for the property names can be found here :
    /// http://opentsdb.net/docs/build/html/api_http/query/index.html#index-0
    /// </summary>
    public class QueryData
    {
        /// <summary>
        ///     The start time for the query. This can be a relative or absolute timestamp
        ///     http://opentsdb.net/docs/build/html/user_guide/query/index.html
        /// </summary>
        [JsonProperty("start")]
        public string Start { get; set; }

        /// <summary>
        ///     An end time for the query. If not supplied, the TSD will assume the local system time on the server.
        ///     This may be a relative or absolute timestamp
        ///     http://opentsdb.net/docs/build/html/user_guide/query/index.html
        /// </summary>
        [JsonProperty("end")]
        public string End { get; set; }

        /// <summary>
        ///     One or more sub queries used to select the time series to return.
        ///     These may be metric m or TSUID tsuids queries
        /// </summary>
        [JsonProperty("queries")]
        public SubQueryData[] SubQueries { get; set; }

        /// <summary>
        ///     Whether or not to return annotations with a query.
        ///     The default is to return annotations for the requested timespan but this flag can disable the return.
        ///     This affects both local and global notes and overrides globalAnnotations
        /// </summary>
        [JsonProperty("noAnnotations")]
        public bool NoAnnotations { get; set; }

        /// <summary>
        ///     Whether or not the query should retrieve global annotations for the requested timespan
        /// </summary>
        [JsonProperty("globalAnnotations")]
        public bool GlobalAnnotations { get; set; }

        /// <summary>
        ///     Whether or not to output data point timestamps in milliseconds or seconds.
        ///     The msResolution flag is recommended.
        ///     If this flag is not provided and there are multiple data points within a second, those data points will be down
        ///     sampled using the query's aggregation function.
        /// </summary>
        [JsonProperty("msResolution")]
        public bool MsResolutions { get; set; }

        /// <summary>
        ///     Whether or not to output the TSUIDs associated with timeseries in the results.
        ///     If multiple time series were aggregated into one set, multiple TSUIDs will be returned in a sorted manner
        /// </summary>
        [JsonProperty("showTSUIDs")]
        public bool ShowTsuids { get; set; }

        /// <summary>
        ///     Whether or not to show a summary of timings surrounding the query in the results.
        ///     This creates another object in the map that is unlike the data point objects.
        ///     http://opentsdb.net/docs/build/html/user_guide/query/stats.html
        /// </summary>
        [JsonProperty("showSummary")]
        public bool ShowSummary { get; set; }

        /// <summary>
        ///     Whether or not to show detailed timings surrounding the query in the results.
        ///     This creates another object in the map that is unlike the data point objects
        ///     http://opentsdb.net/docs/build/html/user_guide/query/stats.html
        /// </summary>
        [JsonProperty("showStats")]
        public bool ShowStats { get; set; }

        /// <summary>
        ///     Whether or not to return the original sub query with the query results.
        ///     If the request contains many sub queries then this is a good way to determine which results belong to which sub
        ///     query.
        ///     Note that in the case of a * or wildcard query, this can produce a lot of duplicate output.
        /// </summary>
        [JsonProperty("showQuery")]
        public bool ShowQuery { get; set; }

        /// <summary>
        ///     Can be passed to the JSON with a POST to delete any data points that match the given query.
        /// </summary>
        [JsonProperty("delete")]
        public bool Delete { get; set; }

        /// <summary>
        ///     An optional timezone for calendar-based downsampling.
        ///     Must be a valid timezone database name supported by the JRE installed on the TSD server.
        ///     https://en.wikipedia.org/wiki/List_of_tz_database_time_zones
        /// </summary>
        [JsonProperty("timezone")]
        public string TimeZone { get; set; }

        /// <summary>
        ///     Whether or not use the calendar based on the given timezone for downsampling intervals
        /// </summary>
        [JsonProperty("useCalendar")]
        public bool UseCalendar { get; set; }
    }
}