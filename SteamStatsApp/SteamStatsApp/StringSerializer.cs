using Newtonsoft.Json;
using Trfc.ClientFramework;

namespace SteamStatsApp
{
    public sealed class StringSerializer : IStringSerializer
    {
        public string Serialize<T>(T toSerialize)
        {
            return JsonConvert.SerializeObject(toSerialize);
        }
    }
}
