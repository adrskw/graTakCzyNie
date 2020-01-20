using System;
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

        public int moveCounter { get; private set; } = 0;
        private bool gameRunning = false;

        public List<Player> PlayersList { get; private set; } = new List<Player>();

        public async Task<EngineResult> StartGame()
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
                    board = new Board(50);
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
                List<Player> createdPlayers = new List<Player>();

                foreach (var item in players)
                {
                    if (createdPlayers.FirstOrDefault(f => f.Name == item.Name) != null)
                    {
                        return new EngineResult
                        {
                            Succedeed = false,
                            ErrorMessage = "Player " + item.Name + " is already exists!"
                        };
                    }

                    Player player = new Player(item.Name, item.Color, item.IsComputer);
                    createdPlayers.Add(player);
                }

                PlayersList = createdPlayers;

                return new EngineResult()
                {
                    Succedeed = true
                };
            });
        }

        public async Task<EngineResult> Move(Player player, int number)
        {
            EngineResult engineResult = new EngineResult();

            var targetedPlayer = PlayersList.FirstOrDefault(f => f.Name == player.Name);
            if (targetedPlayer == null)
            {
                engineResult.Succedeed = false;
                engineResult.ErrorMessage = "Player not found! " + player.Name;
                return engineResult;
            }
            else if (targetedPlayer.ImprisonedTo > moveCounter)
            {
                engineResult.Succedeed = false;
                engineResult.Player = targetedPlayer;
                engineResult.ErrorMessage = "Player " + targetedPlayer.Name + " is in a trap";
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
                    targetedPlayer.ImprisonedTo = moveCounter + 3;
                    engineResult.Succedeed = true;
                    break;
                case Field.Normal:
                    engineResult.Succedeed = true;
                    break;
                default:
                    return new EngineResult
                    {
                        Succedeed = false,
                        ErrorMessage = "Problem with Board! Returns: " + boardResult
                    };
            }

            moveCounter++;

            return engineResult;
        }
    }
}
