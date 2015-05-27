using System;

namespace BlockDestroyer
{
    internal class Board : GameObject
    {
        public Board(int xPosition, int yPosition, byte width, bool direction, bool exists = true, ConsoleColor color = ConsoleColor.White)
            : base(xPosition, yPosition, exists)
        {
            Width = width;
            Direction = direction;
        }

        /// <summary>
        ///     Width of the board
        /// </summary>
        public byte Width { get; private set; }

        /// <summary>
        ///     Direction of the board
        ///     true = moving right
        ///     false = moving left
        /// </summary>
        public bool Direction { get; set; }
    }
}