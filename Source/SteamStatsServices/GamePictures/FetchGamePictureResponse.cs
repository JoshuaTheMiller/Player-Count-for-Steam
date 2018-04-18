using System.Linq;

namespace Trfc.SteamStats.ClientServices.GamePictures
{
    public sealed class FetchGamePictureResponse
    {       
        public byte[] Image { get; }

        public int GameId { get; }

        public bool HasPicture { get; }

        private FetchGamePictureResponse(byte[] data, int gameId, bool hasPicture)
        {
            Image = data;
            GameId = gameId;
            HasPicture = hasPicture;
        }

        public static FetchGamePictureResponse NoPicture(int gameId)
        {
            return new FetchGamePictureResponse(Enumerable.Empty<byte>().ToArray(), gameId, false);
        }

        public static FetchGamePictureResponse ContainsPicture(int gameId, byte[] imageData)
        {
            return new FetchGamePictureResponse(imageData, gameId, true);
        }
    }
}
