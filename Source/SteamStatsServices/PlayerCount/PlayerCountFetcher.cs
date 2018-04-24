using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trfc.ClientFramework;

namespace Trfc.SteamStats.ClientServices.PlayerCount
{
    public sealed class PlayerCountFetcher : IPlayerCountFetcher
    {
        private readonly string connectionStringKey = "PlayerCount";

        private readonly IConfigurationProvider configurationProvider;
        private readonly IWebGateway webGateway;

        public PlayerCountFetcher(IConfigurationProvider configurationProvider,
            IWebGateway webGateway)
        {
            this.configurationProvider = configurationProvider;
            this.webGateway = webGateway;
        }                   

        public async Task<PlayerCountResponse> RetrievePlayerCount(int gameId, CancellationToken cancellationToken)
        {            
            var endpoint = configurationProvider.GetConnectionStringById(connectionStringKey);

            endpoint += $"?appids={gameId}";

            var gatewayResponse = await webGateway.GetResponseFromEndpoint<ResponseDao>(endpoint, cancellationToken);

            if(!gatewayResponse.Succeeded)
            {
                return PlayerCountResponse.Failed();
            }

            var response = gatewayResponse.Value;

            var playerCountResponse = response.PlayerCounts.First();

            if(playerCountResponse == null || playerCountResponse.IsSupported == false)
            {
                return PlayerCountResponse.Failed();
            }

            return PlayerCountResponse.Success(playerCountResponse.PlayerCount);            
        }

        [Preserve(AllMembers = true)]
        private sealed class ResponseDao
        {            
            public IEnumerable<PlayerCountDao> PlayerCounts { get; }
            public ResponseDao(IEnumerable<PlayerCountDao> playerCounts)
            {
                PlayerCounts = playerCounts;
            }
        }

        [Preserve(AllMembers = true)]
        private sealed class PlayerCountDao
        {           
            public int Id { get; }
            public bool IsSupported { get; }
            public int PlayerCount { get; }
            public PlayerCountDao(int id, bool isSupported, int playerCount)
            {
                Id = id;
                IsSupported = isSupported;
                PlayerCount = playerCount;
            }
        }
    }
}
