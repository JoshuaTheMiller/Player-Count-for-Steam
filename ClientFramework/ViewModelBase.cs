using System.Threading.Tasks;
using System.Windows.Input;

namespace Trfc.ClientFramework
{
    public abstract class ViewModelBase : BindableObject, IRefreshable
    {    
        private bool isRefreshing = false;
        public bool IsRefreshing
        {
            get => isRefreshing;
            private set => SetField(ref isRefreshing, value);
        }

        public ICommand RefreshCommand { get; }

        protected ViewModelBase()
        {
            RefreshCommand =  CommandFactory.Create(async () => await Refresh(), () => !IsRefreshing);
        }

        public async Task Refresh()
        {
            //Threading issue?
            if (IsRefreshing)
            {
                return;
            }

            IsRefreshing = true;

            await TasksToExecuteWhileRefreshing();

            IsRefreshing = false;
        }

        protected virtual Task TasksToExecuteWhileRefreshing() => Task.FromResult(default(object));        
    }
}
