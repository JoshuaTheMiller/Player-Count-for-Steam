using System.Collections.Generic;

namespace SteamStatsApp.GameFavorites
{
    public sealed class GameFavorites : Dictionary<int, bool>
    {               
        public void Add(int id)
        {
            this.Add(id, true);
        }
    }
}
