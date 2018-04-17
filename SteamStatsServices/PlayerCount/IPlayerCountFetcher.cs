using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Trfc.SteamStats.ClientServices.PlayerCount
{
    public interface IPlayerCountFetcher
    {
        Task<PlayerCountResponse> RetrievePlayerCount(int gameId, CancellationToken cancellationToken);
    }
}