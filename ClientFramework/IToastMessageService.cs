namespace Trfc.ClientFramework
{
    public interface IToastMessageService
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
