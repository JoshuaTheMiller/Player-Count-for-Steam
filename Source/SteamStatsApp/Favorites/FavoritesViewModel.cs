using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trfc.ClientFramework;
using Trfc.ClientFramework.CollectionViews;
using Trfc.SteamStats.ClientServices.GameFavorites;

namespace SteamStatsApp.Favorites
{
    [Preserve(AllMembers = true)]
    public sealed class FavoritesViewModel : ViewModelBase, INavigationTarget
    {
        public string PageTitle { get; } = "Favorites";

        private readonly IFavoriteGamesViewModelFetcher fetcher;          

        public ICollectionView<FavoriteGameViewModel> Games { get; }

        public FavoritesViewModel(IFavoriteGamesViewModelFetcher viewModelFetcher,
            IFavoriteGameFetcher favoriteFecher)
        {
            this.fetcher = viewModelFetcher;
            favoriteFecher.FavoritesChanged += OnFavoritesChanged;            
            
            Games = CollectionViewFactory.Create(Enumerable.Empty<FavoriteGameViewModel>(),
                Enumerable.Empty<Predicate<FavoriteGameViewModel>>(), 
                ComparerFunction, 
                OrderingFunction,
                SyncParameters.WithDefaultResultSelector<FavoriteGameViewModel>(KeySelector));
        }

        private object KeySelector(FavoriteGameViewModel arg)
        {
            return arg.Id;
        }

        private bool ComparerFunction(FavoriteGameViewModel arg1, FavoriteGameViewModel arg2)
        {
            return arg1.Id == arg2.Id;
        }

        private IEnumerable<FavoriteGameViewModel> OrderingFunction(IEnumerable<FavoriteGameViewModel> arg)
        {
            return arg.OrderBy(game => game.Name).ToList();
        }

        private async Task RefreshGamesList(CancellationToken token)
        {
            var originalGamesList = await fetcher.FetchGameViewModelsAsync(token);          

            Games.SyncNewSourceItems(originalGamesList);        
        }

        private async void OnFavoritesChanged(object sender, EventArgs e)
        {
            await this.Refresh();
        }

        private async Task RefreshGameViewModels()
        {
            var refreshList = Games.Select(game => game.Refresh()).ToList();

            await Task.WhenAll(refreshList);
        }

        protected override async Task TasksToExecuteWhileRefreshing(CancellationToken token)
        {
            await RefreshGamesList(token);
            await RefreshGameViewModels();
        }        
    }
}
