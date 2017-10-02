using System.Threading.Tasks;

namespace OpenTSDBApiWrapper {
    public interface IQuery {
        Task Query();
    }
}