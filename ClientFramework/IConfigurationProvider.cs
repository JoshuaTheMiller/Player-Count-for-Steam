namespace Trfc.ClientFramework
{
    public interface IConfigurationProvider
    {
        string GetConnectionStringById(string key);
    }
}
