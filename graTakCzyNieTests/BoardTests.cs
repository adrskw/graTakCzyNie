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
        Board board;

        [TestMethod]
        public void CreateBoardSuccess()
        {
            board = new Board(100);

            Assert.IsNotNull(Board.CreatedBoard);
        }
    }
}
