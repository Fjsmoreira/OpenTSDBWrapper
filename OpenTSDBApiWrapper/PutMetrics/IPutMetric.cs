using System.Collections.Generic;
using System.Threading.Tasks;
using OpenTSDBApiWrapper.PutMetrics.Data;
using Refit;

namespace OpenTSDBApiWrapper {
    public interface IPutMetric {
        /// <summary>
        ///     Inserts a single metric value into OpenTSDB.
        /// </summary>
        /// <param name="metric">The metric data to be inserted</param>
        /// <param name="sync">Whether or not to wait for the data to be flushed to storage before returning the results.</param>
        /// <param name="syncTimeout">
        ///     A timeout, in milliseconds, to wait for the data to be flushed to storage before returning with an error.
        ///     When a timeout occurs, using the details flag will tell how many data points failed and how many succeeded.
        ///     sync must also be given for this to take effect. A value of 0 means the write will not timeout.
        /// </param>
        [Post("/api/put?sync={sync}&sync_timeout={syncTimeout}")]
        Task InsertSingle([Body] MetricData metric, bool sync = false, int syncTimeout = 0);

        /// <summary>
        ///     Inserts multiple metric value into OpenTSDB.
        /// </summary>
        /// <param name="metric">The metric data to be inserted</param>
        /// <param name="sync">Whether or not to wait for the data to be flushed to storage before returning the results.</param>
        /// <param name="syncTimeout">
        ///     A timeout, in milliseconds, to wait for the data to be flushed to storage before returning with an error.
        ///     When a timeout occurs, using the details flag will tell how many data points failed and how many succeeded.
        ///     sync must also be given for this to take effect. A value of 0 means the write will not timeout.
        /// </param>
        [Post("/api/put?sync={sync}&sync_timeout={syncTimeout}")]
        Task Insert([Body] IEnumerable<MetricData> metric, bool sync = false, int syncTimeout = 0);

        /// <summary>
        ///     Inserts a single metric value into OpenTSDB. Returns Details
        /// </summary>
        /// <param name="metric">The metric data to be inserted</param>
        /// <param name="sync">Whether or not to wait for the data to be flushed to storage before returning the results.</param>
        /// <param name="syncTimeout">
        ///     A timeout, in milliseconds, to wait for the data to be flushed to storage before returning with an error.
        ///     When a timeout occurs, using the details flag will tell how many data points failed and how many succeeded.
        ///     sync must also be given for this to take effect. A value of 0 means the write will not timeout.
        /// </param>
        /// <returns>Details about what data points could not be stored and why </returns>
        [Post("/api/put/?details=true&sync={sync}&sync_timeout={syncTimeout}")]
        Task<PutDetails> InsertSingleWithDetails([Body] MetricData metric, bool sync = false, int syncTimeout = 0);

        /// <summary>
        ///     Inserts multiple metric value into OpenTSDB. Returns Details
        /// </summary>
        /// <param name="metric">The metric data to be inserted</param>
        /// <param name="sync">Whether or not to wait for the data to be flushed to storage before returning the results.</param>
        /// <param name="syncTimeout">
        ///     A timeout, in milliseconds, to wait for the data to be flushed to storage before returning with an error.
        ///     When a timeout occurs, using the details flag will tell how many data points failed and how many succeeded.
        ///     sync must also be given for this to take effect. A value of 0 means the write will not timeout.
        /// </param>
        /// <returns>Details about what data points could not be stored and why </returns>
        [Post("/api/put/?details=true&sync={sync}&sync_timeout={syncTimeout}")]
        Task<PutDetails> InsertWithDetails([Body] IEnumerable<MetricData> metric, bool sync = false, int syncTimeout = 0);

        /// <summary>
        ///     Inserts a single metric value into OpenTSDB. Retuns Summary
        /// </summary>
        /// <param name="metric">The metric data to be inserted</param>
        /// <param name="sync">Whether or not to wait for the data to be flushed to storage before returning the results.</param>
        /// <param name="syncTimeout">
        ///     A timeout, in milliseconds, to wait for the data to be flushed to storage before returning with an error.
        ///     When a timeout occurs, using the details flag will tell how many data points failed and how many succeeded.
        ///     sync must also be given for this to take effect. A value of 0 means the write will not timeout.
        /// </param>
        /// <returns>Summary of how many data points were stored successfully and failed</returns>
        [Post("/api/put?summary=true")]
        Task<PutSummary> InsertSingleWithSummary([Body] MetricData metric, bool sync = false, int syncTimeout = 0);

        /// <summary>
        ///     Inserts multiple metric value into OpenTSDB. Retuns Summary
        /// </summary>
        /// <param name="metric">The metric data to be inserted</param>
        /// <param name="sync">Whether or not to wait for the data to be flushed to storage before returning the results.</param>
        /// <param name="syncTimeout">
        ///     A timeout, in milliseconds, to wait for the data to be flushed to storage before returning with an error.
        ///     When a timeout occurs, using the details flag will tell how many data points failed and how many succeeded.
        ///     sync must also be given for this to take effect. A value of 0 means the write will not timeout.
        /// </param>
        /// <returns>Summary of how many data points were stored successfully and failed</returns>
        [Post("/api/put?summary=true")]
        Task<PutSummary> InsertWithSummary([Body] IEnumerable<MetricData> metric, bool sync = false, int syncTimeout = 0);
    }
}