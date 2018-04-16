using System.Threading;
using System.Threading.Tasks;

namespace Trfc.ClientFramework
{
    public interface IWebGateway
    {
        Task<WebRequestResponse<T>> GetResponseFromEndpoint<T>(string url, CancellationToken token);
    }
}
