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
#pragma warning disable CS0168 // Variable is declared but never used
            catch (System.Net.WebException e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                return WebRequestFactory.Cancelled<T>();
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
