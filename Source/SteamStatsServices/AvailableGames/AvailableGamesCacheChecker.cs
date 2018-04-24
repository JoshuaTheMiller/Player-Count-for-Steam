using System;
using System.Threading;
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

        public async Task<CacheResponse> IsCacheOutOfDate(DateTime cacheLastUpdatedTimeUtc, CancellationToken token)
        {
            var endpoint = configurationProvider.GetConnectionStringById(connectionStringKey);

            var gatewayResponse = await webGateway.GetResponseFromEndpoint<ResponseDao>(endpoint, token);

            if(!gatewayResponse.Succeeded)
            {
                return CacheResponse.OutOfDate();
            }

            var response = gatewayResponse.Value;

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

        [Preserve(AllMembers = true)]
        private sealed class ResponseDao
        {            
            public bool IsCached { get; }
            public DateTime CachedDateTimeUtc { get; }
            public ResponseDao(bool isCached, DateTime cachedDateTimeUtc)
            {
                IsCached = isCached;
                CachedDateTimeUtc = cachedDateTimeUtc;
            }
        }
    }
}
