using Android.App;
using Android.Widget;
using Trfc.ClientFramework;

namespace SteamStatsApp.Droid
{
    public sealed class ToastMessageService : IToastMessageService
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }
    }
}