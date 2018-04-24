using System;
using Trfc.ClientFramework.Connectivity;

namespace Base.Tests.Doppels
{
    internal sealed class DoppelNetworkChecker : INetworkChecker
    {
        public event EventHandler<ConnectionStatusChangedArgs> ConnectivityChanged;

        public bool InternetAccess { get; set; }

        public bool HasInternetAccess()
        {
            return InternetAccess;
        }
    }
}
