using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Trfc.ClientFramework;
using Trfc.SteamStats.ClientServices.GameFavorites;
using Trfc.SteamStats.ClientServices.GamePictures;
using Trfc.SteamStats.ClientServices.PlayerCount;

namespace SteamStatsApp.Favorites
{
    [Preserve(AllMembers = true)]
    public sealed class FavoriteGameViewModel : ViewModelBase
    {
        private readonly IGameFavoriter favoriter;
        private readonly IGamePictureFetcher pictureFetcher;
        private readonly IPlayerCountFetcher playerCountFetcher;

        public string Name { get; }

        public int Id { get; }

        private bool isFavorited = false;
        public bool IsFavorited
        {
            get => isFavorited;
            private set => SetField(ref isFavorited, value);
        }

        private byte[] image;
        public byte[] Image
        {
            get => image;
            private set => SetField(ref image, value, AreByteArraysTheSame);
        }

        private int playerCount;
        public int PlayerCount
        {
            get => playerCount;
            private set => SetField(ref playerCount, value);
        }

        public ICommand ToggleFavoriteCommand { get; }

        public FavoriteGameViewModel(string name, int id, bool isFavorited,
            IGameFavoriter favoriter,
            IGamePictureFetcher pictureFetcher,
            IPlayerCountFetcher playerCountFetcher)
        {
            Name = name;
            Id = id;
            this.IsFavorited = isFavorited;
            this.favoriter = favoriter;
            this.pictureFetcher = pictureFetcher;
            this.playerCountFetcher = playerCountFetcher;
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

        protected override async Task TasksToExecuteWhileRefreshing(CancellationToken token)
        {
            var taskList = new[] { LoadPicture(token), LoadPlayerCount(token) };

            await Task.WhenAll(taskList);
        }

        private async Task LoadPlayerCount(CancellationToken token)
        {
            var response = await this.playerCountFetcher.RetrievePlayerCount(this.Id, token);

            if (response.CountCheckWasSuccessfull)
            {
                this.PlayerCount = response.PlayerCount;
            }
            else
            {
                this.PlayerCount = -1;
            }
        }

        private async Task LoadPicture(CancellationToken token)
        {
            var response = await this.pictureFetcher.FetchPictureForGameAsync(this.Id, token);

            if (response.HasPicture)
            {
                this.Image = response.Image;
            }
        }

        private bool AreByteArraysTheSame(byte[] original, byte[] newArray)
        {
            if (original == null && newArray == null)
            {
                return true;
            }

            if (original == null || newArray == null)
            {
                return false;
            }

            return original.SequenceEqual(newArray);
        }
    }
}