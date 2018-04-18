using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Trfc.ClientFramework;
using Trfc.ClientFramework.CollectionViews;
using Trfc.SteamStats.ClientServices.GameFavorites;

namespace SteamStatsApp.Main
{
    public sealed class MainPageViewModel : ViewModelBase, INavigationTarget
    {
        private readonly IGamesViewModelFetcher fetcher;
        private readonly IFavoriteGameFetcher favoriteFecher;

        private string searchText = string.Empty;
        public string SearchText
        {
            get => searchText;
            set => SetField(ref searchText, value, async (text) => await OnSearchTextUpdated(text));
        }

        public ICommand ClearSearchText { get; }

        public ICommand RefreshGamesList { get; }

        public ICollectionView<GameViewModel> Games { get; }

        public string PageTitle { get; } = "All Games";

        public MainPageViewModel(IGamesViewModelFetcher fetcher,
            IFavoriteGameFetcher favoriteFecher)
        {
            this.fetcher = fetcher;
            this.favoriteFecher = favoriteFecher;
            this.ClearSearchText = CommandFactory.Create(OnClearSearchText);
            this.RefreshGamesList = CommandFactory.Create(async () => await Refresh());

            this.favoriteFecher.FavoritesChanged += OnFavoritesChanged;

            Games = CollectionViewFactory.Create(Enumerable.Empty<GameViewModel>(),
                new Predicate<GameViewModel>[] { SearchTextFilter },
                GameComparer,
                Orderer);
        }

        private IEnumerable<GameViewModel> Orderer(IEnumerable<GameViewModel> arg)
        {
            return arg.OrderBy(game => game.Name).ToList();
        }

        private bool SearchTextFilter(GameViewModel arg1)
        {
            var matchesSearchText = arg1.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase);
            return matchesSearchText;
        }

        private bool GameComparer(GameViewModel arg1, GameViewModel arg2)
        {
            return arg1.Id == arg2.Id;
        }

        private void OnClearSearchText(object obj)
        {
            SearchText = string.Empty;
        }

        private async Task OnRefreshGamesList(CancellationToken token)
        {
            var newGamesList = await fetcher.FetchGameViewModelsAsync(token);

            await Games.SyncNewSourceItemsAsync(newGamesList);
        }

        private async Task OnSearchTextUpdated(string obj)
        {
            await Games.Refresh();
        }

        protected override async Task TasksToExecuteWhileRefreshing(CancellationToken token)
        {
            await OnRefreshGamesList(token);
            OnClearSearchText(null);
        }

        private async void OnFavoritesChanged(object sender, EventArgs e)
        {
            foreach (var item in Games.Source)
            {           
                await item.Refresh();
            }
        }
    }
}