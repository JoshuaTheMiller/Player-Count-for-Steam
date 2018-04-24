using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Trfc.ClientFramework;
using Trfc.ClientFramework.Connectivity;

namespace SteamStatsApp
{
    public sealed class WebGateway : IWebGateway
    {
        private readonly IStringDeserializer stringDeserializer;
        private readonly INetworkChecker networkChecker;        

        public WebGateway(IStringDeserializer stringDeserializer,
            INetworkChecker networkChecker)
        {
            this.stringDeserializer = stringDeserializer;
            this.networkChecker = networkChecker;            
        }       

        public async Task<WebRequestResponse<T>> GetResponseFromEndpoint<T>(string url, CancellationToken token)
        {            
            if(!networkChecker.HasInternetAccess())
            {
                return WebRequestFactory.Cancelled<T>();
            }

            try
            {
                var responseString = await GetResponseString(url, token);

                var obj = stringDeserializer.Deserialize<T>(responseString);

                return WebRequestFactory.Success(obj);
            }
            catch (System.Net.WebException)
            {
                return WebRequestFactory.Cancelled<T>();
            }
            catch (HttpRequestException)
            {                
                return WebRequestFactory.Errored<T>("Internet connection was disrupted.");
            }
        }

        private async Task<string> GetResponseString(string url, CancellationToken token)
        {
            var client = new HttpClient();

            string responseString;

            using (var response = await client.GetAsync(url, token).ConfigureAwait(false))
            {
                responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            return responseString;
        }
    }
}
