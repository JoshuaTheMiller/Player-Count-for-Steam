using System;
using System.Threading.Tasks;
using Trfc.ClientFramework;

namespace Trfc.SteamStats.ClientServices.GamePictures
{
    public sealed class PictureCacheChecker : IPictureCacheChecker
    {
        private readonly string connectionStringKey = "GamePictureCacheTime";

        private readonly IConfigurationProvider configurationProvider;
        private readonly IWebGateway webGateway;

        public PictureCacheChecker(IConfigurationProvider configurationProvider,
            IWebGateway webGateway)
        {
            this.configurationProvider = configurationProvider;
            this.webGateway = webGateway;
        }

        public async Task<CacheResponse> IsCacheOutOfDate(DateTime cacheLastUpdatedTimeUtc, int appId)
        {
            var endpoint = configurationProvider.GetConnectionStringById(connectionStringKey);

            endpoint += $"?appId={appId}";

            var response = await webGateway.GetResponseFromEndpoint<ResponseDao>(endpoint);

            if (!response.IsCached)
            {
                return CacheResponse.OutOfDate();
            }

            if (response.CachedDateTimeUtc < cacheLastUpdatedTimeUtc)
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
