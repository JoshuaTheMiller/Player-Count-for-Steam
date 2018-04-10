using SteamStatsApp.GameFavorites;
using System.Threading.Tasks;

namespace SteamStatsApp.Tests.Doppels
{
    internal sealed class DoppelGameFavoriter : IGameFavoriter
    {
        public Task FavoriteGameById(int id)
        {
            return Task.FromResult(default(object));
        }

        public Task UnfavoriteGameById(int id)
        {
            return Task.FromResult(default(object));
        }
    }
}
