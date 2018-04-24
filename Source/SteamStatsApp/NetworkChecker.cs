using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System;
using Trfc.ClientFramework.Connectivity;

namespace SteamStatsApp
{
    public sealed class NetworkChecker : INetworkChecker
    {
        public event EventHandler<ConnectionStatusChangedArgs> ConnectivityChanged;

        public NetworkChecker()
        {
            CrossConnectivity.Current.ConnectivityChanged += OnConnectivityChanged;
        }

        private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            ConnectionStatusChangedArgs args;

            if (e.IsConnected)
            {
                args = ConnectionStatusChangedArgs.ConnectionGained();
            }
            else
            {
                args = ConnectionStatusChangedArgs.ConnectionLost();
            }

            ConnectivityChanged?.Invoke(this, args);
        }

        public bool HasInternetAccess()
        {                        
            return CrossConnectivity.Current.IsConnected;
        }
    }
}
