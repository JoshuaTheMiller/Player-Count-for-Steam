using System.Threading.Tasks;
using System.Windows.Input;

namespace Trfc.ClientFramework
{
    internal interface IRefreshable
    {
        bool IsRefreshing { get; }

        ICommand RefreshCommand { get; }

        Task Refresh();
    }
}