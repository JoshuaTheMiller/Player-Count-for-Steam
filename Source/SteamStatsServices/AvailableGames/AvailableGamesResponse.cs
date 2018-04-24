using System.Collections.Generic;
using System.Linq;

namespace Trfc.SteamStats.ClientServices.AvailableGames
{
    public sealed class AvailableGamesResponse : Response
    {
        private AvailableGamesResponse(IEnumerable<Game> games, bool succeeded, string message)
            : base(succeeded, message)
        {
            Games = games;
        }

        public IEnumerable<Game> Games { get; }

        public static AvailableGamesResponse Succeeded(IEnumerable<Game> games)
        {
            return new AvailableGamesResponse(games, true, string.Empty);
        }

        public static AvailableGamesResponse Failed(string message)
        {
            return new AvailableGamesResponse(Enumerable.Empty<Game>(), false, message);
        }
    }
}
