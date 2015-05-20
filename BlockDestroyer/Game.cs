using System;
using System.Threading;
#if DEBUG
using System.Diagnostics;
#endif

namespace BlockDestroyer
{
    internal class Game
    {
        private Random _randomGen;

        /// <summary>
        ///     Field holding game score
        /// </summary>
        private int Score { get; set; }

        private Board _boardStatus;

        private struct Board
        {
            public int Xpos { get; set; }
            public int Width { get; set; }
        }

        private struct Block
        {
            public int X { get; set; }
            public int Y { get; set; }
            public bool Exists { get; set; }
        }

        public Game()
        {
            _randomGen = new Random();
        }

        public void Start()
        {
            Score = 0;
            bool[,] blocksArray = new bool[5, 20]; // [rows, columns]
            ResetBlocksArray(blocksArray);

            _boardStatus = new Board
            {
                Width = 4,
                Xpos = Console.WindowWidth / 2,
            };
            
            Thread inputThread = new Thread(ReadInput);
            inputThread.Start();

            while (true)
            {
                DrawBlocks(blocksArray);
                DrawGameInfo();
                Thread.Sleep(2000);
                Console.Clear();
#if DEBUG
                var tick = 1;
                Debug.WriteLine("Tick no: {0}", tick);
#endif
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
                    
                }
                if (userInput.Key == ConsoleKey.RightArrow)
                {
                    
                }
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
    }
}