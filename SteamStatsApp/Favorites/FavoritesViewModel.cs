using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trfc.ClientFramework;
using Trfc.ClientFramework.CollectionViews;
using Trfc.SteamStats.ClientServices.GameFavorites;

namespace SteamStatsApp.Favorites
{
    public sealed class FavoritesViewModel : ViewModelBase, INavigationTarget
    {
        public string PageTitle { get; } = "Favorites";

        private readonly IFavoriteGamesViewModelFetcher fetcher;          

        public ICollectionView<GameViewModel> Games { get; }

        public FavoritesViewModel(IFavoriteGamesViewModelFetcher viewModelFetcher,
            IFavoriteGameFetcher favoriteFecher)
        {
            this.fetcher = viewModelFetcher;
            favoriteFecher.FavoritesChanged += OnFavoritesChanged;            
            
            Games = CollectionViewFactory.Create(Enumerable.Empty<GameViewModel>(),
                Enumerable.Empty<Predicate<GameViewModel>>(), 
                ComparerFunction, 
                OrderingFunction);
        }

        private bool ComparerFunction(GameViewModel arg1, GameViewModel arg2)
        {
            return arg1.Id == arg2.Id;
        }

        private IEnumerable<GameViewModel> OrderingFunction(IEnumerable<GameViewModel> arg)
        {
            return arg.OrderBy(game => game.Name).ToList();
        }

        private async Task OnRefreshGamesList()
        {
            var originalGamesList = await fetcher.FetchGameViewModelsAsync();          

            await Games.SyncNewSourceItemsAsync(originalGamesList);        
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
