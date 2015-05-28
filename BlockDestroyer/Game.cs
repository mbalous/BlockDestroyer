using System;
using System.Threading;

// ReSharper disable RedundantArgumentName
// ReSharper disable RedundantArgumentNameForLiteralExpression

/*
 * Columns stands for X axis
 * Rows stands for Y axis
 */

namespace BlockDestroyer
{
    internal class Game
    {
        private Random RandomGenerator { get; set; }
        private bool IsGameRunning { get; set; }
        private int Score { get; set; }

        private int[,] Blocks { get; set; }

        private BallClass Ball { get; set; }
        private BoardClass Board { get; set; }

        public Game()
        {
            RandomGenerator = new Random();
        }

        public void Start()
        {
            IsGameRunning = true;
            Score = 0;

            /* [rows, columns] */
            Blocks = new int[5, 20];
            ResetBlocks();

            const byte boardWidth = 8;
            Board = new BoardClass(       
                xPosition: (Console.BufferWidth / 2) - boardWidth,
                yPosition: Console.BufferHeight - 1,
                width: boardWidth,
                direction: RandomGenerator.Next(0, 2) == 0,
                exists: true,
                color: ConsoleColor.Yellow);

            Ball = new BallClass(
                xPosition: Console.WindowWidth / 2, 
                yPosition: Console.WindowHeight / 2,
                dir: (RandomGenerator.Next(0, 2) == 0) ? (Direction)new UpLeft() : new UpRight(), 
                ballChar: 'O',
                exists: true, 
                color: ConsoleColor.Red);

            DrawScoreDivider();
            
            Thread inputThread = new Thread(ReadInput) { Name = "inputThread" };
            inputThread.Start();
            GameLoop();
        }

        private void GameLoop()
        {
            while (IsGameRunning)
            {
                PrintScore();
                PrintScore();
                DrawBlocks();
                MoveBoard();
                DrawBoard();
                
                MoveBall();
                DrawBall();
                Thread.Sleep(10);
            }
        }

        private void MoveBall()
        {
            Writer.ClearPosition(Ball.XPosition, Ball.YPosition);
            if (Ball.Dir is UpLeft)
            {
                Ball.XPosition--;
                Ball.YPosition--;
            }
            else if (Ball.Dir is UpRight)
            {
                Ball.XPosition++;
                Ball.YPosition--;
            }
            else if (Ball.Dir is DownLeft)
            {
                Ball.XPosition--;
                Ball.YPosition++;
            }
            else if (Ball.Dir is DownRight)
            {
                Ball.XPosition++;
                Ball.YPosition++;
            }
        }

        private void DrawBall()
        {
            Console.SetCursorPosition(Ball.XPosition, Ball.YPosition);
            Console.ForegroundColor = Ball.Color;
            Console.Write(Ball.BallChar);
            Console.ResetColor();
        }

        private void DrawBoard()
        {
            // Bug: when board is on the right end, screen shifts
            if (Board.Exists)
            {
                Console.ForegroundColor = Board.Color;
                Console.SetCursorPosition(Board.XPosition, Console.BufferHeight - 1);
                
                for (int i = 0; i < Board.Width; i++)
                    Console.Write(Board.BoardChar);

                Console.ResetColor();
            }
        }

        private void MoveBoard()
        {
            int leftEnd = 0;
            int rightEnd = Console.BufferWidth - Board.Width - 2;

            Writer.ClearPosition(Board.XPosition, Board.YPosition, Board.Width);

            if (Board.XPosition <= leftEnd) //Changing board dir if were on the end
                Board.Direction = true;
            else if (Board.XPosition >= rightEnd)
                Board.Direction = false;

            if (Board.Direction)
                Board.XPosition += 1;
            else
                Board.XPosition -= 1;
        }

        private void ReadInput()
        {
            /* Moving the board */
            while (IsGameRunning)
            {
                lock (Board)
                {
                    ConsoleKey pressedKey = Console.ReadKey(true).Key;
                    /* In order to detect numerous keys pressed at once */
                    /*
                    while (Console.KeyAvailable)
                        Console.ReadKey(true);
                    */

                    if (pressedKey == ConsoleKey.LeftArrow)
                        Board.Direction = false;

                    if (pressedKey == ConsoleKey.RightArrow)
                        Board.Direction = true;
                }
            }
        }

        /// <summary>
        ///     Function resets all blocks. (Recreates them)
        /// </summary>
        private void ResetBlocks()
        {
            for (int i = 0; i < Blocks.GetLength(0); i++)
            {
                for (int j = 0; j < Blocks.GetLength(1); j++)
                {
                    Blocks[i, j] = 1;
                }
            }
        }

        private void DrawBlocks()
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorTop = 10;
            for (int j = 0; j < Blocks.GetLength(0); j++)
            {
                for (int i = 0; i < Blocks.GetLength(1); i++)
                {
                    if (Blocks[j, i] == 1)
                        Console.Write("████ ");
                    else
                        Console.Write("     ");
                }
                Console.CursorTop += 1;
            }
        }


        /// <summary>
        ///     Function prints score above the score divider.
        /// </summary>
        private void PrintScore()
        {
            /* 
             * Draw score and lives
             * TODO: Implement lives
             */
            Writer.PrintAtPosition(0, 0, string.Format("Score: {0}", Score), ConsoleColor.DarkRed);
        }


        /// <summary>
        ///     Function prints score divider.
        /// </summary>
        private void DrawScoreDivider()
        {
            /* Score divider */
            for (int i = 0; i < Console.WindowWidth; i++)
                Writer.PrintAtPosition(i, 5, '-');
        }
    }
}