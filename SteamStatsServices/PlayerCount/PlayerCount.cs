namespace Trfc.SteamStats.ClientServices.PlayerCount
{
    public sealed class PlayerCount
    {
        public int GameId { get; }

        public int Count { get; }

        public bool IsGameSupported { get; }

        public PlayerCount(int gameId, int count, bool isGameSupported)
        {
            GameId = gameId;
            Count = count;
            IsGameSupported = isGameSupported;
        }
    }
}
