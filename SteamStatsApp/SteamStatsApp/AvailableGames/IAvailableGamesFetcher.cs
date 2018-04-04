using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteamStatsApp.AvailableGames
{
    public interface IAvailableGamesFetcher
    {
        Task<IEnumerable<Game>> FetchGamesAsync();
    }
}