using System.Threading.Tasks;
using System.Windows.Input;

namespace SteamStatsApp
{
    public abstract class ViewModelBase : BindableObject, IRefreshable
    {    
        private bool isRefreshing = false;
        public bool IsRefreshing
        {
            get => isRefreshing;
            private set
            {
                isRefreshing = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand Refresh { get; }

        protected ViewModelBase()
        {
            Refresh =  CommandFactory.Create(async () => await RefreshWrapper(), () => !IsRefreshing);
        }

        private async Task RefreshWrapper()
        {            
            IsRefreshing = true;

            await TasksToExecuteWhileRefreshing();

            IsRefreshing = false;
        }

        protected virtual Task TasksToExecuteWhileRefreshing() => Task.FromResult(default(object));
    }
}
