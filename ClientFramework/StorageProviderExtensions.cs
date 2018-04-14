using System.Threading.Tasks;

namespace Trfc.ClientFramework
{
    public static class StorageProviderExtensions
    {
        public static Task<StorageOperationResponse> Update<T>(this IStorageProvider<T> provider, string masterKey, string itemId, T value)
        {
            var compositeKey = CreateCompositeKey(masterKey, itemId);
            return provider.Update(compositeKey, value);
        }

        public static Task<StorageRetrievalResponse<T>> Get<T>(this IStorageProvider<T> provider, string masterKey, string itemId)
        {
            var compositeKey = CreateCompositeKey(masterKey, itemId);
            return provider.Get(compositeKey);
        }

        private static string CreateCompositeKey(string masterKey, string itemId)
        {
            return $"{masterKey}{itemId}";
        }
    }
}
