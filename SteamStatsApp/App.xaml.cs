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
            foreach(var view in bootstrappedApplication.Views)
            {
                await ((ViewModelBase)view.BindingContext).Refresh();
            };            
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override async void OnResume ()
		{
            foreach (var view in bootstrappedApplication.Views)
            {
                await((ViewModelBase)view.BindingContext).Refresh();
            };
        }
	}
}
