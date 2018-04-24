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
