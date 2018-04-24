using SteamStatsApp.Shell;
using Trfc.ClientFramework;
using Xamarin.Forms;

namespace SteamStatsApp
{
    public partial class App : Application
	{
        private readonly BootstrappedApplication bootstrappedApplication;

		public App(IToastMessageService toastMessageService)
		{
            this.bootstrappedApplication = Bootstrapper.Bootstrap(toastMessageService, this);

            var shell = new TabbedMainView()
            {
                ItemsSource = this.bootstrappedApplication.Views
            };

            MainPage = shell;

            InitializeComponent();            
        }

		protected override async void OnStart ()
		{
            await bootstrappedApplication.RefreshAllViewsAsync();
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override async void OnResume ()
		{
            await bootstrappedApplication.RefreshAllViewsAsync();
        }
	}
}
