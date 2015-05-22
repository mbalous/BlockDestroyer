using System;

namespace BlockDestroyer
{
    internal class Board : GameObject
    {
        public Board(int xPosition, int yPosition, byte width, bool direction, ConsoleColor color = ConsoleColor.White) : base(xPosition, yPosition, color)
        {
            Width = width;
            Direction = direction;
        }

        /// <summary>
        ///     Width of the board
        /// </summary>
        byte Width { get; set; }

        /// <summary>
        ///     Direction of the board
        ///     true = moving right
        ///     false = moving left
        /// </summary>
        bool Direction { get; set; }
    }
}