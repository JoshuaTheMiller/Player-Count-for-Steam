using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trfc.SteamStats.ClientServices.AvailableGames;
using Trfc.SteamStats.ClientServices.GameFavorites;

namespace SteamStatsApp.Main
{
    public sealed class GamesViewModelFetcher : IGamesViewModelFetcher
    {
        private readonly IAvailableGamesFetcher fetcher;
        private readonly IGameFavoriter favoriter;
        private readonly IFavoriteGameFetcher favoriteFetcher;
        private readonly IFavoriteGameQuerier gameQuerier;

        public GamesViewModelFetcher(IAvailableGamesFetcher fetcher,
            IFavoriteGameFetcher favoriteFetcher,
            IGameFavoriter favoriter,
            IFavoriteGameQuerier gameQuerier)
        {
            this.fetcher = fetcher;
            this.favoriteFetcher = favoriteFetcher;
            this.favoriter = favoriter;
            this.gameQuerier = gameQuerier;
        }

        public async Task<IEnumerable<GameViewModel>> FetchGameViewModelsAsync()
        {
            var allGames = await fetcher.FetchGamesAsync();
            var favoriteGames = await favoriteFetcher.GetFavoritedGames();

            IEnumerable<GameViewModel> viewModels = ConvertToGameViewModels(allGames, favoriteGames);

            return viewModels;
        }

        private IEnumerable<GameViewModel> ConvertToGameViewModels(IEnumerable<Game> allGames, IEnumerable<int> favoriteGames)
        {
            return allGames.Select(game => new GameViewModel(game.Name, game.Id, favoriteGames.Contains(game.Id), this.favoriter, this.gameQuerier)).ToList();
        }
    }
}
