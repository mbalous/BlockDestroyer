using System;
using System.Threading;
#if DEBUG
using System.Diagnostics;

#endif

namespace BlockDestroyer
{
    internal class Game
    {
        public Random RandomGen { get; }
        private bool IsRunning { get; set; }

        /// <summary>
        ///     Field holding game score
        /// </summary>
        private int Score { get; set; }

        private Board BoardStatus { get; set; }
        private bool BoardDirection { get; set; }


        private struct Block
        {
            public int X { get; set; }
            public int Y { get; set; }
            public bool Exists { get; set; }
        }

        public Game()
        {
            RandomGen = new Random();
        }

        public void Start()
        {
            Score = 0;
            var blocksArray = new bool[5, 20]; // [rows, columns]
            ResetBlocksArray(blocksArray);

            BoardStatus = new Board((sbyte) (Console.WindowWidth/2), 4);

            var inputThread = new Thread(ReadInput);
            inputThread.Start();

            var boardMoverThread = new Thread(BoardMover);
            boardMoverThread.Start();
#if DEBUG
            var tick = 0;
#endif
            while (true)
            {
                DrawBlocks(blocksArray);
                DrawGameInfo();
                Thread.Sleep(2000);
                Console.Clear();
#if DEBUG
                tick++;
                Debug.WriteLine("Tick no: {0}", tick);
#endif
            }
        }

        private void BoardMover()
        {
            while (IsRunning)
            {
                if (BoardDirection)
                    BoardStatus.Xpos++;
                else
                    BoardStatus.Xpos--;
            }
        }

        private void ReadInput()
        {
            //moving the dwarf
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo userInput = Console.ReadKey();
                // In order to avoid the moving bug (If numerous keys are pressed, the program will execute each one)
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
                if (userInput.Key == ConsoleKey.LeftArrow)
                {
                    BoardDirection = false;
                }
                if (userInput.Key == ConsoleKey.RightArrow)
                {
                    BoardDirection = true;
                }
#if DEBUG
                Debug.WriteLine("Pressed key: {0}", userInput.KeyChar);
#endif
            }
        }

        private void ResetBlocksArray(bool[,] blocksArray)
        {
            for (var i = 0; i < blocksArray.GetLength(0); i++)
            {
                for (var j = 0; j < blocksArray.GetLength(1); j++)
                {
                    blocksArray[i, j] = true;
                }
            }
        }


        private void DrawBlocks(bool[,] blocksArray)
        {
            Console.CursorTop = 10;
            for (var j = 0; j < blocksArray.GetLength(0); j++)
            {
                for (var i = 0; i < blocksArray.GetLength(1); i++)
                {
                    if (blocksArray[j, i])
                        Console.Write("████" + " ");
                    else
                        Console.CursorLeft += 5;
                }
                Console.CursorTop += 1;
            }
        }

        /// <summary>
        ///     Function used for displaying score, etc...
        /// </summary>
        private void DrawGameInfo()
        {
            // Draw Score and lives
            for (var i = 0; i < Console.WindowWidth; i++) // Score Divider
                Writer.PrintAtPosition(i, 5, '-');

            Writer.PrintAtPosition(0, 0, string.Format("Score: {0}", Score), ConsoleColor.DarkRed);
        }
    }
}