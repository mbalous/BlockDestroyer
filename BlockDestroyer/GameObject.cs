using System;

namespace BlockDestroyer
{
    abstract class GameObject
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        bool Exists { get; set; }
        ConsoleColor Color { get; set; }

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