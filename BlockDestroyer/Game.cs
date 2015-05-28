using System;
using System.Collections.Generic;
using System.Threading;

// ReSharper disable RedundantArgumentName
// ReSharper disable RedundantArgumentNameForLiteralExpression

/*
 * Columns stands for Xposition axis
 * Rows stands for Yposition axis
 */

namespace BlockDestroyer
{
    internal class Game
    {
        /// <summary>
        ///     [Xposition-columns, Yposition-rows]
        /// </summary>
        private int[,] Blocks { get; set; }

        /// <summary>
        ///     Random number generator.
        /// </summary>
        private Random RandomGenerator { get; set; }

        /// <summary>
        ///     Property "is game running".
        /// </summary>
        private bool IsGameRunning { get; set; }

        /// <summary>
        ///     Score property.
        /// </summary>
        private int Score { get; set; }
        
        /// <summary>
        ///     Property holding blocks positions.
        /// </summary>
        private int[,] ConsoleBlocks { get; set; }

        /// <summary>
        ///     BallClass instance property.
        /// </summary>
        private BallClass Ball { get; set; }

        /// <summary>
        ///     BoardClass instance property.
        /// </summary>
        private BoardClass Board { get; set; }

        public Game()
        {
            ConsoleBlocks = new int[Console.BufferWidth,Console.BufferHeight];
            RandomGenerator = new Random();
        }

        public void Start()
        {
            IsGameRunning = true;
            Score = 0;

            Blocks = new int[20, 5];
            ResetBlocks();

            const byte boardWidth = 8;
            Board = new BoardClass(       
                xColumn: (Console.BufferWidth / 2) - boardWidth,
                yRows: Console.BufferHeight - 1,
                width: boardWidth,
                dir: RandomGenerator.Next(0, 2) == 0,
                exists: true,
                color: ConsoleColor.Yellow);

            Ball = new BallClass(
                xColumn: Console.WindowWidth / 2, 
                yRows: Console.WindowHeight / 2,
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
                Thread.Sleep(100);
            }
        }

        private void MoveBall()
        {
            Writer.ClearPosition(Ball.XColumn, Ball.YRow);
            
            if (Ball.Dir is UpLeft)
            {
                Ball.XColumn--;
                Ball.YRow--;
            }
            else if (Ball.Dir is UpRight)
            {
                Ball.XColumn++;
                Ball.YRow--;
            }
            else if (Ball.Dir is DownLeft)
            {
                Ball.XColumn--;
                Ball.YRow++;
            }
            else if (Ball.Dir is DownRight)
            {
                Ball.XColumn++;
                Ball.YRow++;
            }
        }

        private void DrawBall()
        {
            Console.SetCursorPosition(Ball.XColumn, Ball.YRow);
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
                Console.SetCursorPosition(Board.XColumn, Console.BufferHeight - 1);
                
                for (int i = 0; i < Board.Width; i++)
                    Console.Write(Board.BoardChar);

                Console.ResetColor();
            }
        }

        private void MoveBoard()
        {
            int leftEnd = 0;
            int rightEnd = Console.BufferWidth - Board.Width - 2;

            Writer.ClearPosition(Board.XColumn, Board.YRow, Board.Width);

            if (Board.XColumn <= leftEnd) //Changing board dir if were on the end
                Board.Dir = true;
            else if (Board.XColumn >= rightEnd)
                Board.Dir = false;

            if (Board.Dir)
                Board.XColumn += 1;
            else
                Board.XColumn -= 1;
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
                        Board.Dir = false;

                    if (pressedKey == ConsoleKey.RightArrow)
                        Board.Dir = true;
                }
            }
        }


        public List<Block> BlocksList { get; set; } = new List<Block>();

        /// <summary>
        ///     Function resets all blocks. (Recreates them)
        /// </summary>
        private void ResetBlocks()
        {
            /* Xposition - columns */
            for (int x = 0; x < 20; x++) 
            {
                /* Yposition - rows */
                for (int y = 0; y < 5; y++)
                {
                    BlocksList.Add(new Block(x, y));
                }
            }
        }

        private void DrawBlocks()
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorTop = 10;

            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                       
                }
            }

            foreach (Block block in BlocksList)
            {
                
            }


            /* Xposition and y are swapped because we need to print whole row first. */
            for (int y = 0; y < Blocks.GetLength(1); y++)
            {
                for (int x = 0; x < Blocks.GetLength(0); x++)
                {
                    
                    if (Blocks[x, y] == 1)
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