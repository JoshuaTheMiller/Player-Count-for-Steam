using Microsoft.VisualStudio.TestTools.UnitTesting;
using SteamStatsApp.AvailableGames;
using SteamStatsApp.Main;
using SteamStatsApp.Tests.Doppels;
using System.Threading.Tasks;

namespace SteamStatsApp.Tests
{
    [TestClass]
    public class MainPageViewModelTests
    {
        [TestMethod]
        public async Task SearchIgnoresCase()
        {
            DoppelCommandFactory.SetupStaticCommandFactory();
            var doppelFetcher = new DoppelGameFetcher()
            {
                Games = new Game[] 
                {
                    NewGame("SoA", 1),
                    NewGame("s", 2)                    
                }
            };
            var viewModel = new MainPageViewModel(doppelFetcher);
            await viewModel.Refresh();

            viewModel.SearchText = "s";

            var expectedCount = 2;
            var actualCount = viewModel.Games.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }         

        private Game NewGame(string name, int id)
        {
            return new Game()
            {
                Name = name,
                Id = id.ToString()
            };
        }
    }
}
