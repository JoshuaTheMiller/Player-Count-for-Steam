using System.Threading.Tasks;
using System.Windows.Input;
using Trfc.ClientFramework;
using Trfc.SteamStats.ClientServices.GameFavorites;

namespace SteamStatsApp.Main
{
    public sealed class GameViewModel : ViewModelBase
    {
        private readonly IGameFavoriter favoriter;

        public string Name { get; }

        public int Id { get; }

        private bool isFavorited = false;
        public bool IsFavorited
        {
            get => isFavorited;
            private set => SetField(ref isFavorited, value);
        }    

        public ICommand ToggleFavoriteCommand { get; }
  
        public GameViewModel(string name, int id, bool isFavorited, IGameFavoriter favoriter)
        {
            Name = name;
            Id = id;
            this.IsFavorited = isFavorited;
            this.favoriter = favoriter;
            this.ToggleFavoriteCommand = CommandFactory.Create(async () => await ToggleFavorite());
        }

        /*
         * HACK
         * Possibly should have this check in the view model before even performing a service call.
         * This is probably a sign that there should be a view model that represents 
         * the list of all games (i.e. the MainViewModel at this point in time) so 
         * that checks may be done against the whole list.
        */
        private async Task ToggleFavorite()
        {
            bool result = false;

            var valueIfToggled = !this.IsFavorited;

            if (valueIfToggled)
            {
                result = await favoriter.FavoriteGameById(this.Id);

                IsFavorited = result;
            }
            else
            {
                result = await favoriter.UnfavoriteGameById(this.Id);

                IsFavorited = !result;
            } 
        }
    }
}
