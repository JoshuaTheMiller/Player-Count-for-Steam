using System.Threading.Tasks;

namespace SteamStatsApp.GameFavorites
{
    public interface IGameFavoriter
    {
        Task FavoriteGameById(int id);

        Task UnfavoriteGameById(int id);
    }
}
