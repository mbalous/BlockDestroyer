using System;
using System.Collections.Generic;

namespace BlockDestroyer.GameObjects
{
    /// <summary>
    ///     Class representing sigle game block.
    /// </summary>
    internal class BlockObject : GameObj
    {
        /// <summary>
        ///     Exact list of coordianates where is the board.
        /// </summary>
        public List<ConsolePoint> AbsolutXyPoints { get; private set; }

        /// <summary>
        ///     Width of the block
        /// </summary>
        public int Width { get; private set; }

        public BlockObject(int xColumn, int yRow, bool exists, byte width,byte spaces, char objectChar, bool isBonus,
            ConsoleColor color = ConsoleColor.White)
            : base(xColumn, yRow, objectChar, exists, color)
        {
            Width = width;
            AbsolutXyPoints = new List<ConsolePoint>();

            int row = 10 + (yRow * 2);
            for (int i = 0; i < width; i++)
            {
                if (i == 0)
                {
                    AbsolutXyPoints.Add(new ConsolePoint(x: (xColumn * (width + spaces)) + 1, y: row));
                    continue;
                }
                AbsolutXyPoints.Add(new ConsolePoint(x: AbsolutXyPoints[0].x + i, y: row));
            }
        }
    }
}