using System;

namespace BlockDestroyer
{
    class BallClass : GameObject
    {
        public char BallChar { get; set; }
        public bool GoingUp { get; set; }
        public bool GoingRight { get; set; }
        public Direction Dir { get; set; }

        public BallClass(int xPosition, int yPosition, bool goingUp, bool goingRight, char ballChar, bool exists = true, ConsoleColor color = ConsoleColor.White)
            : base(xPosition, yPosition, exists, color)
        {
            GoingUp = goingUp;
            GoingRight = goingRight;
            BallChar = ballChar;
        }

        public BallClass(int xPosition, int yPosition, bool exists, Direction dir, char ballChar, ConsoleColor color = ConsoleColor.White)
            : base(xPosition, yPosition, exists, color)
        {
            this.Dir = dir;
            this.BallChar = ballChar;
        }
    }

    abstract class Direction
    {

    }

    class UpRight : Direction
    {

    }

    class UpLeft : Direction
    {

    }

    class DownRight : Direction
    {

    }

    class DownLeft : Direction
    {

    }
}