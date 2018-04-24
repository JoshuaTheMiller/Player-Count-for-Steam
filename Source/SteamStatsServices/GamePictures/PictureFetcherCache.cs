using System;
using System.Threading;
using System.Threading.Tasks;
using Trfc.ClientFramework;

namespace Trfc.SteamStats.ClientServices.GamePictures
{
    public sealed class PictureFetcherCache : IGamePictureFetcher
    {        
        private readonly static string storageKey = "CachedGamePicture";

        private readonly IGamePictureFetcher pictureFetcher;
        private readonly IStorageProvider<CachedGamePicture> storageProvider;
        private readonly IPictureCacheChecker cacheChecker;

        public PictureFetcherCache(IGamePictureFetcher pictureFetcher, 
            IStorageProvider<CachedGamePicture> storageProvider, 
            IPictureCacheChecker cacheChecker)
        {
            this.pictureFetcher = pictureFetcher;
            this.storageProvider = storageProvider;
            this.cacheChecker = cacheChecker;
        }

        public async Task<FetchGamePictureResponse> FetchPictureForGameAsync(int appId, CancellationToken token)
        {
            var localStorageResponse = await storageProvider.Get(storageKey, appId.ToString());

            if(!localStorageResponse.Succeeded)
            {
                return await FetchAndUpdatePictureCache(appId, token);
            }

            var gamePicture = (CachedGamePicture)localStorageResponse.Value;

            var localCacheCheck = await cacheChecker.IsCacheOutOfDate(gamePicture.TimeCachedUtc, appId, token);

            if(localCacheCheck.IsOutOfDate)
            {
                return await FetchAndUpdatePictureCache(appId, token);
            }

            var pictureAsBytes = Convert.FromBase64String(gamePicture.Base64EncodedImage);

            return FetchGamePictureResponse.ContainsPicture(appId, pictureAsBytes);
        }

        private async Task<FetchGamePictureResponse> FetchAndUpdatePictureCache(int appId, CancellationToken token)
        {
            var pictureFetchResponse = await pictureFetcher.FetchPictureForGameAsync(appId, token);

            if(!pictureFetchResponse.Successful)
            {
                return FetchGamePictureResponse.FetchFailed(pictureFetchResponse.Message);
            }

            var imageAsBase64String = Convert.ToBase64String(pictureFetchResponse.Image);

            var cacheGamePicture = CachedGamePicture.Create(imageAsBase64String, appId);

            await storageProvider.Update(storageKey, appId.ToString(), cacheGamePicture);

            var response = FetchGamePictureResponse.ContainsPicture(appId, pictureFetchResponse.Image);

            return response;
        }
    }
}
