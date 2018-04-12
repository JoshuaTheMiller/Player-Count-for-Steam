using System.Collections.Generic;
using System.Threading.Tasks;

namespace Trfc.SteamStats.ClientServices.AvailableGames
{
    public interface IAvailableGamesFetcher
    {
        Task<IEnumerable<Game>> FetchGamesAsync();
    }
}