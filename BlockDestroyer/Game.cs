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
        private readonly int _bufferHeight;
        private readonly int _bufferWidth;
        private readonly CollisionEngine _collisionEngine;
        private readonly Random _randomGenerator;

        public Game()
        {
            _randomGenerator = new Random();
            _collisionEngine = new CollisionEngine();

            BlockList = new List<BlockObject>();

            _bufferWidth = Console.BufferWidth;
            _bufferHeight = Console.BufferHeight;
        }

        private bool IsGameRunning { get; set; }
        private int Score { get; set; }
        private BallObject Ball { get; set; }
        private BoardObject Board { get; set; }
        private List<BlockObject> BlockList { get; set; }

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
            const byte boardWidth = 10;
            const byte spaceBetweenBoards = 2;

            InitializeBlocksList(blockColumns, blockRows, blockWidth, spaceBetweenBoards);
            
            Board = new BoardObject(
                xColumn: (_bufferWidth/2) - boardWidth,
                yRow: _bufferHeight - 3,
                width: boardWidth,
                dir: _randomGenerator.Next(0, 2) == 0,
                objectChar: '-',
                exists: true,
                color: ConsoleColor.Yellow);
            
            Ball = new BallObject(
                xColumn: _bufferWidth/2,
                yRow: _bufferHeight/2,
                dir: (_randomGenerator.Next(0, 2) == 0) ? (Direction) new UpLeft() : new UpRight(),
                objectChar: 'O',
                exists: true,
                color: ConsoleColor.Red);
            DrawEdges();

            Thread inputThread = new Thread(ReadInput) {Name = "inputThread"};
            inputThread.Start();
            GameLoop(gameSpeed);
        }

        private void DrawEdges()
        {
            for (int i = 5; i < _bufferHeight - 1; i++)
            {
                /* Left edge */
                Writer.PrintAtPosition(col: 1, row: i, charToPrint: '║', color: ConsoleColor.Cyan);
                /* Right edge */
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

                MoveBall();
                DrawBall();
                

                MoveBoard();
                DrawBoard();
                

                Thread.Sleep(gameSpeed);
            }
        }

        private void MoveBall()
        {
            ConsolePoint actualBallPosition = new ConsolePoint(Ball.XColumn, Ball.YRow);
            Writer.ClearPosition(actualBallPosition.x, actualBallPosition.y);

            ConsolePoint nextBallPosition = GetNextBallPosition();

            Collision collision = _collisionEngine.DetectCollision(actualBallPosition, nextBallPosition, Board, BlockList);

            if (collision != null)
            {
                if (collision.CollidedBlockIndex != 0)
                {
                    BlockList[collision.CollidedBlockIndex].Exists = false;
                }
            }


            if (collision is TopCollision)
            {
                FlipVerticalBallDirection();
            }
            else if (collision is BottomCollision)
            {
                FlipVerticalBallDirection();
            }
            else if (collision is RightCollision)
            {
                FlipHorizontalBallDirection();
            }
            else if (collision is LeftCollision)
            {
                FlipHorizontalBallDirection();
            }
            else if (collision is BoardCollision)
            {
                FlipVerticalBallDirection();
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

        private void FlipHorizontalBallDirection()
        {
            if (Ball.Dir is UpLeft)
            {
                Ball.Dir = new UpRight();
            }
            else if (Ball.Dir is UpRight)
            {
                Ball.Dir = new UpLeft();
            }
            else if (Ball.Dir is DownLeft)
            {
                Ball.Dir = new DownRight();
            }
            else if (Ball.Dir is DownRight)
            {
                Ball.Dir = new DownLeft();
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
            int rightEnd = _bufferWidth - Board.Width - 3;

            Writer.ClearPosition(Board.XColumn, Board.YRow, Board.Width);
            Board.SetBoardExactPosition();
            /* Changing board dir if were on the end */
            if (Board.XColumn <= leftEnd)
                Board.Dir = true;
            else if (Board.XColumn >= rightEnd)
                Board.Dir = false;

            if (Board.Dir)
                Board.XColumn += 2;
            else
                Board.XColumn -= 2;
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
                {
                    if (block.Exists)
                    {
                        blck += block.ObjectChar;
                    }
                    else
                    {
                        blck += " ";
                    }
                }
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
            Writer.PrintAtPosition(0, 0, string.Format("Score: {0}", Score), ConsoleColor.DarkRed);
        }

        /// <summary>
        ///     Function prints score divider.
        /// </summary>
        private void DrawScoreDivider()
        {
        }
    }
}