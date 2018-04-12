using Microsoft.VisualStudio.TestTools.UnitTesting;
using SteamStatsApp.Main;
using SteamStatsApp.Tests.Doppels;
using System.Threading.Tasks;
using Trfc.SteamStats.ClientServices.GameFavorites;

namespace SteamStatsApp.Tests
{
    [TestClass]
    public class MainPageViewModelTests
    {
        [TestMethod]
        public async Task SearchIgnoresCase()
        {
            DoppelCommandFactory.SetupStaticCommandFactory();
            var gameFavoriter = new DoppelGameFavoriter();
            var doppelFetcher = new DoppelGameFetcher()
            {
                Games = new GameViewModel[] 
                {
                    NewGame("SoA", 1, gameFavoriter),
                    NewGame("s", 2, gameFavoriter)                    
                }
            };
            var viewModel = new MainPageViewModel(doppelFetcher);
            await viewModel.Refresh();

            viewModel.SearchText = "s";

            var expectedCount = 2;
            var actualCount = viewModel.Games.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }         

        private GameViewModel NewGame(string name, int id, IGameFavoriter favoriter)
        {
            return new GameViewModel(name, id, false, favoriter);
        }
    }
}
