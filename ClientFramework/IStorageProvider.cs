using System.Threading.Tasks;

namespace Trfc.ClientFramework
{
    public interface IStorageProvider<T>
    {
        Task<StorageOperationResponse> Update(string key, T value);

        Task<StorageRetrievalResponse<T>> Get(string key);
    }
}
