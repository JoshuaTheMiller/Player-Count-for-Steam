namespace Trfc.ClientFramework
{
    public class StorageRetrievalResponse<T> : StorageRetrievalResponse
    {
        new T Value { get; }

        protected StorageRetrievalResponse(T value, bool succeeded, string message)
            : base(value, succeeded, message)
        {
            Value = value;
        }

        public static StorageRetrievalResponse<T> CreateSucceeded(T value)
        {
            return new StorageRetrievalResponse<T>(value, true, string.Empty);
        }

        new public static StorageRetrievalResponse<T> CreateFailed(string message)
        {
            return new StorageRetrievalResponse<T>(default(T), false, message);
        }
    }


    public class StorageRetrievalResponse : StorageOperationResponse
    {
        public object Value { get; }

        protected StorageRetrievalResponse(object value, bool succeeded, string message)
            : base(succeeded, message)
        {
            this.Value = value;
        }

        public static StorageRetrievalResponse CreateSucceeded(object value)
        {
            return new StorageRetrievalResponse(value, true, string.Empty);
        }

        new public static StorageRetrievalResponse CreateFailed(string message)
        {
            return new StorageRetrievalResponse(null, false, message);
        }
    }

    public class StorageOperationResponse
    {
        public bool Succeeded { get; }

        public string Message { get; }

        protected StorageOperationResponse(bool succeeded, string message)
        {
            Succeeded = succeeded;
            Message = message;
        }

        public static StorageOperationResponse CreateSucceeded()
        {
            return new StorageOperationResponse(true, string.Empty);
        }

        public static StorageOperationResponse CreateFailed(string message)
        {
            return new StorageOperationResponse(false, message);
        }
    }
}
