using Newtonsoft.Json;
using Trfc.ClientFramework;

namespace SteamStatsApp
{
    public sealed class StringDeserializer : IStringDeserializer
    {
        public T Deserialize<T>(string text)
        {
            return JsonConvert.DeserializeObject<T>(text);
        }
    }
}
