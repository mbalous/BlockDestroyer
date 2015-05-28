using System;

namespace BlockDestroyer
{
    internal class BoardClass : GameObject
    {
        /// <summary>
        ///     BoardClass construcor
        /// </summary>
        /// <param name="xPosition">X position of the board - left corner (column).</param>
        /// <param name="yPosition">Y positon of the board (row).</param>
        /// <param name="width">Width of the board.</param>
        /// <param name="direction">Which direction is the board heading. FALSE = left; TRUE = right</param>
        /// <param name="boardChar">Which character is going to be used to draw the board.</param>
        /// <param name="exists">Does the object exist?</param>
        /// <param name="color">Color of the board.</param>
        public BoardClass(int xPosition,
            int yPosition,
            byte width,
            bool direction,
            char boardChar = '-',
            bool exists = true,
            ConsoleColor color = ConsoleColor.White)
            : base(xPosition, yPosition, exists)
        {
            Width = width;
            Direction = direction;
            BoardChar = boardChar;
        }

        /// <summary>
        ///     Which character is going to be used to draw the board.
        /// </summary>
        public char BoardChar { get; set; }

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