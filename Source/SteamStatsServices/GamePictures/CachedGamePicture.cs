using System;

namespace Trfc.SteamStats.ClientServices.GamePictures
{
    public sealed class CachedGamePicture
    {        
        public DateTime TimeCachedUtc { get; }

        public string Base64EncodedImage { get; }

        public int AppId { get; }

        public CachedGamePicture(DateTime timeCachedUtc, string base64EncodedImage, int appId)
        {
            TimeCachedUtc = timeCachedUtc;
            Base64EncodedImage = base64EncodedImage;
            AppId = appId;
        }

        internal static CachedGamePicture Create(string base64EncodedImage, int appId)
        {
            return new CachedGamePicture(DateTime.UtcNow, base64EncodedImage, appId);
        }
    }
}
