using System;

namespace BlockDestroyer
{
    abstract class GameObject
    {
        int XPosition { get; set; }
        int YPosition { get; set; }
        bool Exists { get; set; }
        ConsoleColor Color { get; set; }

        protected GameObject(int xPosition, int yPosition, ConsoleColor color = ConsoleColor.White)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            Color = color;
        }
    }
}