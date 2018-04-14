using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteamStatsApp.Favorites
{
    public interface IFavoriteGamesViewModelFetcher
    {
        Task<IEnumerable<FavoriteGameViewModel>> FetchGameViewModelsAsync();
    }
}