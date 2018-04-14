using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trfc.SteamStats.ClientServices.AvailableGames;
using Trfc.SteamStats.ClientServices.GameFavorites;
using Trfc.SteamStats.ClientServices.GamePictures;

namespace SteamStatsApp.Favorites
{
    public sealed class FavoriteGamesViewModelFetcher : IFavoriteGamesViewModelFetcher
    {
        private readonly IAvailableGamesFetcher fetcher;
        private readonly IGameFavoriter favoriter;
        private readonly IFavoriteGameFetcher favoriteFetcher;
        private readonly IGamePictureFetcher pictureFetcher;

        public FavoriteGamesViewModelFetcher(IAvailableGamesFetcher fetcher,
            IFavoriteGameFetcher favoriteFetcher,
            IGameFavoriter favoriter,
            IGamePictureFetcher pictureFetcher)
        {
            this.fetcher = fetcher;
            this.favoriteFetcher = favoriteFetcher;
            this.favoriter = favoriter;
            this.pictureFetcher = pictureFetcher;
        }

        public async Task<IEnumerable<FavoriteGameViewModel>> FetchGameViewModelsAsync()
        {
            var allGames = await fetcher.FetchGamesAsync();
            var favoriteGames = await favoriteFetcher.GetFavoritedGames();

            IEnumerable<FavoriteGameViewModel> viewModels = ConvertToGameViewModels(allGames, favoriteGames);

            return viewModels;
        }

        private IEnumerable<FavoriteGameViewModel> ConvertToGameViewModels(IEnumerable<Game> allGames, IEnumerable<int> favoriteGames)
        {
            //TODO: This could be probably be made more efficient
            return allGames.Select(ConvertToViewModel(favoriteGames))
                .Where(game => game.IsFavorited)
                .ToList();
        }

        private System.Func<Game, FavoriteGameViewModel> ConvertToViewModel(IEnumerable<int> favoriteGames)
        {
            return game => new FavoriteGameViewModel(game.Name, game.Id, favoriteGames.Contains(game.Id), this.favoriter, this.pictureFetcher);
        }
    }
}
