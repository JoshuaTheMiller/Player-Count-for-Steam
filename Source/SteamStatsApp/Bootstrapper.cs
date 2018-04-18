using SteamStatsApp.Favorites;
using SteamStatsApp.Main;
using System.Collections.Generic;
using Trfc.ClientFramework;
using Trfc.SteamStats.ClientServices.AvailableGames;
using Trfc.SteamStats.ClientServices.GameFavorites;
using Trfc.SteamStats.ClientServices.GamePictures;
using Trfc.SteamStats.ClientServices.PlayerCount;
using Xamarin.Forms;

namespace SteamStatsApp
{
    internal sealed class Bootstrapper
    {
        //TODO: Abstract Application
        public static BootstrappedApplication Bootstrap(IToastMessageService toastMessageService, Application app)
        {
            CommandFactory.CommandFactoryInstance = new XamarinCommandFactory();

            var mainEndpoint = "https://steamstatsapi.herokuapp.com/";
            var configurationValues = new Dictionary<string, string>()
            {
                { "AvailableGames", $"{mainEndpoint}api/v1.0/availablegames"},
                { "AvailableGamesCacheTime", $"{mainEndpoint}api/v1.0/cachedatetime/availablegames"},
                { "GamePicture", $"{mainEndpoint}api/v1.0/appHeader"},
                { "GamePictureCacheTime", $"{mainEndpoint}api/v1.0/cachedatetime/apppicture"},
                { "PlayerCount", $"{mainEndpoint}/api/v1.0/playercount"}
            };

            var stringSerializer = new StringSerializer();
            var stringDeserializer = new StringDeserializer();

            var webGateway = new WebGateway(stringDeserializer);
            var configurationProvider = new ConfigurationProvider(configurationValues);            

            var storageProvider = new StorageProvider<LocalGameFavorites.GameFavoritesDao>(app, stringDeserializer, stringSerializer);
            var cachedAvailableGamesStorageProvider = new StorageProvider<CachedGameList>(app, stringDeserializer, stringSerializer);

            var gameFavoriter = new LocalGameFavorites(storageProvider, toastMessageService);

            var availableGamesCacheChecker = new AvailableGamesCacheChecker(configurationProvider, webGateway);
            var availableGameFetcher = new OnlineGameFetcher(configurationProvider, webGateway);
            var gameFetcherCache = new GameFetcherCache(availableGameFetcher, cachedAvailableGamesStorageProvider, availableGamesCacheChecker);

            var gameViewModelFetcher = new GamesViewModelFetcher(gameFetcherCache, gameFavoriter, gameFavoriter, gameFavoriter);

            var mainPaigeViewModel = new MainPageViewModel(gameViewModelFetcher, gameFavoriter);

            var pictureFetcher = new GamePictureFetcher(configurationProvider, webGateway);
            var pictureCacheChecker = new PictureCacheChecker(configurationProvider, webGateway);
            var cachedPictureStorageProvider = new StorageProvider<CachedGamePicture>(app, stringDeserializer, stringSerializer);
            var pictureCacheFetcher = new PictureFetcherCache(pictureFetcher, cachedPictureStorageProvider, pictureCacheChecker);

            var playerCountFetcher = new PlayerCountFetcher(configurationProvider, webGateway);

            var favoritesViewModelFetcher = new FavoriteGamesViewModelFetcher(gameFetcherCache, gameFavoriter, gameFavoriter, pictureCacheFetcher, playerCountFetcher);
            var favoritesViewModel = new FavoritesViewModel(favoritesViewModelFetcher, gameFavoriter);
            var favoritesView = new FavoritesView()
            {
                BindingContext = favoritesViewModel
            };

            var gameListView = new MainPage
            {
                BindingContext = mainPaigeViewModel
            };

            return new BootstrappedApplication(new View[] 
            {
                favoritesView,
                gameListView
            });
        }
    }
}
