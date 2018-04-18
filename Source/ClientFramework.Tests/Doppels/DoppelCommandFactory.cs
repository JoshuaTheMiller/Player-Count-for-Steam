using System;
using System.Windows.Input;
using Trfc.ClientFramework;

namespace ClientFramework.Tests.Doppels
{
    internal sealed class DoppelCommandFactory : ICommandFactory
    {
        public ICommand Create(Action action, Func<bool> canExecute)
        {
            return new DoppelCommand(action);
        }

        public ICommand Create(Action<object> action)
        {
            return new DoppelCommand<object>(action);
        }

        public ICommand Create(Action action)
        {
            return new DoppelCommand(action);
        }

        public static void SetupStaticCommandFactory()
        {
            CommandFactory.CommandFactoryInstance = new DoppelCommandFactory();
        }
    }
}
