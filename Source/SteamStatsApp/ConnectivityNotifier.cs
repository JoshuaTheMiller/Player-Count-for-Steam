using Trfc.ClientFramework;
using Trfc.ClientFramework.Connectivity;

namespace SteamStatsApp
{
    public sealed class ConnectivityNotifier
    {
        private readonly IToastMessageService toastMessageService;

        public ConnectivityNotifier(INetworkChecker networkChecker,
            IToastMessageService toastMessageService)
        {
            this.toastMessageService = toastMessageService;
            networkChecker.ConnectivityChanged += OnConnectivityChanged;            
        }

        private void OnConnectivityChanged(object sender, ConnectionStatusChangedArgs e)
        {
            if(!e.IsNowConnected)
            {
                toastMessageService.ShortAlert("Internet Connection Lost. An internet connection is necessary for this app to work!");
            }
        }
    }
}
