using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Trfc.SteamStats.ClientServices.GameFavorites
{
    public interface IFavoriteGameFetcher
    {
        event EventHandler FavoritesChanged;

        Task<IEnumerable<int>> GetFavoritedGames();        
    }
}
