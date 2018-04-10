using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteamStatsApp.Main
{
    public interface IGamesViewModelFetcher
    {
        Task<IEnumerable<GameViewModel>> FetchGameViewModelsAsync();
    }
}