using System;

namespace Trfc.ClientFramework.Connectivity
{
    public interface INetworkChecker
    {
        event EventHandler<ConnectionStatusChangedArgs> ConnectivityChanged;

        bool HasInternetAccess();
    }
}