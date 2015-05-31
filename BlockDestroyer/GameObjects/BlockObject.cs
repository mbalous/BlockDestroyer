using System;
using System.Collections;
using System.Collections.Generic;

namespace BlockDestroyer.GameObjects
{
    /// <summary>
    ///     Class representing sigle game block.
    /// </summary>
    internal class BlockObject : GameObj
    {
        public BlockObject(int xColumn, int yRow, bool exists, byte width, byte spaces, char objectChar, bool isBonus,
            ConsoleColor color = ConsoleColor.White)
            : base(xColumn, yRow, objectChar, exists, color)
        {
            Width = width;
            AbsolutXyPoints = GetBlockExactPosition(xColumn, yRow, width, spaces);
        }

        /// <summary>
        ///     Width of the block
        /// </summary>
        public int Width { get; private set; }

        private List<ConsolePoint> GetBlockExactPosition(int xColumn, int yRow, byte width, byte spaces)
        {
            List<ConsolePoint> blockConsolePoints = new List<ConsolePoint>(width);
            int row = 10 + (yRow*2);
            for (int i = 0; i < width; i++)
            {
                if (i == 0)
                {
                    blockConsolePoints.Add(new ConsolePoint((xColumn*(width + spaces)) + 5, row));
                    continue;
                }
                blockConsolePoints.Add(new ConsolePoint(blockConsolePoints[0].x + i, row));
            }
            return blockConsolePoints;
        }
    }
}