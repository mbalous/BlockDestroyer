using System;

namespace BlockDestroyer.GameObjects.Ball
{
    internal class BallObject : GameObj
    {
        public Direction Dir { get; set; }

        public BallObject(int xColumn, int yRow, bool exists, Direction dir, char objectChar,
            ConsoleColor color = ConsoleColor.White)
            : base(xColumn, yRow, objectChar, exists, color)
        {
            Dir = dir;
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