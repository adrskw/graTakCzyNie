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
            CreateBoard(fieldNumber);
        }

        private async void CreateBoard(int fieldNumber)
        {
            if (CreatedBoard == null)
            {
                CreatedBoard = new Dictionary<int, Field>() { { 0, Field.Start } };
            }
            for (int i = 1; i <= fieldNumber; i++)
            {
                if (i == fieldNumber)
                {
                    CreatedBoard.Add(i, Field.Meta);
                    continue;
                }
                CreatedBoard.Add(i, await GetRandomFieldAction());
            }
        }      

        private async Task<Field> GetRandomFieldAction()
        {
            Array val = Enum.GetValues(typeof(Field));
            Field rndField = (Field)val.GetValue(rnd.Next(val.Length));
            if (rndField == Field.Start || rndField == Field.Meta)
                return await GetRandomFieldAction();
            return rndField;
        }

        public async Task<Field> MovePlayer(Player player, int fieldNumber)
        {
            return await Task.Run(() =>
            {
                var result = player.CurrentPosition += fieldNumber;
                var field = player.CurrentField = CreatedBoard.FirstOrDefault(f => f.Key == result).Value;
                return field;
            });
        }

    }
}
