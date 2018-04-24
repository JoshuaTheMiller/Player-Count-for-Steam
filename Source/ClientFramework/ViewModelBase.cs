using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Trfc.ClientFramework
{
    [Preserve(AllMembers = true)]
    public abstract class ViewModelBase : BindableObject, IRefreshable
    {
        private CancellationTokenSource cancellationTokenSource;

        private bool isRefreshing = false;        
        public bool IsRefreshing
        {
            get => isRefreshing;
            private set => SetField(ref isRefreshing, value);
        }
        
        public ICommand RefreshCommand { get; }

        protected ViewModelBase()
        {
            RefreshCommand = CommandFactory.Create(async () => await Refresh(), () => !IsRefreshing);
        }

        private object refreshLock = new object();
        public async Task Refresh()
        {
            try
            {
                lock (refreshLock)
                {
                    if (IsRefreshing)
                    {
                        cancellationTokenSource.Cancel();
                    }

                    cancellationTokenSource = new CancellationTokenSource();

                    IsRefreshing = true;
                }

                await TasksToExecuteWhileRefreshing(cancellationTokenSource.Token);
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (TaskCanceledException e)
#pragma warning restore CS0168 // Variable is declared but never used
            {                
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (OperationCanceledException e)
#pragma warning restore CS0168 // Variable is declared but never used
            {                
            }

            IsRefreshing = false;
        }

        protected virtual Task TasksToExecuteWhileRefreshing(CancellationToken token) => Task.FromResult(default(object));
    }
}
