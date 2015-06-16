using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using BlockDestroyer.GameObjects;
using BlockDestroyer.GameObjects.Ball;

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
        private int Lives { get; set; }
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
            Lives = 3;

            /* GAME PARAMETERS */
            const int blockColumns = 13;
            const int blockRows = 4;
            const byte blockWidth = 7;
            const byte boardWidth = 10;
            const byte spaceBetweenBoards = 2;

            InitializeBlocksList(blockColumns, blockRows, blockWidth, spaceBetweenBoards);
            InitializeBoard(boardWidth);
            InitializeBall();

            Drawer.DrawEdges(_bufferWidth, _bufferHeight);

            Thread inputThread = new Thread(ReadInput) {Name = "inputThread"};
            inputThread.Start();
            GameLoop(gameSpeed);
        }

        private void InitializeBall()
        {
            Ball = new BallObject(_bufferWidth/2, _bufferHeight/2,
                dir: (_randomGenerator.Next(0, 2) == 0) ? (Direction) new UpLeft() : new UpRight(),
                objectChar: 'O',
                exists: true,
                color: ConsoleColor.Red);
        }

        private void InitializeBoard(byte boardWidth)
        {
            Board = new BoardObject((_bufferWidth/2) - boardWidth, _bufferHeight - 3, boardWidth,
                _randomGenerator.Next(0, 2) == 0, '-', true, ConsoleColor.Yellow);
        }

        private void GameLoop(int gameSpeed)
        {
            while (IsGameRunning)
            {
                Drawer.DrawLivesAndScore(Score, Lives);
                Drawer.DrawBlocks(BlockList);

                MoveBall();
                Drawer.DrawBall(Ball);

                MoveBoard();
                Drawer.DrawBoard(Board);

                Thread.Sleep(gameSpeed);
            }
        }

        private void MoveBall()
        {
            ConsolePoint actualBallPosition = new ConsolePoint(Ball.XColumn, Ball.YRow);
            Printer.ClearPosition(actualBallPosition.X, actualBallPosition.Y);

            ConsolePoint nextBallPosition = GetNextBallPosition();

            Collision collision = _collisionEngine.DetectCollision(actualBallPosition, nextBallPosition, Board,
                BlockList);

            if (collision != null)
            {
                if (collision.CollidedBlockIndex != 0)
                {
                    BlockObject destryoedBlock = BlockList[collision.CollidedBlockIndex];
                    BlockDestroyed(destryoedBlock);
                }
            }

            if (collision is TopCollision)
            {
                FlipVerticalBallDirection();
            }
            else if (collision is BottomEdgeCollision)
            {
                BallWasntCatched();
                return;
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

        private void BallWasntCatched()
        {
            Lives--;
            Printer.ClearPosition(Board.XColumn, Board.YRow, Board.Width);
            Printer.ClearPosition(Ball.XColumn, Ball.YRow);
            InitializeBall();
            InitializeBoard(Board.Width);
        }

        private void BlockDestroyed(BlockObject block)
        {
            block.Exists = false;
            if (block.IsBonus)
            {
                Score += 2;
            }
            else
            {
                Score += 1;
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

        private void MoveBoard()
        {
            int leftEnd = 2;
            int rightEnd = _bufferWidth - Board.Width - 3;

            Printer.ClearPosition(Board.XColumn, Board.YRow, Board.Width);
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
                        new BlockObject(cols, rows, true, blockWidth, spaces, '█', (_randomGenerator.Next(0, 9) == 0))
                        );
                }
            }
        }
    }
}