using System;
using System.Windows.Input;

namespace ClientFramework.Tests.Doppels
{
    internal sealed class DoppelCommand<T> : DoppelCommand
    {
        private readonly Action<T> action;                

        new public void Execute(object parameter)
        {
            action.Invoke((T) parameter);
        }

        public DoppelCommand(Action<T> action)
        {
            this.action = action;
        }
    }

    internal class DoppelCommand : ICommand
    {
        private readonly Action parameterlessAction;        

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            parameterlessAction.Invoke();
        }

        public DoppelCommand() { }

        public DoppelCommand(Action action)
        {
            this.parameterlessAction = action;
        }
    }
}
