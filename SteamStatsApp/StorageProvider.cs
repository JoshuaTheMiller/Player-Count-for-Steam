using System.Collections.Generic;
using System.Threading.Tasks;
using Trfc.ClientFramework;
using Xamarin.Forms;

namespace SteamStatsApp
{
    public sealed class StorageProvider<T> : IStorageProvider<T>
    {
        private readonly Application app;
        private readonly IStringDeserializer deserializer;
        private readonly IStringSerializer serializer;

        public StorageProvider(Application app, IStringDeserializer deserializer, IStringSerializer serializer)
        {
            this.app = app;
            this.deserializer = deserializer;
            this.serializer = serializer;
        }

        public Task<StorageRetrievalResponse<T>> Get(string key)
        {
            var properties = this.app.Properties;

            T thing = default(T);

            if (properties.TryGetValue(key, out object value))
            {
                thing = deserializer.Deserialize<T>((string)value);
            }

            if (thing != null)
            {
                return Task.FromResult(StorageRetrievalResponse<T>.CreateSucceeded(thing));
            }

            return Task.FromResult(StorageRetrievalResponse<T>.CreateFailed("Failed to retrieve data"));
        }

        public async Task<StorageOperationResponse> Update(string key, T value)
        {
            var properties = this.app.Properties;

            await AddKeyIfItDoesntExist(properties, key);

            properties[key] = serializer.Serialize(value);

            return StorageOperationResponse.CreateSucceeded();
        }

        private Task AddKeyIfItDoesntExist(IDictionary<string, object> properties, string key)
        {
            if (!properties.ContainsKey(key))
            {
                properties.Add(key, null);
            }

            return Task.FromResult(default(object));
        }
    }
}
