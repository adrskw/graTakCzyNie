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

        [TestMethod]
        public async Task If_TryingMovePlayerWhenGameIsNotRunning_ReturnFalse()
        { 
            var result = await engine.Move(new Player(2, "Test", "Green"), 1);
            Assert.IsFalse(result.Succedeed);
        }

        [TestMethod]
        public async Task If_TryingMovePlayerWhoIsNotOnPlayersList_ReturnFalse()
        {
            playerList.Add(new Player(1, "Gracz", "Red"));
            await engine.CreatePlayers(playerList);
            await engine.StartGame(50);

            var result = await engine.Move(new Player(2, "Test", "Green"), 1);

            Assert.IsFalse(result.Succedeed);
        }

        [TestMethod]
        public async Task If_TryingMovePlayerWhoIsInTrap_ReturnFalse()
        {
            var player = new Player(1, "Gracz", "Red");
            playerList.Add(player);
            await engine.CreatePlayers(playerList);
            await engine.StartGame(50);
            engine.PlayersList[0].ImprisonedTo = 3;
            var result = await engine.Move(player, 5);

            Assert.IsFalse(result.Succedeed);
        }


        [TestMethod]
        public async Task If_TryingMovePlayerSuccedeed_ReturnTrue()
        {
            var player = new Player(1, "Gracz", "Red");
            playerList.Add(player);
            await engine.CreatePlayers(playerList);
            await engine.StartGame(50);
            var result = await engine.Move(player, 5);

            Assert.IsTrue(result.Succedeed);
        }


        [TestMethod]
        public async Task If_PlayerDoNotExistsOnList_PlayerAnswer_ReturnFalse()
        {
            var player = new Player(1, "Gracz", "Red");
            playerList.Add(player);
            var player2 = new Player(2, "Gracz2", "Red");
            await engine.CreatePlayers(playerList);
            await engine.StartGame(50);

            var result = await engine.PlayerAnswer(player2, 1, false);

            Assert.IsFalse(result.Succedeed);
        }

        [TestMethod]
        public async Task If_PlayerAnswerIsWrong_ReturnCorrectAnswerFalse()
        {
            var player = new Player(1, "Gracz", "Red");
            playerList.Add(player);
            await engine.CreatePlayers(playerList);
            await engine.StartGame(50);

            var result = await engine.PlayerAnswer(player, 1, false);

            Assert.IsFalse(result.Question.CorrectAnswer.Value);
        }

        [TestMethod]
        public async Task If_PlayerAnswerIsCorrect_ReturnCorrectAnswerTrue()
        {
            var player = new Player(1, "Gracz", "Red");
            playerList.Add(player);
            await engine.CreatePlayers(playerList);
            await engine.StartGame(50);

            var result = await engine.PlayerAnswer(player, 1, true);

            Assert.IsTrue(result.Question.CorrectAnswer.Value);
        }

    }
}
