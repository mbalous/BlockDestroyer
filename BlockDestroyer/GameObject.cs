using System;

namespace BlockDestroyer
{
    /// <summary>
    ///     Class representing generic game object.
    /// </summary>
    abstract class GameObject
    {
        public int XColumn { get; set; }
        public int YRow { get; set; }
        public bool Exists { get; set; }
        public ConsoleColor Color { get; set; }
        
        /// <summary>
        ///     Generic constuctor.
        /// </summary>
        protected GameObject(int xColumn, int yRow, bool exists, ConsoleColor color = ConsoleColor.White)
        {
            XColumn = xColumn;
            YRow = yRow;
            Exists = exists;
            Color = color;
        }
    }
}