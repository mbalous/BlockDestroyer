using System;

namespace BlockDestroyer.GameObjects
{
    /// <summary>
    ///     Class representing generic game object.
    /// </summary>
    abstract class GameObj
    {
        public int XColumn { get; set; }
        public int YRow { get; set; }
        public char ObjectChar { get; set; }
        public bool Exists { get; set; }
        public ConsoleColor Color { get; set; }

        /// <summary>gndanjanm
        ///     Generic constuctor.
        /// </summary>
        protected GameObj(int xColumn, int yRow, char objectChar, bool exists, ConsoleColor color = ConsoleColor.White)
        {
            XColumn = xColumn;
            YRow = yRow;
            ObjectChar = objectChar;
            Exists = exists;
            Color = color;
        }
    }
}