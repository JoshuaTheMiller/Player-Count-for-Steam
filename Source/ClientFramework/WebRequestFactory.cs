namespace Trfc.ClientFramework
{
    public static class WebRequestFactory
    {
        public static WebRequestResponse<T> Success<T>(T value)
        {
            return new WebRequestResponse<T>(value, true, WebRequestResponseResultCode.Succeeded, string.Empty);
        }

        public static WebRequestResponse<T> Cancelled<T>()
        {
            return new WebRequestResponse<T>(default(T), false, WebRequestResponseResultCode.Cancelled, string.Empty);
        }

        public static WebRequestResponse<T> Errored<T>(string message)
        {
            return new WebRequestResponse<T>(default(T), false, WebRequestResponseResultCode.Errored, message);
        }
    }
}
