namespace Trfc.ClientFramework
{
    public sealed class WebRequestResponse<T>
    {        
        public T Value { get; }

        public bool Succeeded { get; } = true;

        public WebRequestResponseResultCode ResultCode { get; } = WebRequestResponseResultCode.Errored;

        public string ResultMessage { get; }

        internal WebRequestResponse(T value, 
            bool succeeded,
            WebRequestResponseResultCode resultCode,
            string resultMessage)
        {
            Value = value;
            Succeeded = succeeded;
            ResultCode = resultCode;
            ResultMessage = resultMessage;
        }        
    }
}
