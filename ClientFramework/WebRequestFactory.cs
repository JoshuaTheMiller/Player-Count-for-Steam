namespace Trfc.ClientFramework
{
    public static class WebRequestFactory
    {
        public static WebRequestResponse<T> Success<T>(T value)
        {
            return new WebRequestResponse<T>(value, true);
        }

        public static WebRequestResponse<T> Cancelled<T>()
        {
            return new WebRequestResponse<T>(default(T), false);
        }
    }
}
