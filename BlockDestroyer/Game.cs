using System;
using System.Collections.Generic;
using System.Threading;
using BlockDestroyer.GameObjects;
using BlockDestroyer.GameObjects.Ball;

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
        private readonly Random _randomGenerator;
        private bool IsGameRunning { get; set; }
        private int Score { get; set; }

        private BallObject Ball { get; set; }
        private BoardObject Board { get; set; }

        private int[,] Corners { get; set; }
        readonly int _bufferWidth;
        readonly int _bufferHeight;

        private static List<BlockObject> BlockList { get; set; }
        

        public Game()
        {
            CreateCorners();

            _bufferWidth = Console.BufferWidth;
            _bufferHeight = Console.BufferHeight;

            _randomGenerator = new Random();
            BlockList = new List<BlockObject>();

            Corners = null;
        }


        /// <summary>
        ///     Start the game.
        /// </summary>
        /// <param name="gameSpeed">Aproximate duration of one tick in miliseconds.</param>
        public void Start(int gameSpeed)
        {
            IsGameRunning = true;
            Score = 0;

            /* GAME PARAMETERS */
            const int blockColumns = 13;
            const int blockRows = 5;
            const byte blockWidth = 7;
            const byte boardWidth = 8;
            const byte spaceBetweenBoards = 2;

            InitializeBlocksList(blockColumns, blockRows, blockWidth, spaceBetweenBoards);


            Board = new BoardObject(
                xColumn: (_bufferWidth / 2) - boardWidth,
                yRows: _bufferHeight - 1,
                width: boardWidth,
                dir: _randomGenerator.Next(0, 2) == 0,
                objectChar: '-',
                exists: true,
                color: ConsoleColor.Yellow);

            Ball = new BallObject(
                xColumn: _bufferWidth / 2,
                yRows: _bufferHeight / 2,
                dir: (_randomGenerator.Next(0, 2) == 0) ? (Direction)new UpLeft() : new UpRight(),
                objectChar: 'O',
                exists: true,
                color: ConsoleColor.Red);

            DrawScoreDivider();

            Thread inputThread = new Thread(ReadInput) { Name = "inputThread" };
            inputThread.Start();
            GameLoop(gameSpeed);
        }

        private void CreateCorners()
        {
            
            //// TODO: Finish corner creating
            //for (int i = 0; i < _corners.GetLength(0); i++)
            //{
            //    /* Top corner id - 1 */
            //    _corners[i, 11] = 1;
            //    /* Bottom corner - 2 */
            //    _corners[i, _bufferHeight] = 2;
            //}

            //for (int i = 0; i < _corners.GetLength(1); i++)
            //{
            //    /* Left corner - 3 */
            //    _corners[0, i] = 3;
            //    /* Right corner - 4 */
            //    _corners[_bufferWidth, i] = 4;
            //}
        }

        private void GameLoop(int gameSpeed)
        {
            while (IsGameRunning)
            {
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
            ConsolePoint actualBallPosition = new ConsolePoint(Ball.XColumn, Ball.YRow);
            ConsolePoint nextBallPosition = null;

            Writer.ClearPosition(actualBallPosition.x, actualBallPosition.y);
            string ballDirection = Ball.Dir.GetType().ToString();

            switch (ballDirection)
            {
                case "UpLeft":
                    nextBallPosition = new ConsolePoint(Ball.XColumn - 1, Ball.YRow - 1);
                    break;
                case "UpRight":
                    nextBallPosition = new ConsolePoint(Ball.XColumn + 1, Ball.YRow - 1);
                    break;
                case "DownLeft":
                    nextBallPosition = new ConsolePoint(Ball.XColumn - 1, Ball.YRow + 1);
                    break;
                case "DownRight":
                    nextBallPosition = new ConsolePoint(Ball.XColumn + 1, Ball.YRow + 1);
                    break;
            }

            if (true)
            {
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
        }

        private void DrawBall()
        {
            Writer.PrintAtPosition(Ball.XColumn, Ball.YRow, Ball.ObjectChar, Ball.Color);
        }

        private void DrawBoard()
        {
            // Bug: when board is on the right end, screen shifts
            if (Board.Exists)
            {
                string board = null;
                for (int i = 0; i < Board.Width; i++)
                    board += Board.ObjectChar;

                Writer.PrintAtPosition(Board.XColumn, _bufferHeight - 1, board, Board.Color);
            }
        }

        private void MoveBoard()
        {
            int leftEnd = 0;
            int rightEnd = _bufferWidth - Board.Width - 2;

            Writer.ClearPosition(Board.XColumn, Board.YRow, Board.Width);

            /* Changing board dir if were on the end */
            if (Board.XColumn <= leftEnd)
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
                    if (pressedKey == ConsoleKey.LeftArrow)
                        Board.Dir = false;

                    if (pressedKey == ConsoleKey.RightArrow)
                        Board.Dir = true;
                }
            }
        }


        /// <summary>
        ///     Function inicializes GameData.
        /// </summary>
        private void InitializeBlocksList(short blockColumns, short blockRows, byte blockWidth, byte spaces = 2)
        {
            /* ROWS - Y axis */
            for (short rows = 0; rows < blockRows; rows++)
            {
                /* COLUMNS - X axis */
                for (short cols = 0; cols < blockColumns; cols++)
                {
                    BlockList.Add(
                        new BlockObject(
                                xColumn: cols,
                                yRow: rows,
                                exists: true,
                                width: blockWidth,
                                spaces: spaces,
                                objectChar: '█',
                                isBonus: false)
                        );
                }
            }
        }


        /// <summary>
        ///     Draw the blocks from GameData
        /// </summary>
        private void DrawBlocks()
        {
            foreach (BlockObject block in BlockList)
            {
                ConsolePoint blockFirstPosition = block.AbsolutXyPoints[0];

                string blck = null;
                for (int i = 0; i < block.Width; i++)
                    blck += block.ObjectChar;

                Writer.PrintAtPosition(blockFirstPosition.x, blockFirstPosition.y, blck, block.Color);
                Console.Write("  ");
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
            Writer.PrintAtPosition(0, 0, String.Format("Score: {0}", Score), ConsoleColor.DarkRed);
        }


        /// <summary>
        ///     Function prints score divider.
        /// </summary>
        private void DrawScoreDivider()
        {
            /* Score divider */
            for (int i = 0; i < _bufferWidth; i++)
                Writer.PrintAtPosition(i, 5, '-', ConsoleColor.Cyan);
        }
    }
}