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
        private int Score { get; set; }
        private Board Board { get; set; }
        private bool[,] _blocksArray;

        public void Start()
        {
            IsRunning = true;
            Score = 0;

            ResetBlocks(_blocksArray);
            CreateBoard();

            Thread inputThread = new Thread(ReadInput) { Name = "inputThread" };
            inputThread.Start();

            /*
            Thread boardMoveThread = new Thread(MoveBoard) {Name = "boardMoveThread"};
            boardMoveThread.Start();
            */
            while (true)
            {
                Console.Clear();
                Draw();
                MoveBoard();
                Thread.Sleep(40);
            }
        }

        private void Draw()
        {
            DrawBlocks();
            DrawBoard();
            DrawGameInfo();
        }

        private void DrawBoard()
        {
            Console.SetCursorPosition(Board.XPos, Console.BufferHeight - 1);
            for (int i = 0; i < Board.Width; i++)
            {
                Console.Write('-');
            }
        }

        private void CreateBoard()
        {
            Board = new Board(xPos: (sbyte)(Console.WindowWidth / 2), width: 8, direction: RandomGen.Next(0, 1) == 0);
        }

        private void MoveBoard()
        {
            int leftEnd = 0;
            int rightEnd = Console.BufferWidth - Board.Width;

            //while (IsRunning)
            //{
            //Changing board direction if were on the end
            if (Board.XPos <= leftEnd)
                Board.Direction = true;
            else if (Board.XPos >= rightEnd)
                Board.Direction = false;

            if (Board.Direction)
                Board.XPos += 2;
            else
                Board.XPos -= 2;
            //}
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
                    Board.Direction = false;
                }
                if (userInput.Key == ConsoleKey.RightArrow)
                {
                    Board.Direction = true;
                }
            }
        }

        private void ResetBlocks(bool[,] blocksArray)
        {
            for (int i = 0; i < blocksArray.GetLength(0); i++)
            {
                for (int j = 0; j < blocksArray.GetLength(1); j++)
                {
                    blocksArray[i, j] = true;
                }
            }
        }

        private void DrawBlocks()
        {
            Console.CursorTop = 10;
            for (int j = 0; j < _blocksArray.GetLength(0); j++)
            {
                for (int i = 0; i < _blocksArray.GetLength(1); i++)
                {
                    if (_blocksArray[j, i])
                        Console.Write("████ ");
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

            Writer.PrintAtPosition(0, 0, $"Score: {Score}", ConsoleColor.DarkRed);
        }
    }
}