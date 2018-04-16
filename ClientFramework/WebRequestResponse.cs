namespace Trfc.ClientFramework
{
    public sealed class WebRequestResponse<T>
    {        
        public T Value { get; }

        public bool Succeeded { get; } = true;

        internal WebRequestResponse(T value, bool succeeded)
        {
            Value = value;
            Succeeded = succeeded;
        }        
    }
}
