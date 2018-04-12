using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Trfc.ClientFramework;

namespace SteamStatsApp.Main
{
    public sealed class MainPageViewModel : ViewModelBase
    {
        private readonly IGamesViewModelFetcher fetcher;        

        private string searchText = string.Empty;
        public string SearchText
        {
            get => searchText;
            set => SetField(ref searchText, value, OnSearchTextUpdated);            
        }

        public ICommand ClearSearchText { get; } 

        public ICommand RefreshGamesList { get; }

        private ICollection<GameViewModel> originalGamesList = new List<GameViewModel>();

        public IRangedCollection<GameViewModel> Games { get; } = new RangedObservableCollection<GameViewModel>();

        private int countOfGames = 0;

        public int CountOfGames
        {
            get => countOfGames;
            private set => SetField(ref countOfGames, value);
        }

        public MainPageViewModel(IGamesViewModelFetcher fetcher)
        {
            this.fetcher = fetcher;            
            this.ClearSearchText = CommandFactory.Create(OnClearSearchText);
            this.RefreshGamesList = CommandFactory.Create(async () => await OnRefreshGamesList());
        }

        private void OnClearSearchText(object obj)
        {
            SearchText = string.Empty;
        }

        private async Task OnRefreshGamesList()
        {
            originalGamesList = (await fetcher.FetchGameViewModelsAsync())
                .OrderByDescending(game => game.IsFavorited)
                .ThenBy(game => game.Name).ToList();

            CountOfGames = originalGamesList.Count;

            SetGameList(originalGamesList);
        }

        private void OnSearchTextUpdated(string obj)
        {
            if (string.IsNullOrWhiteSpace(obj))
            {
                SetGameList(originalGamesList);
                return;
            }

            var filteredList = originalGamesList.Where(game => game.Name.Contains(obj, StringComparison.OrdinalIgnoreCase)).ToList();

            SetGameList(filteredList);
        }

        private void SetGameList(IEnumerable<GameViewModel> newList)
        { 
            Games.ReplaceWithRange(newList);
        }

        protected override async Task TasksToExecuteWhileRefreshing()
        {
            await OnRefreshGamesList();
            OnClearSearchText(null);
        }
    }
}
