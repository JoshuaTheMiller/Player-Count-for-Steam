using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteamStatsApp.GameFavorites
{
    public interface IFavoriteGameFetcher
    {
        Task<IEnumerable<int>> GetFavoritedGames();
    }
}
