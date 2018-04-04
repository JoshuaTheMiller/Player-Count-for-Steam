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

        public ICommand RefreshCommand { get; }

        protected ViewModelBase()
        {
            RefreshCommand =  CommandFactory.Create(async () => await Refresh(), () => !IsRefreshing);
        }

        public async Task Refresh()
        {
            if (IsRefreshing)
            {
                return;
            }

            await TasksToExecuteWhileRefreshing();
        }

        protected virtual Task TasksToExecuteWhileRefreshing() => Task.FromResult(default(object));        
    }
}
