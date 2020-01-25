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
        public Field CurrentField { get; set; }
        public int CurrentPosition { get; set; }
        public int ImprisonedTo { get; set; }

        public Player(int id, string name, string color, bool isComputer = false)
        {
            Id = id;
            Name = name;
            Color = color;
            IsComputer = isComputer;
            CurrentField = Field.Start;
        }
    }
}
