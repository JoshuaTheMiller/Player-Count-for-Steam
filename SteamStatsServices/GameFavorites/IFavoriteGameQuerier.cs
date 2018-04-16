using System.Threading.Tasks;

namespace Trfc.SteamStats.ClientServices.GameFavorites
{
    public interface IFavoriteGameQuerier
    {
        Task<bool> IsGameFavorite(int gameId);
    }
}
