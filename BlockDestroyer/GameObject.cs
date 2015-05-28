using System;

namespace BlockDestroyer
{
    /// <summary>
    ///     Class representing generic game object.
    /// </summary>
    abstract class GameObject
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public bool Exists { get; set; }
        public ConsoleColor Color { get; set; }

        /// <summary>
        ///     Generic constuctor.
        /// </summary>
        protected GameObject(int xPosition, int yPosition, bool exists, ConsoleColor color = ConsoleColor.White)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            Exists = exists;
            Color = color;
        }
    }
}