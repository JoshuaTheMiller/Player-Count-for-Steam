using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteamStatsApp
{
    public interface IAvailableGamesFetcher
    {
        Task<IEnumerable<Game>> FetchGamesAsync();
    }
}