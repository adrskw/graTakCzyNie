using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graTakCzyNieLibrary
{
    /// <summary>
    /// Model odpowiedzi klasy Engine
    /// </summary>
    public class EngineResult
    {
        /// <summary>
        /// Informacja czy operacja się powiodła
        /// </summary>
        public bool Succedeed { get; set; }

        /// <summary>
        /// Zawiera wylosowane pytanie
        /// </summary>
        public Question Question { get; set; }

        /// <summary>
        /// Typ pola na którym znalazł się gracz
        /// </summary>
        public Field Field { get; set; }

        /// <summary>
        /// Gracz
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// Zawiera błąd w przypadku niepowodzenia
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
