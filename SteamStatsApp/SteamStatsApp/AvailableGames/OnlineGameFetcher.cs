using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Trfc.ClientFramework;

namespace SteamStatsApp.AvailableGames
{
    public sealed class OnlineGameFetcher : IAvailableGamesFetcher
    {
        private readonly string connectionStringKey = "AvailableGames";

        private readonly IConfigurationProvider configurationProvider;

        public OnlineGameFetcher(IConfigurationProvider configurationProvider)
        {
            this.configurationProvider = configurationProvider;
        }

        public async Task<IEnumerable<Game>> FetchGamesAsync()
        {
            var endpoint = configurationProvider.GetConnectionStringById(connectionStringKey);

            var response = await GetResponseFromEndpoint<ResponseDao>(endpoint);

            return response.AvailableGames;
        }

        private async Task<T> GetResponseFromEndpoint<T>(string url)
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

        private sealed class ResponseDao
        {
            public List<Game> AvailableGames { get; set; }
        }
    }
}
