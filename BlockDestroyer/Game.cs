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
            _random = new Random();
            _blocksArray = new bool[5, 20]; // [rows, columns]
            _blocks = new Block[20 , 5];
        }

        private readonly Random _random;
        private bool _isRunning;
        private int _score;
        private Board _board;
        private bool[,] _blocksArray;
        private Block[,] _blocks;

        public void Start()
        {
            _isRunning = true;
            _score = 0;
            InitializeBlocks();
            ResetBlocks(_blocksArray);
            CreateBoard();

            Thread inputThread = new Thread(ReadInput) { Name = "inputThread" };
            inputThread.Start();

            while (true)
            {
                Console.Clear();
                MoveBoard();
                Draw();
                Thread.Sleep(90);
            }
        }

        private void InitializeBlocks()
        {
            int[] coords = new int[2];

            for (int x = 0; x < _blocks.GetLength(0); x++)
            {
                coords[0] = x + 1;
                for (int y = 0; y < _blocks.GetLength(1); y++)
                {
                    coords[1] = y + 1;
                    _blocks[x, y] = new Block();
                }   
            }



            for (int y = 1; y <= _blocks.GetLength(0); y++)
            {
                for (int x = 1; x <= _blocks.GetLength(1) * 5; x += 5)
                {
                    _blocks[y - 1, x - 1] = new Block();
                    _blocks[y - 1, x - 1].XPos = x;
                    _blocks[y - 1, x - 1].YPos = y;
                }
            }
        }

        private void Draw()
        {
            DrawGameInfo(); // draws divider and score
            DrawBlocks(); // draws blocks
            DrawBoard(); // draws board according to its position
        }

        private void DrawBoard()
        {
            // Bug: when board is on the right end, screen shifts
            Console.SetCursorPosition(_board.XPos, Console.BufferHeight - 1);
            for (int i = 0; i < _board.Width; i++)
                Console.Write('#');
        }

        private void CreateBoard()
        {
            // Creating the board, xPosition is set to center screen, direction is randomized
            _board = new Board(
                xPosition: (sbyte)(Console.WindowWidth / 2),
                width: 8,
                direction: _random.Next(0, 1) == 0);
        }

        private void MoveBoard()
        {
            int leftEnd = 0;
            int rightEnd = Console.BufferWidth - _board.Width - 2;

            if (_board.XPos <= leftEnd) //Changing board direction if were on the end
                _board.Direction = true;
            else if (_board.XPos >= rightEnd)
                _board.Direction = false;

            if (_board.Direction)
                _board.XPos += 1;
            else
                _board.XPos -= 1;
        }

        private void ReadInput()
        {
            //moving the dwarf
            while (true)
            {
                ConsoleKeyInfo userInput = Console.ReadKey();
                // In order to avoid the moving bug (If numerous keys are pressed, the program will execute each one)
                while (Console.KeyAvailable)
                    Console.ReadKey(true);

                if (userInput.Key == ConsoleKey.LeftArrow)
                    _board.Direction = false;

                if (userInput.Key == ConsoleKey.RightArrow)
                    _board.Direction = true;
            }
        }

        /// <summary>
        ///     Function resets all blocks. (Recreates them)
        /// </summary>
        /// <param name="blocksArray"></param>
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
            Console.SetCursorPosition(0, 0);
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

            Writer.PrintAtPosition(0, 0, string.Format("Score: {0}", _score), ConsoleColor.DarkRed);
        }
    }
}