using System.Collections.Generic;
using Xamarin.Forms;

namespace SteamStatsApp
{
    internal sealed class BootstrappedApplication
    {
        public BootstrappedApplication(IEnumerable<View> views)
        {
            Views = views;
        }

        public IEnumerable<View> Views { get; }
    }
}
