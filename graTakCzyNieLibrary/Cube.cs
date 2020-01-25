using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graTakCzyNieLibrary
{
    public class Cube
    {
        private static Random random = new Random();
        /// <summary>
        /// Losuje liczbę w podanym zakresie
        /// </summary>
        /// <param name="a">Liczba początkowa</param>
        /// <param name="b">Liczba końcowa</param>
        /// <returns></returns>
        public static async Task<int> GetRandomResult(int a, int b)
        {
            return await Task.Run(() =>
            {
                return random.Next(a, b);
            });
        }
    }
}
