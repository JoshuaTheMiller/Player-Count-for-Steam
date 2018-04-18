using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Trfc.ClientFramework;

namespace SteamStatsApp
{
    public sealed class WebGateway : IWebGateway
    {
        private readonly IStringDeserializer stringDeserializer;

        public WebGateway(IStringDeserializer stringDeserializer)
        {
            this.stringDeserializer = stringDeserializer;
        }

        public async Task<WebRequestResponse<T>> GetResponseFromEndpoint<T>(string url, CancellationToken token)
        {
            try
            {
                var responseString = await GetResponseString(url, token);

                var obj = stringDeserializer.Deserialize<T>(responseString);

                return WebRequestFactory.Success(obj);
            }
            catch(System.Net.WebException e)
            {                
                return WebRequestFactory.Cancelled<T>();
            }            
        }

        private async Task<string> GetResponseString(string url, CancellationToken token)
        {
            var client = new HttpClient();

            var response = await client.GetAsync(url, token);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
