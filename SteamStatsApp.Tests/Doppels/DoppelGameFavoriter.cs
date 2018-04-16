using System;
using System.Threading.Tasks;
using Trfc.SteamStats.ClientServices.GameFavorites;

namespace SteamStatsApp.Tests.Doppels
{
    internal sealed class DoppelGameFavoriter : IGameFavoriter
    {        
        public Task<bool> FavoriteGameById(int id)
        {
            return Task.FromResult(false);
        }

        public Task<bool> UnfavoriteGameById(int id)
        {
            return Task.FromResult(false);
        }
    }
}
