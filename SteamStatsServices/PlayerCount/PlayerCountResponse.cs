namespace Trfc.SteamStats.ClientServices.PlayerCount
{
    public sealed class PlayerCountResponse
    {       
        public int PlayerCount { get; }

        public bool CountCheckWasSuccessfull { get; }

        private PlayerCountResponse(int playerCount, bool wasSuccessfull)
        {
            PlayerCount = playerCount;
            CountCheckWasSuccessfull = wasSuccessfull;
        }

        internal static PlayerCountResponse Success(int coutOfPlayers)
        {
            return new PlayerCountResponse(coutOfPlayers, true);
        }

        internal static PlayerCountResponse Failed()
        {
            return new PlayerCountResponse(-1, true);
        }
    }
}
