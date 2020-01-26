using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graTakCzyNieLibrary
{

    public class Board
    {
        public static Dictionary<int, Field> CreatedBoard { get; private set; }
        private static Random rnd = new Random();

        public Board(int fieldNumber)
        {
            CreatedBoard = null;
            CreateBoard(fieldNumber);
        }

        /// <summary>
        /// Tworzy plansze o określonej liczbie pól
        /// </summary>
        /// <param name="fieldNumber">Liczba pól</param>
        private void CreateBoard(int fieldNumber)
        {
            if (CreatedBoard == null)
            {
                CreatedBoard = new Dictionary<int, Field>() { { 0, Field.Start } };

                for (int i = 1; i < fieldNumber; i++)
                {
                    if (i == fieldNumber - 1)
                    {
                        CreatedBoard.Add(i, Field.Meta);
                        continue;
                    }
                    CreatedBoard.Add(i, GetRandomFieldAction());
                }
            }
        }

        private Field GetRandomFieldAction()
        {
            List<Field> fieldTypes = Enum.GetValues(typeof(Field)).Cast<Field>().ToList();
            fieldTypes.Remove(Field.Start);
            fieldTypes.Remove(Field.Meta);
            fieldTypes.Add(Field.Normal);
            fieldTypes.Add(Field.Normal);
            fieldTypes.Add(Field.Normal);
            fieldTypes.Add(Field.Normal);
            fieldTypes.Add(Field.Question);
            fieldTypes.Add(Field.Question);

            Field rndField = fieldTypes[rnd.Next(0, fieldTypes.Count - 1)];

            return rndField;
        }

        /// <summary>
        /// Przemieszcza gracza po planszy
        /// </summary>
        /// <param name="player">Gracz</param>
        /// <param name="fieldNumber">Liczba pól</param>
        /// <returns></returns>
        public async Task<Field> MovePlayer(Player player, int fieldNumber)
        {
            return await Task.Run(() =>
            {
                var result = player.CurrentPosition + fieldNumber;

                if (result < 0)
                {
                    result = player.CurrentPosition = 0;
                }
                else if (result >= CreatedBoard.Count)
                {
                    result = player.CurrentPosition = CreatedBoard.Count - 1;
                }
                else
                {
                    player.CurrentPosition = result;
                }

                var field = player.CurrentField = CreatedBoard.FirstOrDefault(f => f.Key == result).Value;
                return field;
            });
        }

    }
}
