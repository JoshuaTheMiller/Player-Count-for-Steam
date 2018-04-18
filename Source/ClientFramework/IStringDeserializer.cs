namespace Trfc.ClientFramework
{
    public interface IStringDeserializer
    {
        T Deserialize<T>(string text);
    }
}
