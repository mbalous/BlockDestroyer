using System;
using System.Collections.Generic;
using System.Threading;

// ReSharper disable RedundantArgumentName
// ReSharper disable RedundantArgumentNameForLiteralExpression

/*
 * Columns stands for xColumn axis
 * Rows stands for yRow axis
 */

namespace BlockDestroyer
{
    internal class Game
    {
        /// <summary>
        ///     [xColumn-columns, yRow-rows]
        /// </summary>
        private int[,] Blocks { get; set; }

        private Random RandomGenerator { get; set; }
        private bool IsGameRunning { get; set; }
        private int Score { get; set; }
        private Ball GameBall { get; set; }
        private Board GameBoard { get; set; }
        private List<Block> BlocksList { get; set; }

        public Game()
        {
            RandomGenerator = new Random();
            BlocksList = new List<Block>();
        }

        /// <summary>
        ///     Start the game.
        /// </summary>
        /// <param name="gameSpeed">Aproximate duration of one tick in miliseconds.</param>
        public void Start(int gameSpeed)
        {
            IsGameRunning = true;
            Score = 0;

            Blocks = new int[20, 5];
            ResetBlocks();

            const byte boardWidth = 8;
            GameBoard = new Board(
                xColumn: (Console.BufferWidth / 2) - boardWidth,
                yRows: Console.BufferHeight - 1,
                width: boardWidth,
                dir: RandomGenerator.Next(0, 2) == 0,
                objectChar: '-',
                exists: true,
                color: ConsoleColor.Yellow);

            GameBall = new Ball(
                xColumn: Console.WindowWidth / 2,
                yRows: Console.WindowHeight / 2,
                dir: (RandomGenerator.Next(0, 2) == 0) ? (Direction)new UpLeft() : new UpRight(),
                objectChar: 'O',
                exists: true,
                color: ConsoleColor.Red);

            DrawScoreDivider();

            Thread inputThread = new Thread(ReadInput) { Name = "inputThread" };
            inputThread.Start();
            GameLoop(gameSpeed);
        }

        private void GameLoop(int gameSpeed)
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

                Thread.Sleep(gameSpeed);
            }
        }

        private void MoveBall()
        {
            ConsolePoint actualBallPosition = new ConsolePoint(GameBall.XColumn, GameBall.YRow);
            ConsolePoint nextBallPosition = null;

            Writer.ClearPosition(actualBallPosition.x, actualBallPosition.y);
            string ballDirection = GameBall.Dir.GetType().ToString();

            switch (ballDirection)
            {
                case "UpLeft":
                    nextBallPosition = new ConsolePoint(GameBall.XColumn - 1, GameBall.YRow - 1);
                    break;
                case "UpRight":
                    nextBallPosition = new ConsolePoint(GameBall.XColumn + 1, GameBall.YRow - 1);
                    break;
                case "DownLeft":
                    nextBallPosition = new ConsolePoint(GameBall.XColumn - 1, GameBall.YRow + 1);
                    break;
                case "DownRight":
                    nextBallPosition = new ConsolePoint(GameBall.XColumn + 1, GameBall.YRow + 1);
                    break;
            }
            Collison collison = CollisonCheck(
                checkedPosition: nextBallPosition,
                previousPosition: actualBallPosition);


            if (//TODO: IMPLEMENT COLLISION)
            {
                if (GameBall.Dir is UpLeft)
                {
                    GameBall.XColumn--;
                    GameBall.YRow--;
                }
                else if (GameBall.Dir is UpRight)
                {
                    GameBall.XColumn++;
                    GameBall.YRow--;
                }
                else if (GameBall.Dir is DownLeft)
                {
                    GameBall.XColumn--;
                    GameBall.YRow++;
                }
                else if (GameBall.Dir is DownRight)
                {
                    GameBall.XColumn++;
                    GameBall.YRow++;
                }
            }
        }

        private Collison CollisonCheck(ConsolePoint checkedPosition, ConsolePoint previousPosition)
        {
            Collison collision = new Collison();



        }

        private struct Collison
        {
            public object left;
            public object right;
            public object top;
            public object bottom;
        }

        private void DrawBall()
        {
            Console.SetCursorPosition(GameBall.XColumn, GameBall.YRow);
            Console.ForegroundColor = GameBall.Color;
            Console.Write(GameBall.ObjectChar);
            Console.ResetColor();
        }

        private void DrawBoard()
        {
            // Bug: when board is on the right end, screen shifts
            if (GameBoard.Exists)
            {
                Console.ForegroundColor = GameBoard.Color;
                Console.SetCursorPosition(GameBoard.XColumn, Console.BufferHeight - 1);

                for (int i = 0; i < GameBoard.Width; i++)
                    Console.Write(GameBoard.BoardChar);

                Console.ResetColor();
            }
        }

        private void MoveBoard()
        {
            int leftEnd = 0;
            int rightEnd = Console.BufferWidth - GameBoard.Width - 2;

            Writer.ClearPosition(GameBoard.XColumn, GameBoard.YRow, GameBoard.Width);

            if (GameBoard.XColumn <= leftEnd) //Changing board dir if were on the end
                GameBoard.Dir = true;
            else if (GameBoard.XColumn >= rightEnd)
                GameBoard.Dir = false;

            if (GameBoard.Dir)
                GameBoard.XColumn += 1;
            else
                GameBoard.XColumn -= 1;
        }

        private void ReadInput()
        {
            /* Moving the board */
            while (IsGameRunning)
            {
                lock (GameBoard)
                {
                    ConsoleKey pressedKey = Console.ReadKey(true).Key;

                    /* In order to detect numerous keys pressed at once */
                    if (pressedKey == ConsoleKey.LeftArrow)
                        GameBoard.Dir = false;

                    if (pressedKey == ConsoleKey.RightArrow)
                        GameBoard.Dir = true;
                }
            }
        }


        /// <summary>
        ///     Function resets all blocks. (Recreates them)
        /// </summary>
        private void ResetBlocks()
        {
            const int blockRows = 5;
            const int blockColumns = 20;

            /* xColumn - columns */

            for (int col = 0; col < blockColumns; col++)
            {
                /* yRow - rows */
                for (int row = 0; row < blockRows; row++)
                {
                    BlocksList.Add(
                        new Block(
                            xColumn: col,
                            yRow: row,
                            exists: true,
                            width: 4,
                            objectChar: '█',
                            isBonus: false)
                            );
                }
            }
        }

        private void DrawBlocks()
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorTop = 10;

            foreach (Block block in BlocksList)
            {
                var blockFirstPosition = block.AbsolutXyPoints[0];
                Console.SetCursorPosition(blockFirstPosition.x, blockFirstPosition.y);
                for (int i = 0; i < block.Width; i++)
                {
                    Console.Write(block.ObjectChar);
                }
                Console.Write(' ');
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