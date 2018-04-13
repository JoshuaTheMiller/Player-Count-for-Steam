using System.Collections.Generic;
using System.Threading.Tasks;
using Trfc.ClientFramework;

namespace Trfc.SteamStats.ClientServices.AvailableGames
{
    public sealed class GameFetcherCache : IAvailableGamesFetcher
    {
        private readonly static string storageKey = "CachedAvailableGames";

        private readonly IAvailableGamesFetcher gamesFetcher;
        private readonly IStorageProvider<CachedGameList> storageProvider;
        private readonly IAvailableGamesCacheChecker cachChecker;

        public GameFetcherCache(IAvailableGamesFetcher gamesFetcher, 
            IStorageProvider<CachedGameList> storageProvider, 
            IAvailableGamesCacheChecker cachChecker)
        {
            this.gamesFetcher = gamesFetcher;
            this.storageProvider = storageProvider;
            this.cachChecker = cachChecker;
        }

        public async Task<IEnumerable<Game>> FetchGamesAsync()
        {
            var localStorageResponse = await storageProvider.Get(storageKey);                        

            if(!localStorageResponse.Succeeded)
            {
                return await FetchAndUpdateGamesCache();
            }

            var cachedGamesList = ((CachedGameList)localStorageResponse.Value);

            var cacheCheck = await cachChecker.IsCacheOutOfDate(cachedGamesList.TimeCachedUtc);

            if(!cacheCheck.IsOutOfDate)
            {
                return cachedGamesList.GamesList;
            }

            return await FetchAndUpdateGamesCache();
        }

        private async Task<IEnumerable<Game>> FetchAndUpdateGamesCache()
        {
            var games = await gamesFetcher.FetchGamesAsync();

            await storageProvider.Update(storageKey, CachedGameList.Create(games));

            return games;
        }
    }
}
