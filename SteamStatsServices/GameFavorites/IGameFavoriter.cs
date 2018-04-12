using System.Threading.Tasks;

namespace Trfc.SteamStats.ClientServices.GameFavorites
{
    public interface IGameFavoriter
    {
        Task<bool> FavoriteGameById(int id);

        Task<bool> UnfavoriteGameById(int id);
    }
}
