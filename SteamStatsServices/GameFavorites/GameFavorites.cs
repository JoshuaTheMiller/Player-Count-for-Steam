using System.Collections.Generic;

namespace Trfc.SteamStats.ClientServices.GameFavorites
{
    public sealed class GameFavorites : Dictionary<int, bool>
    {               
        public void Add(int id)
        {
            this.Add(id, true);
        }
    }
}
