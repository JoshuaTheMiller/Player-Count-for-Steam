namespace Trfc.ClientFramework
{
    public interface IStringSerializer
    {
        string Serialize<T>(T toSerialize);
    }
}
