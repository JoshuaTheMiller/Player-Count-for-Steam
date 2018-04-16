using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SteamStatsApp.Main
{
    public interface IGamesViewModelFetcher
    {
        Task<IEnumerable<GameViewModel>> FetchGameViewModelsAsync(CancellationToken token);
    }
}