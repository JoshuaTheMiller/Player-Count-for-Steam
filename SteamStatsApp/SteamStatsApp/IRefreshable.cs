using System.Threading.Tasks;
using System.Windows.Input;

namespace SteamStatsApp
{
    internal interface IRefreshable
    {
        bool IsRefreshing { get; }

        ICommand RefreshCommand { get; }

        Task Refresh();
    }
}