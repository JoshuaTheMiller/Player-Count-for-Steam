using SteamStatsApp.Main;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamStatsApp.Tests.Doppels
{
    internal sealed class DoppelGameFetcher : IGamesViewModelFetcher
    {
        public IEnumerable<GameViewModel> Games { get; set; } = Enumerable.Empty<GameViewModel>();

        public Task<IEnumerable<GameViewModel>> FetchGameViewModelsAsync()
        {
            return Task.FromResult(Games);
        }
    }
}
