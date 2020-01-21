using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graTakCzyNieLibrary
{
    public class Player
    {
        public int Id { get; }
        public string Name { get; }
        public string Color { get; }
        public bool IsComputer { get; }
        public ushort Score { get; set; }
        public Field CurrentField { get; set; }
        public int CurrentPosition { get; set; }
        public int ImprisonedTo { get; set; }

        public Player(int id, string name, string color, bool isComputer = false)
        {
            Id = id;
            Name = name;
            Color = color;
            IsComputer = isComputer;
            Score = 0;
            CurrentField = Field.Start;
        }

        public void AddPoints(ushort points)
        {
            Score += points;
        }

        public void SubtractPoints(ushort points)
        {
            if ((Score - points) >= 0)
                Score -= points;
            else
                Score = 0;
        }
    }
}
