using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace OpenTSDBApiWrapper {
    public interface IConfig {
        /// <summary>
        ///     Retrieves the opentsdb Configuration
        /// </summary>
        /// <returns>A dictionary with the configuration List</returns>
        [Get("api/config")]
        Task<IDictionary<string, string>> Configuration();
    }
}