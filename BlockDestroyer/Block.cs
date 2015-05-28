using System;
using System.Collections.Generic;

namespace BlockDestroyer
{
    /// <summary>
    ///     Class representing sigle game block.
    /// </summary>
    internal class Block : GameObject
    {
        /// <summary>
        ///     Exact list of coordianates where is the board.
        /// </summary>
        public List<ConsolePoint> AbsolutXyPoints { get; private set;}

        /// <summary>
        ///     Width of the block
        /// </summary>
        public int Width { get; private set; }

        public Block(int xColumn, int yRow, bool exists, byte width, char objectChar, bool isBonus,
            ConsoleColor color = ConsoleColor.White)
            : base(xColumn, yRow, objectChar, exists, color)
        {
            Width = width;
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

    /// <summary>
    ///     Class representing exact position in console windows.
    /// </summary>
    internal class ConsolePoint
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