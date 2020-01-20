using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graTakCzyNieLibrary
{
    public class EngineResult
    {
        public bool Succedeed { get; set; }
        public Field Field { get; set; }
        public Player Player { get; set; }
        public string ErrorMessage { get; set; }
    }
}
