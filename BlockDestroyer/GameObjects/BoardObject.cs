using System;

namespace BlockDestroyer.GameObjects
{
    internal class BoardObject : GameObj
    {
        /// <summary>
        ///     BoardObject construcor
        /// </summary>
        /// <param name="xColumn">xColumn position of the board - left corner (column).</param>
        /// <param name="yRows">yRow positon of the board (row).</param>
        /// <param name="width">Width of the board.</param>
        /// <param name="dir">Which dir is the board heading. FALSE = left; TRUE = right</param>
        /// <param name="objectChar">Which character is going to be used to draw the board.</param>
        /// <param name="exists">Does the object exist?</param>
        /// <param name="color">Color of the board.</param>
        public BoardObject(int xColumn, int yRows, byte width, bool dir, char objectChar,
            bool exists = true, ConsoleColor color = ConsoleColor.White)
            : base(xColumn, yRows, objectChar, exists)
        {
            Width = width;
            Dir = dir;
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
        ///     dir of the board
        ///     true = moving right
        ///     false = moving left
        /// </summary>
        public bool Dir { get; set; }
    }
}