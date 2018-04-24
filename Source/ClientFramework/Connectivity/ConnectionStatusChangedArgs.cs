using System;

namespace Trfc.ClientFramework.Connectivity
{
    public sealed class ConnectionStatusChangedArgs : EventArgs
    {        
        public bool IsNowConnected { get; }

        private ConnectionStatusChangedArgs(bool isNowConnected)
        {
            IsNowConnected = isNowConnected;
        }

        public static ConnectionStatusChangedArgs ConnectionLost()
        {
            return new ConnectionStatusChangedArgs(false);
        }

        public static ConnectionStatusChangedArgs ConnectionGained()
        {
            return new ConnectionStatusChangedArgs(true);
        }
    }
}
