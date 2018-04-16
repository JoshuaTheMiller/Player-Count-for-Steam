using System.Threading.Tasks;
using Trfc.SteamStats.ClientServices.GameFavorites;

namespace SteamStatsApp.Tests.Doppels
{
    internal sealed class DoppelFavoriteGameQuerier : IFavoriteGameQuerier
    {
        public Task<bool> IsGameFavorite(int gameId)
        {
            return Task.FromResult(false);
        }
    }
}
