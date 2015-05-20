using System;
using System.Threading;
#if DEBUG
using System.Diagnostics;

#endif

namespace BlockDestroyer
{
    internal class Game
    {
        public Game()
        {
            RandomGen = new Random();
            _blocksArray = new bool[5, 20]; // [rows, columns]
        }

        private Random RandomGen { get; set; }
        private bool IsRunning { get; set; }

        /// <summary>
        ///     Field holding game score
        /// </summary>
        private int Score { get; set; }

        private Board BoardStatus { get; set; }
        //private bool BoardDirection { get; set; }

        private readonly bool[,] _blocksArray;

        public void Start()
        {
            Score = 0;
            ResetBlocksArray(_blocksArray);

            BoardStatus = new Board((sbyte) (Console.WindowWidth/2), 4, RandomGen.Next(0,1) == 0);

            Thread inputThread = new Thread(ReadInput);
            inputThread.Start();

            Thread boardMoverThread = new Thread(BoardMover);
            boardMoverThread.Start();
#if DEBUG
            int tick = 0;
#endif
            while (true)
            {
                Console.Clear();
                DrawBlocks(_blocksArray);
                DrawGameInfo();
                Thread.Sleep(200);
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
                    BoardStatus.XPos++;
                else
                    BoardStatus.XPos--;
            }
        }

        private void ReadInput()
        {
            //moving the dwarf
            while (true)
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
                Debug.WriteLine("Pressed key: {0}", userInput.Key);
#endif
            }
        }

        private void ResetBlocksArray(bool[,] blocksArray)
        {
            for (int i = 0; i < blocksArray.GetLength(0); i++)
            {
                for (int j = 0; j < blocksArray.GetLength(1); j++)
                {
                    blocksArray[i, j] = true;
                }
            }
        }

        private void DrawBlocks(bool[,] blocksArray)
        {
            Console.CursorTop = 10;
            for (int j = 0; j < blocksArray.GetLength(0); j++)
            {
                for (int i = 0; i < blocksArray.GetLength(1); i++)
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
            for (int i = 0; i < Console.WindowWidth; i++) // Score Divider
                Writer.PrintAtPosition(i, 5, '-');

            Writer.PrintAtPosition(0, 0, string.Format("Score: {0}", Score), ConsoleColor.DarkRed);
        }

        private struct Block
        {
            public int X { get; set; }
            public int Y { get; set; }
            public bool Exists { get; set; }
        }
    }
}