using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Trfc.ClientFramework;

namespace SteamStatsApp
{
    public sealed class WebGateway : IWebGateway
    {
        public async Task<T> GetResponseFromEndpoint<T>(string url)
        {
            var responseString = await GetResponseString(url);

            return JsonConvert.DeserializeObject<T>(responseString);
        }

        private async Task<string> GetResponseString(string url)
        {
            var client = new HttpClient();

            var response = await client.GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
