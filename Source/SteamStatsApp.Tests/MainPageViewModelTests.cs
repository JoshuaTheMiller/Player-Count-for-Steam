using Microsoft.VisualStudio.TestTools.UnitTesting;
using SteamStatsApp.Main;
using SteamStatsApp.Tests.Doppels;
using System.Linq;
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
            var favoriteFetcher = new DoppelFavoriteGameFetcher();
            var querier = new DoppelFavoriteGameQuerier();
            var networkChecker = new NetworkChecker();
            var doppelFetcher = new DoppelGameFetcher()
            {
                Games = new GameViewModel[] 
                {
                    NewGame("SoA", 1, gameFavoriter, querier),
                    NewGame("s", 2, gameFavoriter, querier)                    
                }
            };
            var viewModel = new MainPageViewModel(doppelFetcher, favoriteFetcher, networkChecker);
            await viewModel.Refresh();

            viewModel.SearchText = "s";

            var expectedCount = 2;
            var actualCount = viewModel.Games.Count();

            Assert.AreEqual(expectedCount, actualCount);
        }         

        private GameViewModel NewGame(string name, int id, IGameFavoriter favoriter, IFavoriteGameQuerier gameQuerier)
        {
            return new GameViewModel(name, id, false, favoriter, gameQuerier);
        }
    }
}
