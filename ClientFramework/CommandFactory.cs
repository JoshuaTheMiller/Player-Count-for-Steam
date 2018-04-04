using System;
using System.Windows.Input;

namespace Trfc.ClientFramework
{
    public static class CommandFactory
    {
        public static ICommandFactory CommandFactoryInstance { get; set; }

        public static ICommand Create(Action action, Func<bool> canExecute)
        {
            return CommandFactoryInstance.Create(action, canExecute);
        }

        public static ICommand Create(Action<object> action)
        {
            return CommandFactoryInstance.Create(action);
        }

        public static ICommand Create(Action action)
        {
            return CommandFactoryInstance.Create(action);
        }
    }
}
