using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SteamStatsApp
{
    public sealed class MainPageViewModel : ViewModelBase
    {
        private readonly IAvailableGamesFetcher fetcher;

        private string searchText = string.Empty;
        public string SearchText
        {
            get => searchText;
            set => SetField(ref searchText, value, OnSearchTextUpdated);            
        }

        public ICommand ClearSearchText { get; } 

        public ICommand RefreshGamesList { get; }

        private ICollection<Game> originalGamesList = new List<Game>();

        public IRangedCollection<Game> Games { get; } = new RangedObservableCollection<Game>();

        private int countOfGames = 0;

        public int CountOfGames
        {
            get => countOfGames;
            private set => SetField(ref countOfGames, value);
        }

        public MainPageViewModel(IAvailableGamesFetcher fetcher)
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
            originalGamesList = (await fetcher.FetchGamesAsync()).OrderBy(game => game.Name).ToList();

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

            var filteredList = originalGamesList.Where(game => game.Name.Contains(obj)).ToList();

            SetGameList(filteredList);
        }

        private void SetGameList(IEnumerable<Game> newList)
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
