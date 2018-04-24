using System.Collections.Generic;
using System.Threading;
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

        public async Task<AvailableGamesResponse> FetchGamesAsync(CancellationToken token)
        {
            var localStorageResponse = await storageProvider.Get(storageKey);                        

            if(!localStorageResponse.Succeeded)
            {
                return await FetchAndUpdateGamesCache(token);
            }

            var cachedGamesList = ((CachedGameList)localStorageResponse.Value);

            var cacheCheck = await cachChecker.IsCacheOutOfDate(cachedGamesList.TimeCachedUtc, token);

            if(!cacheCheck.IsOutOfDate)
            {
                return AvailableGamesResponse.Succeeded(cachedGamesList.GamesList);
            }            

            return await FetchAndUpdateGamesCache(token);
        }

        private async Task<AvailableGamesResponse> FetchAndUpdateGamesCache(CancellationToken token)
        {
            var response = await gamesFetcher.FetchGamesAsync(token);

            if(response.Successful)
            {
                await storageProvider.Update(storageKey, CachedGameList.Create(response.Games));

                return response;
            }

            return response;
        }
    }
}
