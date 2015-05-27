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
        private bool IsRunning { get; set; }
        private int Score { get; set; }
        private Board BoardObject { get; set; }
        private int[,] BlocksArray { get; set; }

        public Game()
        {
            RandomGenerator = new Random();
            BlocksArray = new int[5, 20]; // [rows, columns]
        }

        public void Start()
        {
            IsRunning = true;
            Score = 0;
            InitializeBoard();

            DrawScoreDivider();
            ResetBlocks();
            
            Thread inputThread = new Thread(ReadInput) {Name = "inputThread"};
            inputThread.Start();

            GameLoop();
        }

        private void GameLoop()
        {
            while (true)
            {
                PrintScore();
                MoveBoard();
                DrawBoard(); // draws board according to its position
                PrintScore(); // draws divider and score
                DrawBlocks(); // draws blocks
                Thread.Sleep(90);
            }
        }

        private void DrawBoard()
        {
            // Bug: when board is on the right end, screen shifts
            Console.SetCursorPosition(BoardObject.XPosition, Console.BufferHeight - 1);
            for (int i = 0; i < BoardObject.Width; i++)
                Console.Write(BoardObject.BoardChar);
        }

        private void InitializeBoard()
        {
            BoardObject = new Board(
                xPosition: Console.WindowWidth/2,
                yPosition: Console.BufferHeight - 1,
                width: 8,
                direction: RandomGenerator.Next(0, 1) == 0,
                exists: true,
                color: ConsoleColor.Yellow);
        }

        private void MoveBoard()
        {
            int leftEnd = 0;
            int rightEnd = Console.BufferWidth - BoardObject.Width - 2;

            Writer.ClearPosition(BoardObject.XPosition, BoardObject.YPosition, BoardObject.Width);

            if (BoardObject.XPosition <= leftEnd) //Changing board direction if were on the end
                BoardObject.Direction = true;
            else if (BoardObject.XPosition >= rightEnd)
                BoardObject.Direction = false;

            if (BoardObject.Direction)
                BoardObject.XPosition += 1;
            else
                BoardObject.XPosition -= 1;
        }

        private void ReadInput()
        {
            /* Moving the board */
            while (true)
            {
                lock (this)
                {

                    ConsoleKey pressedKey = Console.ReadKey().Key;
                    /* In order to detect numerous keys pressed at once */
                    while (Console.KeyAvailable)
                        Console.ReadKey(true);

                    if (pressedKey == ConsoleKey.LeftArrow)
                        BoardObject.Direction = false;

                    if (pressedKey == ConsoleKey.RightArrow)
                        BoardObject.Direction = true;
                }
            }
        }


        /// <summary>
        ///     Function resets all blocks. (Recreates them)
        /// </summary>
        private void ResetBlocks()
        {
            for (int i = 0; i < BlocksArray.GetLength(0); i++)
            {
                for (int j = 0; j < BlocksArray.GetLength(1); j++)
                {
                    BlocksArray[i, j] = 1;
                }
            }
        }

        private void DrawBlocks()
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorTop = 10;
            for (int j = 0; j < BlocksArray.GetLength(0); j++)
            {
                for (int i = 0; i < BlocksArray.GetLength(1); i++)
                {
                    if (BlocksArray[j, i] == 1)
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