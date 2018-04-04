using SteamStatsApp.AvailableGames;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamStatsApp.Tests.Doppels
{
    internal sealed class DoppelGameFetcher : IAvailableGamesFetcher
    {
        public IEnumerable<Game> Games { get; set; } = Enumerable.Empty<Game>();

        public Task<IEnumerable<Game>> FetchGamesAsync()
        {
            return Task.FromResult(Games);
        }        
    }
}
