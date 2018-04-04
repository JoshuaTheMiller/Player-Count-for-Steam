using System;
using System.Windows.Input;

namespace Trfc.ClientFramework
{
    public interface ICommandFactory
    {
        ICommand Create(Action action, Func<bool> canExecute);
        ICommand Create(Action<object> action);
        ICommand Create(Action action);
    }
}