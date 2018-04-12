using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trfc.SteamStats.ClientServices.AvailableGames;
using Trfc.SteamStats.ClientServices.GameFavorites;

namespace SteamStatsApp.Favorites
{
    public sealed class FavoriteGamesViewModelFetcher : IFavoriteGamesViewModelFetcher
    {
        private readonly IAvailableGamesFetcher fetcher;
        private readonly IGameFavoriter favoriter;
        private readonly IFavoriteGameFetcher favoriteFetcher;

        public FavoriteGamesViewModelFetcher(IAvailableGamesFetcher fetcher,
            IFavoriteGameFetcher favoriteFetcher,
            IGameFavoriter favoriter)
        {
            this.fetcher = fetcher;
            this.favoriteFetcher = favoriteFetcher;
            this.favoriter = favoriter;
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
            //TODO: This could be probably be made more efficient
            return allGames.Select(ConvertToViewModel(favoriteGames))
                .Where(game => game.IsFavorited)
                .ToList();
        }

        private System.Func<Game, GameViewModel> ConvertToViewModel(IEnumerable<int> favoriteGames)
        {
            return game => new GameViewModel(game.Name, game.Id, favoriteGames.Contains(game.Id), this.favoriter);
        }
    }
}
