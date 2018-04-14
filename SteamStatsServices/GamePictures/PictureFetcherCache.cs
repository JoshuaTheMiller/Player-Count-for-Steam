using System;
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

        public async Task<FetchGamePictureResponse> FetchPictureForGameAsync(int appId)
        {
            var localStorageResponse = await storageProvider.Get(storageKey, appId.ToString());

            if(!localStorageResponse.Succeeded)
            {
                return await FetchAndUpdatePictureCache(appId);
            }

            var gamePicture = (CachedGamePicture)localStorageResponse.Value;

            var localCacheCheck = await cacheChecker.IsCacheOutOfDate(gamePicture.TimeCachedUtc, appId);

            if(localCacheCheck.IsOutOfDate)
            {
                return await FetchAndUpdatePictureCache(appId);
            }

            var pictureAsBytes = Convert.FromBase64String(gamePicture.Base64EncodedImage);

            return FetchGamePictureResponse.ContainsPicture(appId, pictureAsBytes);
        }

        private async Task<FetchGamePictureResponse> FetchAndUpdatePictureCache(int appId)
        {
            var picture = await pictureFetcher.FetchPictureForGameAsync(appId);

            var imageAsBase64String = Convert.ToBase64String(picture.Image);

            var cacheGamePicture = CachedGamePicture.Create(imageAsBase64String, appId);

            await storageProvider.Update(storageKey, appId.ToString(), cacheGamePicture);

            var response = FetchGamePictureResponse.ContainsPicture(appId, picture.Image);

            return response;
        }
    }
}
