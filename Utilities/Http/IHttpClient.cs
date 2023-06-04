using System.Threading.Tasks;

namespace Utilities.Http
{
    public interface IHttpClient
    {
        Task<string> Get(string requestUri);
    }
}
