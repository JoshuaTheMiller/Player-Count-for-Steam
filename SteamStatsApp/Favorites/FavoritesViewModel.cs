using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trfc.ClientFramework;
using Trfc.SteamStats.ClientServices.GameFavorites;

namespace SteamStatsApp.Favorites
{
    public sealed class FavoritesViewModel : ViewModelBase, INavigationTarget
    {
        public string PageTitle { get; } = "Favorites";

        private readonly IFavoriteGamesViewModelFetcher fetcher;
        private readonly IFavoriteGameFetcher favoriteFecher;

        private ICollection<GameViewModel> originalGamesList = new List<GameViewModel>();

        public IRangedCollection<GameViewModel> Games { get; } = new RangedObservableCollection<GameViewModel>();

        public FavoritesViewModel(IFavoriteGamesViewModelFetcher viewModelFetcher,
            IFavoriteGameFetcher favoriteFecher)
        {
            this.fetcher = viewModelFetcher;
            this.favoriteFecher = favoriteFecher;
            this.favoriteFecher.FavoritesChanged += OnFavoritesChanged;
        }        

        private async Task OnRefreshGamesList()
        {
            originalGamesList = (await fetcher.FetchGameViewModelsAsync())
                .OrderByDescending(game => game.IsFavorited)
                .ThenBy(game => game.Name).ToList();

            Games.ReplaceWithRange(originalGamesList);
        }

        private async void OnFavoritesChanged(object sender, EventArgs e)
        {
            await this.Refresh();
        }

        protected override async Task TasksToExecuteWhileRefreshing()
        {
            await OnRefreshGamesList();            
        }
    }
}
