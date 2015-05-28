using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockDestroyer
{
    class Block : GameObject
    {
        /// <summary>
        ///     Exact list of coordianates where is the board.
        /// </summary>
        public List<ConsolePoint> AbsolutXyPoints { get; }

        public Block(int xColumn, int yRow, bool exists, ConsoleColor color = ConsoleColor.White) : base(xColumn, yRow, exists, color)
        {

            AbsolutXyPoints = new List<ConsolePoint>();

            int row = 10 + (yRow * 2);
            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    AbsolutXyPoints.Add(new ConsolePoint(xColumn * 5, row));
                    continue;
                }
                AbsolutXyPoints.Add(new ConsolePoint(AbsolutXyPoints[0].x + i, row));
            }
        }
    }


    class ConsolePoint
    {
        public int x;
        public int y;

        public ConsolePoint(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}