namespace Trfc.SteamStats.ClientServices
{
    public abstract class Response
    {        
        public bool Successful { get; }
        public string Message { get; }

        protected Response(bool successful, string message)
        {
            Message = message;
            Successful = successful;
        }
    }
}
