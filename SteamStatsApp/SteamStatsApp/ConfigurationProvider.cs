using System.Collections.Generic;
using Trfc.ClientFramework;

namespace SteamStatsApp
{
    public sealed class ConfigurationProvider : IConfigurationProvider
    {        
        private readonly IDictionary<string, string> connectionStrings;

        public ConfigurationProvider(IDictionary<string, string> connectionStrings)
        {
            this.connectionStrings = connectionStrings;
        }

        public string GetConnectionStringById(string key)
        {
            return connectionStrings[key];
        }
    }
}
