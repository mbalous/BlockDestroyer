using System;
using System.Collections.Generic;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace BlockDestroyer.GameObjects
{
    internal class BoardObject : GameObject
    {
        /// <summary>
        ///     BoardObject construcor
        /// </summary>
        /// <param name="xColumn">xColumn position of the board - left corner (column).</param>
        /// <param name="yRow">yRow positon of the board (row).</param>
        /// <param name="width">Width of the board.</param>
        /// <param name="dir">Which dir is the board heading. FALSE = left; TRUE = right</param>
        /// <param name="objectChar">Which character is going to be used to draw the board.</param>
        /// <param name="exists">Does the object exist?</param>
        /// <param name="color">Color of the board.</param>
        public BoardObject(int xColumn, int yRow, byte width, bool dir, char objectChar,
            bool exists = true, ConsoleColor color = ConsoleColor.White)
            : base(xColumn, yRow, objectChar, exists)
        {
            Width = width;
            Dir = dir;
            AbsolutXyPoints = GetBoardExactPosition(xColumn, yRow, width);
        }

        /// <summary>
        ///     Width of the board
        /// </summary>
        public byte Width { get; set; }

        /// <summary>
        ///     dir of the board
        ///     true = moving right
        ///     false = moving left
        /// </summary>
        public bool Dir { get; set; }

        private List<ConsolePoint> GetBoardExactPosition(int xColumn, int yRow, byte width)
        {
            List<ConsolePoint> boardConsolePoints = new List<ConsolePoint>(width);
            for (int i = 0; i < width; i++)
            {
                boardConsolePoints.Add(new ConsolePoint(xColumn + i, yRow));
            }
            return boardConsolePoints;
        }

        public void SetBoardExactPosition()
        {
            AbsolutXyPoints = GetBoardExactPosition(XColumn, YRow, Width);
        }
    }
}