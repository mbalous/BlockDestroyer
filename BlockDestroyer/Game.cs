using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
        private readonly CollisionEngine _collisionEngine;


        private bool IsGameRunning { get; set; }
        private int Score { get; set; }


        private BallObject Ball { get; set; }
        private BoardObject Board { get; set; }
        private List<BlockObject> BlockList { get; set; }

        readonly int _bufferWidth;
        readonly int _bufferHeight;

        public Game()
        {
            _randomGenerator = new Random();
            _collisionEngine = new CollisionEngine();

            BlockList = new List<BlockObject>();

            _bufferWidth = Console.BufferWidth;
            _bufferHeight = Console.BufferHeight;
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
                yRow: _bufferHeight - 3,
                width: boardWidth,
                dir: _randomGenerator.Next(0, 2) == 0,
                objectChar: '-',
                exists: true,
                color: ConsoleColor.Yellow);

            Ball = new BallObject(
                xColumn: _bufferWidth / 2,
                yRow: _bufferHeight / 2,
                dir: (_randomGenerator.Next(0, 2) == 0) ? (Direction)new UpLeft() : new UpRight(),
                objectChar: 'O',
                exists: true,
                color: ConsoleColor.Red);

            GenerateCorners();
            DrawEdges();

            Thread inputThread = new Thread(ReadInput) { Name = "inputThread" };
            inputThread.Start();
            GameLoop(gameSpeed);
        }

        private void GenerateCorners()
        {

        }

        private void DrawEdges()
        {
            /* Left edge */
            for (int i = 5; i < _bufferHeight - 1; i++)
            {
                Writer.PrintAtPosition(col: 1, row: i, charToPrint: '║', color: ConsoleColor.Cyan);
            }
            /* Right edge */
            for (int i = 5; i < _bufferHeight - 1; i++)
            {
                Writer.PrintAtPosition(col: _bufferWidth - 2, row: i, charToPrint: '║', color: ConsoleColor.Cyan);
            }

            /* TOP & bottom edge */
            for (int i = 1; i < _bufferWidth - 1; i++)
            {
                /* TOP egde - score divider */
                if (i == 1)
                {
                    /* TOP edge */
                    Writer.PrintAtPosition(col: i, row: 5, charToPrint: '╔', color: ConsoleColor.Cyan);
                    /* Bottom edge */
                    Writer.PrintAtPosition(col: i, row: _bufferHeight - 2, charToPrint: '╚', color: ConsoleColor.Cyan);
                    continue;
                }
                if (i == _bufferWidth - 2)
                {
                    /* TOP edge */
                    Writer.PrintAtPosition(col: i, row: 5, charToPrint: '╗', color: ConsoleColor.Cyan);
                    /* Bottom edge */
                    Writer.PrintAtPosition(col: i, row: _bufferHeight - 2, charToPrint: '╝', color: ConsoleColor.Cyan);
                    continue;
                }
                Writer.PrintAtPosition(col: i, row: 5, charToPrint: '═', color: ConsoleColor.Cyan);

                /* Bottom egde */
                Writer.PrintAtPosition(col: i, row: _bufferHeight - 2, charToPrint: '═', color: ConsoleColor.Cyan);
            }
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
            Writer.ClearPosition(actualBallPosition.x, actualBallPosition.y);

            ConsolePoint nextBallPosition = GetNextBallPosition();

            Collision collison = _collisionEngine.DetectCollision(actualBallPosition, nextBallPosition, Board, BlockList);

            if (collison is TopCollision)
            {
                FlipVerticalBallDirection();
            }
            else if (collison is BottomCollision)
            {
                FlipVerticalBallDirection();
            }
            else if (collison is RightCollision)
            {

            }
            else if (collison is LeftCollision)
            {

            }

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

        private void FlipVerticalBallDirection()
        {
            if (Ball.Dir is UpLeft)
            {
                Ball.Dir = new DownLeft();
            }
            else if (Ball.Dir is UpRight)
            {
                Ball.Dir = new DownRight();
            }
            else if (Ball.Dir is DownLeft)
            {
                Ball.Dir = new UpLeft();
            }
            else if (Ball.Dir is DownRight)
            {
                Ball.Dir = new UpRight();
            }
        }

        private ConsolePoint GetNextBallPosition()
        {
            ConsolePoint nextBallPosition = null;

            if (Ball.Dir is UpLeft)
            {
                nextBallPosition = new ConsolePoint(Ball.XColumn - 1, Ball.YRow - 1);
            }
            else if (Ball.Dir is UpRight)
            {
                nextBallPosition = new ConsolePoint(Ball.XColumn + 1, Ball.YRow - 1);
            }
            else if (Ball.Dir is DownLeft)
            {
                nextBallPosition = new ConsolePoint(Ball.XColumn - 1, Ball.YRow + 1);
            }
            else if (Ball.Dir is DownRight)
            {
                nextBallPosition = new ConsolePoint(Ball.XColumn + 1, Ball.YRow + 1);
            }
            return nextBallPosition;
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
                {
                    board += Board.ObjectChar;
                }

                Writer.PrintAtPosition(Board.XColumn, Board.YRow, board, Board.Color);
            }
        }

        private void MoveBoard()
        {
            int leftEnd = 2;
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

        }
    }
}