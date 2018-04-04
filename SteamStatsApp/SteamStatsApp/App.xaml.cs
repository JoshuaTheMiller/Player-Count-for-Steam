using SteamStatsApp.AvailableGames;
using SteamStatsApp.Main;
using Trfc.ClientFramework;
using Xamarin.Forms;

namespace SteamStatsApp
{
    public partial class App : Application
	{
		public App ()
		{
            CommandFactory.CommandFactoryInstance = new XamarinCommandFactory();

            var viewModel = new MainPageViewModel(new HardCodedGameFetcher());

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
