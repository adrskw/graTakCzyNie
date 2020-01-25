using graTakCzyNieLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graTakCzyNieTests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public async Task If_MovePlayerSuccedeed_ReturnFieldType()
        {
            Board board = new Board(50);

            var result = await board.MovePlayer(new Player(1, "Gracz", "Red"), 5);

            Assert.IsInstanceOfType(result, typeof(Field));
        }
    }
}
