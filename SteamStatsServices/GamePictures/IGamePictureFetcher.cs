using System.Threading.Tasks;

namespace Trfc.SteamStats.ClientServices.GamePictures
{
    public interface IGamePictureFetcher
    {
        Task<FetchGamePictureResponse> FetchPictureForGameAsync(int id);
    }
}