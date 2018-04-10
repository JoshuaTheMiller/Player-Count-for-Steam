using SteamStatsApp.AvailableGames;
using SteamStatsApp.GameFavorites;
using SteamStatsApp.Main;
using System.Collections.Generic;
using Trfc.ClientFramework;
using Xamarin.Forms;

namespace SteamStatsApp
{
    public partial class App : Application
	{
		public App ()
		{
            CommandFactory.CommandFactoryInstance = new XamarinCommandFactory();
            
            var mainEndpoint = "https://steamstatsapi.herokuapp.com/";
            var configurationValues = new Dictionary<string,string>()
            {
                { "AvailableGames", $"{mainEndpoint}/api/v1.0/availablegames"}                
            };
            var webGateway = new WebGateway();
            var configurationProvider = new ConfigurationProvider(configurationValues);
            var availableGameFetcher = new OnlineGameFetcher(configurationProvider, webGateway);
            var gameFavoriter = new InMemoryGameFavorites();
            var gameFetcher = new GamesViewModelFetcher(availableGameFetcher, gameFavoriter, gameFavoriter);
            var viewModel = new MainPageViewModel(gameFetcher);

            MainPage = new MainPage
            {
                BindingContext = viewModel
            };

            InitializeComponent();
            viewModel.RefreshCommand.Execute(null);
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
