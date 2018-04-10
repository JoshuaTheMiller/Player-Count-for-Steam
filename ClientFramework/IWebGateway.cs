using System.Threading.Tasks;

namespace Trfc.ClientFramework
{
    public interface IWebGateway
    {
        Task<T> GetResponseFromEndpoint<T>(string url);
    }
}
