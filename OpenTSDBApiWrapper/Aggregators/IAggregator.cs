using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace OpenTSDBApiWrapper {
    public interface IAggregator {
        /// <summary>
        ///     Available aggregates on Opentsdb
        /// </summary>
        /// <returns>A readonly list with the values that are available for us to do the aggregations </returns>
        [Get("/api/aggregators")]
        Task<IReadOnlyList<string>> Available();
    }
}