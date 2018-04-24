using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trfc.ClientFramework;
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

        public async Task RefreshAllViewsAsync()
        {
            var refreshTasks = Views
                .Select(view => view.BindingContext)
                .Cast<ViewModelBase>()
                .Select(viewModel => viewModel.Refresh())
                .ToList();

            await Task.WhenAll(refreshTasks);
        }        
    }
}
