using System;

namespace BlockDestroyer
{
    internal class BallClass : GameObject
    {
        public char BallChar { get; set; }
        public Direction Dir { get; set; }

        public BallClass(int xColumn, int yRows, bool exists, Direction dir, char ballChar,
            ConsoleColor color = ConsoleColor.White)
            : base(xColumn, yRows, exists, color)
        {
            Dir = dir;
            BallChar = ballChar;
        }
    }

    internal abstract class Direction
    {
    }

    internal class UpRight : Direction
    {
    }

    internal class UpLeft : Direction
    {
    }

    internal class DownRight : Direction
    {
    }

    internal class DownLeft : Direction
    {
    }
}