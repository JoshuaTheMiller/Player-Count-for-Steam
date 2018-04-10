using SteamStatsApp.GameFavorites;
using Trfc.ClientFramework;

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
            set => SetField(ref isFavorited, value, FavoriteChanged);
        }

        private void FavoriteChanged(bool obj)
        {
            if(obj)
            {
                favoriter.FavoriteGameById(this.Id);
            }
            else
            {
                favoriter.UnfavoriteGameById(this.Id);
            }
        }

        public GameViewModel(string name, int id, bool isFavorited, IGameFavoriter favoriter)
        {
            Name = name;
            Id = id;
            this.isFavorited = isFavorited;
            this.favoriter = favoriter;
        }
    }
}
