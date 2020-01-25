﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graTakCzyNieLibrary
{
    public class Engine
    {
        private Board board;
        public QuestionDatabase qDatabase { get; set; }
        public int MoveCounter { get; private set; } = 0;
        public int NextTurnPlayerId { get; private set; } = 1;
        private bool gameRunning = false;

        public List<Player> PlayersList { get; private set; } = new List<Player>();

        public async Task<EngineResult> StartGame(int fieldNumber)
        {
            return await Task.Run(() =>
            {
                if (PlayersList.Count == 0)
                {
                    return new EngineResult
                    {
                        Succedeed = false,
                        ErrorMessage = "Player list is not set!"
                    };
                }
                else if (gameRunning)
                {
                    return new EngineResult
                    {
                        Succedeed = false,
                        ErrorMessage = "Game is already running!"
                    };
                }
                else
                {
                    qDatabase = new QuestionDatabase();
                    board = new Board(fieldNumber);
                    gameRunning = true;
                    return new EngineResult
                    {
                        Succedeed = true
                    };
                }
            });
        }

        public async Task<EngineResult> CreatePlayers(List<Player> players)
        {
            return await Task.Run(() =>
            {
                PlayersList = new List<Player>();

                foreach (var item in players)
                {
                    if (item.Name == string.Empty)
                    {
                        return new EngineResult
                        {
                            Succedeed = false,
                            ErrorMessage = "Nie podano nazwy jednego z graczy!"
                        };
                    }

                    if (PlayersList.FirstOrDefault(f => f.Name == item.Name) != null)
                    {
                        return new EngineResult
                        {
                            Succedeed = false,
                            ErrorMessage = "Player " + item.Name + " is already exists!"
                        };
                    }

                    if (PlayersList.FirstOrDefault(f => f.Color == item.Color) != null)
                    {
                        return new EngineResult
                        {
                            Succedeed = false,
                            ErrorMessage = "Gracze nie mogą mieć tego samego koloru!"
                        };
                    }

                    Player player = new Player(item.Id, item.Name, item.Color, item.IsComputer);
                    PlayersList.Add(player);
                }

                return new EngineResult()
                {
                    Succedeed = true
                };
            });
        }

        public async Task<EngineResult> Move(Player player, int number)
        {
            EngineResult engineResult = new EngineResult();

            if (PlayersList.Count > player.Id + 1)
            {
                NextTurnPlayerId = player.Id + 1;
            }
            else
            {
                NextTurnPlayerId = 0;
            }

            var targetedPlayer = PlayersList.FirstOrDefault(f => f.Name == player.Name);
            if (!gameRunning)
            {
                engineResult.Succedeed = false;
                engineResult.ErrorMessage = "Gra nie jest uruchomiona!";
                return engineResult;
            }
            else if (targetedPlayer == null)
            {
                engineResult.Succedeed = false;
                engineResult.ErrorMessage = "Nie znaleziono takiego gracza! " + player.Name;
                return engineResult;
            }

            MoveCounter++;

            if (targetedPlayer.ImprisonedTo > MoveCounter)
            {
                engineResult.Succedeed = false;
                engineResult.Player = targetedPlayer;
                engineResult.ErrorMessage = "Gracz " + targetedPlayer.Name + " jest w pułapce";
                return engineResult;
            }

            engineResult.Player = targetedPlayer;

            var boardResult = await board.MovePlayer(targetedPlayer, number);

            engineResult.Field = boardResult;

            switch (boardResult)
            {
                case Field.Start:
                    engineResult.Succedeed = true;
                    break;
                case Field.Meta:
                    engineResult.Succedeed = true;
                    break;
                case Field.Question:
                    engineResult.Succedeed = true;
                    engineResult.Question = await qDatabase.GetRandomQuestion();
                    break;
                case Field.Penalty:
                    await board.MovePlayer(targetedPlayer, -3);
                    engineResult.Succedeed = true;
                    break;
                case Field.Bonus:
                    await board.MovePlayer(targetedPlayer, 3);
                    engineResult.Succedeed = true;
                    break;
                case Field.Trap:
                    targetedPlayer.ImprisonedTo = MoveCounter + 3 * PlayersList.Count + 1;
                    engineResult.Succedeed = true;
                    break;
                case Field.Normal:
                    engineResult.Succedeed = true;
                    break;
            }
            return engineResult;
        }

        public async Task<EngineResult> PlayerAnswer(Player player, int questionId, bool answer)
        {
            EngineResult engineResult = new EngineResult();

            var targetedPlayer = PlayersList.FirstOrDefault(f => f.Name == player.Name);
            if (targetedPlayer == null)
            {
                engineResult.Succedeed = false;
                engineResult.ErrorMessage = "Nie znaleziono takiego gracza! " + player.Name;
                return engineResult;
            }

            engineResult.Player = targetedPlayer;

            var checkAnswer = await qDatabase.CheckAnswer(questionId, answer);
            if (checkAnswer == null)
            {
                engineResult.Succedeed = false;
                engineResult.ErrorMessage = $"Pytanie o id {questionId} nie istnieje!";
                return engineResult;
            }
            else if (checkAnswer.Value)
            {

                targetedPlayer.AddPoints(2);
                await board.MovePlayer(targetedPlayer, 3);
                engineResult.Succedeed = true;
                engineResult.Question = new Question
                {
                    CorrectAnswer = true
                };
                return engineResult;
            }
            else
            {
                targetedPlayer.SubtractPoints(1);
                await board.MovePlayer(targetedPlayer, -3);
                engineResult.Succedeed = true;
                engineResult.Question = new Question
                {
                    CorrectAnswer = false
                };
                return engineResult;
            }
        }
    }
}

