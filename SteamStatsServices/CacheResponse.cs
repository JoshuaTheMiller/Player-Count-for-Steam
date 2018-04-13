namespace Trfc.SteamStats.ClientServices
{
    public sealed class CacheResponse
    {
        public bool IsOutOfDate { get; private set; }        

        private CacheResponse() { }

        public static CacheResponse OutOfDate()
        {
            return new CacheResponse()
            {
                IsOutOfDate = true                
            };
        }

        public static CacheResponse NotOutOfDate()
        {
            return new CacheResponse()
            {
                IsOutOfDate = false                
            };
        }
    }
}