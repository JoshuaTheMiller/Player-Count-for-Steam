using System.Collections.Generic;
using System.Threading.Tasks;

namespace Trfc.SteamStats.ClientServices.GameFavorites
{
    public interface IFavoriteGameFetcher
    {
        Task<IEnumerable<int>> GetFavoritedGames();
    }
}
