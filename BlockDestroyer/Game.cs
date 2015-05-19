using System;
using System.Collections.Generic;
using System.Threading;

namespace BlockDestroyer
{
    class Game
    {
        private Random _randomGen;
        private int Score { get; set; }
        const string BlockString = "████";

        private void SetBuffer()
        {
            Console.BufferHeight = Console.WindowHeight = 40;
            Console.BufferWidth = Console.WindowWidth = 100;
        }

        private struct Board
        {
            public int X { get; set; }
            public int Y { get; set; }
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
            SetBuffer();
            _randomGen = new Random();
        }

        public void Start()
        {
            bool[,] blocksArray = new bool[5, 20]; // [rows, columns]
            FillBlocksArray(blocksArray);
            while (true)
            {
                DrawBlocks(blocksArray);
                DrawGameInfo();
                Thread.Sleep(200);
                Console.Clear();
            }
        }

        private void FillBlocksArray(bool[,] blocksArray)
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
                        Console.Write(BlockString + " ");
                    else
                        Console.CursorLeft += 5;
                }
                Console.CursorTop += 1;
            }
        }


        private void DrawGameInfo()
        {
            // Draw Score and lives
            for (int i = 0; i < Console.WindowWidth; i++) // Score Divider
                Writer.WriteCharAtPosition(i, 5, '-');
        }
    }
}