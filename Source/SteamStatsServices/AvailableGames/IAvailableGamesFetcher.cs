using System.Threading;
using System.Threading.Tasks;

namespace Trfc.SteamStats.ClientServices.AvailableGames
{
    public interface IAvailableGamesFetcher
    {
        Task<AvailableGamesResponse> FetchGamesAsync(CancellationToken token);
    }
}