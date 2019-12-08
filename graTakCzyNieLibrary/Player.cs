using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graTakCzyNieLibrary
{
    public class Player
    {
        public string Name { get; }
        public string Color { get; }
        public bool IsComputer { get; }
        public ushort Score { get; set; }

        public Player(string name, string color, bool isComputer = false)
        {
            Name = name;
            Color = color;
            IsComputer = isComputer;
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
