using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace SteamStatsApp
{
    public static class CommandFactory
    {
        internal static ICommand Create(Action action, Func<bool> canExecute)
        {
            return new Command(action, canExecute);
        }

        internal static ICommand Create(Action<object> action)
        {
            return new Command(action);
        }

        internal static ICommand Create(Action action)
        {
            return new Command(action);
        }
    }
}
