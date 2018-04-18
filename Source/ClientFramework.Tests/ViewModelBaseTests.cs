using System;
using System.Threading;
using System.Threading.Tasks;
using ClientFramework.Tests.Doppels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trfc.ClientFramework;

namespace ClientFramework.Tests
{
    [TestClass]
    public class ViewModelBaseTests
    {
        [TestMethod]
        public async Task RefreshCausesIsRefreshingToBeTrue()
        {
            DoppelCommandFactory.SetupStaticCommandFactory();

            var actual = false;

            var viewModel = new ViewModel()
            {
                Callback = (e) => 
                {
                    actual = e;
                }
            };     

            await viewModel.Refresh();            

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public async Task WhenRefreshFinishesIsRefreshIsFalse()
        {
            DoppelCommandFactory.SetupStaticCommandFactory();

            var viewModel = new ViewModel();

            await viewModel.Refresh();

            var actual = viewModel.IsRefreshing;

            Assert.IsFalse(actual);
        }

        private sealed class ViewModel : ViewModelBase
        {
            public Action<bool> Callback { get; set; } 

            protected override async Task TasksToExecuteWhileRefreshing(CancellationToken token)
            {
                await base.TasksToExecuteWhileRefreshing(token);

                Callback?.Invoke(this.IsRefreshing);
            }
        }
    }
}
