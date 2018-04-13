using System;
using System.Collections.Generic;

namespace Trfc.SteamStats.ClientServices.AvailableGames
{
    public sealed class CachedGameList
    {        
        public DateTime TimeCachedUtc { get; }

        public IEnumerable<Game> GamesList { get;}

        public CachedGameList(DateTime timeCachedUtc, IEnumerable<Game> gamesList)
        {
            TimeCachedUtc = timeCachedUtc;
            GamesList = gamesList;
        }

        internal static CachedGameList Create(IEnumerable<Game> gamesList)
        {
            return new CachedGameList(DateTime.UtcNow, gamesList);
        }
    }
}
