using System;
using System.Threading.Tasks;
using Trfc.ClientFramework;

namespace Trfc.SteamStats.ClientServices.AvailableGames
{
    public sealed class AvailableGamesCacheChecker : IAvailableGamesCacheChecker
    {
        private readonly string connectionStringKey = "AvailableGamesCacheTime";

        private readonly IConfigurationProvider configurationProvider;
        private readonly IWebGateway webGateway;

        public AvailableGamesCacheChecker(IConfigurationProvider configurationProvider, IWebGateway webGateway)
        {
            this.configurationProvider = configurationProvider;
            this.webGateway = webGateway;
        }

        public async Task<CacheResponse> IsCacheOutOfDate(DateTime cacheLastUpdatedTimeUtc)
        {
            var endpoint = configurationProvider.GetConnectionStringById(connectionStringKey);

            var response = await webGateway.GetResponseFromEndpoint<ResponseDao>(endpoint);

            if(!response.IsCached)
            {
                return CacheResponse.OutOfDate();
            }

            if(response.CachedDateTimeUtc < cacheLastUpdatedTimeUtc)
            {
                return CacheResponse.NotOutOfDate();
            }

            return CacheResponse.OutOfDate();
        }

        private sealed class ResponseDao
        {
            public bool IsCached { get; set; }
            public DateTime CachedDateTimeUtc { get; set; }
        }
    }
}
