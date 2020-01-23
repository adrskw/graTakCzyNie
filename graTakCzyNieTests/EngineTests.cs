using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using graTakCzyNieLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace graTakCzyNie.Tests
{
    [TestClass]
    public class EngineTests
    {
        Engine engine = new Engine();
        List<Player> playerList = new List<Player>();

        [TestMethod]
        public async Task If_CreatePlayersSuccedeed_ReturnTrue()
        {
            playerList.Add(new Player(1, "Gracz", "Red"));
            var result = await engine.CreatePlayers(playerList);
            Assert.IsTrue(result.Succedeed);
        }

        [TestMethod]
        public async Task If_PlayerNameAlreadyExists_ReturnFalse()
        {
            playerList.Add(new Player(1, "Gracz", "Red"));
            playerList.Add(new Player(1, "Gracz", "Red"));
            var result = await engine.CreatePlayers(playerList);
            Assert.IsFalse(result.Succedeed);
        }

        [TestMethod]
        public async Task If_StartGameAndPlayersListIsNotSet_ReturnFalse()
        {
            var result = await engine.StartGame(50);
            Assert.IsFalse(result.Succedeed);
        }

        [TestMethod]
        public async Task If_StartGameAndPlayerListIsSet_ReturnTrue()
        {
            playerList.Add(new Player(1, "Gracz", "Red"));
            await engine.CreatePlayers(playerList);

            var result = await engine.StartGame(50);

            Assert.IsTrue(result.Succedeed);
        }
    }
}
