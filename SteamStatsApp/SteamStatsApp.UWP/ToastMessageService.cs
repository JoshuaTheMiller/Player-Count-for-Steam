using Plugin.Toasts;
using Plugin.Toasts.UWP;
using Trfc.ClientFramework;

namespace SteamStatsApp.UWP
{
    public sealed class ToastMessageService : IToastMessageService
    {
        private readonly IToastNotificator toastNotificator;

        public ToastMessageService()
        {
            toastNotificator = new ToastNotification();
            ToastNotification.Init();
        }

        public void LongAlert(string message)
        {
            toastNotificator.Notify(new NotificationOptions()
            {
                Description = message,
                ClearFromHistory = true
            });
        }

        public void ShortAlert(string message)
        {
            toastNotificator.Notify(new NotificationOptions()
            {
                Description = message,
                ClearFromHistory = true               
            });
        }
    }
}
