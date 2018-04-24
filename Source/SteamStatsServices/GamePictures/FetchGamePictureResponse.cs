using System;
using System.Linq;

namespace Trfc.SteamStats.ClientServices.GamePictures
{
    public sealed class FetchGamePictureResponse : Response
    {       
        public byte[] Image { get; }

        public int GameId { get; }

        public bool HasPicture { get; }

        private FetchGamePictureResponse(byte[] data, int gameId, bool hasPicture, string message, bool successful)
            : base(successful, message)
        {
            Image = data;
            GameId = gameId;
            HasPicture = hasPicture;
        }

        public static FetchGamePictureResponse NoPicture(int gameId)
        {
            return new FetchGamePictureResponse(Enumerable.Empty<byte>().ToArray(), gameId, false, string.Empty, true);
        }

        public static FetchGamePictureResponse ContainsPicture(int gameId, byte[] imageData)
        {
            return new FetchGamePictureResponse(imageData, gameId, true, string.Empty, true);
        }

        internal static FetchGamePictureResponse FetchFailed(string resultMessage)
        {
            return new FetchGamePictureResponse(Enumerable.Empty<byte>().ToArray(), -1, false, resultMessage, false);
        }
    }
}
