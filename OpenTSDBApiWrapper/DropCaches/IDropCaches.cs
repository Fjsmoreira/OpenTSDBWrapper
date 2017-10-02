using System.Threading.Tasks;
using Refit;

namespace OpenTSDBApiWrapper {
    public interface IDropCaches {
        /// <summary>
        ///     purges the in-memory data cached in OpenTSDB. This includes all UID to name and name to UID maps for metrics, tag
        ///     names and tag values.
        /// </summary>
        /// <returns>A message and the status. </returns>
        [Get("/api/dropcaches")]
        Task<DropCachesData> Drop();
    }
}