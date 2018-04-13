using System;
using System.Threading.Tasks;

namespace Trfc.SteamStats.ClientServices.AvailableGames
{
    public interface IAvailableGamesCacheChecker
    {
        Task<CacheResponse> IsCacheOutOfDate(DateTime cacheLastUpdatedTime);
    }
}