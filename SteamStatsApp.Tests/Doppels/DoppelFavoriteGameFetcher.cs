using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trfc.SteamStats.ClientServices.GameFavorites;

namespace SteamStatsApp.Tests.Doppels
{
    internal sealed class DoppelFavoriteGameFetcher : IFavoriteGameFetcher
    {
        public event EventHandler FavoritesChanged;

        public Task<IEnumerable<int>> GetFavoritedGames()
        {
            return Task.FromResult(Enumerable.Empty<int>());
        }
    }
}
