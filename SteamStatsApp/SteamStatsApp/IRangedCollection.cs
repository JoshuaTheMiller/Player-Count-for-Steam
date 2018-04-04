using System.Collections.Generic;

namespace SteamStatsApp
{
    public interface IRangedCollection<T> : ICollection<T>
    {
        void AddRange(IEnumerable<T> itemsToAdd);
    }
}
