using System.Threading.Tasks;

namespace Trfc.ClientFramework
{
    public interface IRefreshable
    {
        bool IsRefreshing { get; }        

        Task Refresh();
    }
}