using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamStatsApp.GameFavorites
{
    public sealed class InMemoryGameFavorites : IFavoriteGameFetcher, IGameFavoriter
    {
        private readonly IDictionary<int, bool> favoriteGameIds = new Dictionary<int, bool>();

        public Task FavoriteGameById(int id)
        {
            if (!favoriteGameIds.ContainsKey(id))
            {
                favoriteGameIds.Add(id, true);
            }            

            return Task.FromResult(default(object));
        }

        public Task UnfavoriteGameById(int id)
        {
            if (favoriteGameIds.ContainsKey(id))
            {
                favoriteGameIds.Remove(id);
            }

            return Task.FromResult(default(object));
        }

        public Task<IEnumerable<int>> GetFavoritedGames()
        {
            return Task.FromResult<IEnumerable<int>>(favoriteGameIds.Keys.ToList());
        }
    }
}
