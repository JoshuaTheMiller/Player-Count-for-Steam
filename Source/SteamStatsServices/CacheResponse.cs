namespace Trfc.SteamStats.ClientServices
{
    public sealed class CacheResponse : Response
    {
        public bool IsOutOfDate { get; private set; }        

        private CacheResponse(string responseMessage, bool successful)
            : base(successful, responseMessage)
        { }

        public static CacheResponse OutOfDate()
        {
            return new CacheResponse(string.Empty, true)
            {
                IsOutOfDate = true                
            };
        }

        public static CacheResponse NotOutOfDate()
        {
            return new CacheResponse(string.Empty, true)
            {
                IsOutOfDate = false                
            };
        }

        public static CacheResponse CacheCheckFailed(string message)
        {
            return new CacheResponse(message, false);
        }
    }
}