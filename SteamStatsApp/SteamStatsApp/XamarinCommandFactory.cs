using System;
using System.Windows.Input;
using Trfc.ClientFramework;
using Xamarin.Forms;

namespace SteamStatsApp
{
    public sealed class XamarinCommandFactory : ICommandFactory
    {
        public ICommand Create(Action action, Func<bool> canExecute)
        {
            return new Command(action, canExecute);
        }

        public ICommand Create(Action<object> action)
        {
            return new Command(action);
        }

        public ICommand Create(Action action)
        {
            return new Command(action);
        }
    }
}
