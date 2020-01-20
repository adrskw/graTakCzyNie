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

        private bool gameRunning = false;

        public List<Player> PlayersList { get; private set; } = new List<Player>();

        public async Task<EngineResult> StartGame()
        {
            return await Task.Run(() =>
            {
                if (PlayersList is null)
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
    }
