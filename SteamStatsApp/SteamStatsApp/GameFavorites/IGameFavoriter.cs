using System.Threading.Tasks;

namespace SteamStatsApp.GameFavorites
{
    public interface IGameFavoriter
    {
        Task<bool> FavoriteGameById(int id);

        Task<bool> UnfavoriteGameById(int id);
    }
}
