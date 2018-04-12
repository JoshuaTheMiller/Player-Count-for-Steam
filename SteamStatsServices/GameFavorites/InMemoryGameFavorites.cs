using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trfc.SteamStats.ClientServices.GameFavorites
{
    public sealed class InMemoryGameFavorites : IFavoriteGameFetcher, IGameFavoriter
    {
        private readonly IDictionary<int, bool> favoriteGameIds = new Dictionary<int, bool>();

        public event EventHandler FavoritesChanged;

        public Task<bool> FavoriteGameById(int id)
        {
            if (!favoriteGameIds.ContainsKey(id))
            {
                favoriteGameIds.Add(id, true);

                return Task.FromResult(true);
            }            

            return Task.FromResult(false);
        }

        public Task<bool> UnfavoriteGameById(int id)
        {
            if (favoriteGameIds.ContainsKey(id))
            {
                favoriteGameIds.Remove(id);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public Task<IEnumerable<int>> GetFavoritedGames()
        {
            return Task.FromResult<IEnumerable<int>>(favoriteGameIds.Keys.ToList());
        }
    }
}
