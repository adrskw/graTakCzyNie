using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graTakCzyNieLibrary
{
    /// <summary>
    /// Klasa odpowiedzialna za gracza
    /// </summary>
    public class Player
    {
        /// <summary>
        /// ID Gracza
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Nazwa Gracza
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Kolor Gracza
        /// </summary>
        public string Color { get; }

        /// <summary>
        /// Obecne pole na planszy
        /// </summary>
        public Field CurrentField { get; set; }

        /// <summary>
        /// Obecna pozycja na planszy
        /// </summary>
        public int CurrentPosition { get; set; }

        /// <summary>
        /// Uwieziony do danego ruchu
        /// </summary>
        /// 
        public int ImprisonedTo { get; set; }

        /// <summary>
        /// Tworzy gracza i przypisuje mu pole startowe
        /// </summary>
        /// <param name="id">ID Gracza</param>
        /// <param name="name">Nazwa Gracza</param>
        /// <param name="color">Kolor Gracza</param>
        public Player(int id, string name, string color)
        {
            Id = id;
            Name = name;
            Color = color;
            CurrentField = Field.Start;
        }
    }
}
