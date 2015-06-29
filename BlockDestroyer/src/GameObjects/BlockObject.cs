using System;
using System.Collections.Generic;

namespace BlockDestroyer.GameObjects
{
    /// <summary>
    ///     Class representing sigle game block.
    /// </summary>
    internal class BlockObject : GameObject
    {
        public BlockObject(int xColumn, int yRow, bool exists, byte width, byte spaces, char objectChar, bool isBonus,
            ConsoleColor color = ConsoleColor.White)
            : base(xColumn, yRow, objectChar, exists, color)
        {
            Width = width;
            IsBonus = isBonus;
            AbsolutXyPoints = GetBlockExactPosition(xColumn, yRow, width, spaces);

            if (isBonus)
            {
                Color = ConsoleColor.Red;
            }
        }

        /// <summary>
        ///     Width of the block
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        ///     Is the block a bonus block?
        /// </summary>
        public bool IsBonus { get; set; }

        /// <summary>
        ///     Function calculates exact block position
        /// </summary>
        /// <param name="xColumn">Column</param>
        /// <param name="yRow">Row</param>
        /// <param name="width">Width</param>
        /// <param name="spaces">Spaces between blocks</param>
        /// <returns>List of block exact points</returns>
        private List<ConsolePoint> GetBlockExactPosition(int xColumn, int yRow, byte width, byte spaces)
        {
            List<ConsolePoint> blockConsolePoints = new List<ConsolePoint>(width);
            int row = 10 + (yRow*3);
            for (int i = 0; i < width; i++)
            {
                if (i == 0)
                {
                    blockConsolePoints.Add(new ConsolePoint((xColumn*(width + spaces)) + 5, row));
                    continue;
                }
                blockConsolePoints.Add(new ConsolePoint(blockConsolePoints[0].X + i, row));
            }
            return blockConsolePoints;
        }
    }
}