using System;
using System.Collections.Generic;
using BlockDestroyer.GameObjects;
using BlockDestroyer.GameObjects.Ball;

namespace BlockDestroyer
{
    internal static class Drawer
    {
        /// <summary>
        ///     Draw the blocks.
        /// </summary>
        public static void DrawBlocks(IEnumerable<BlockObject> blockList)
        {
            foreach (BlockObject block in blockList)
            {
                ConsolePoint blockFirstPosition = block.AbsolutXyPoints[0];
                string blck = null;

                for (int i = 0; i < block.Width; i++)
                {
                    if (block.Exists)
                        blck += block.ObjectChar;
                    else
                        blck += " ";
                }
                Printer.PrintAtPosition(blockFirstPosition.X, blockFirstPosition.Y, blck, block.Color);
            }
        }


        /// <summary>
        ///     Prints score above the score divider.
        /// </summary>
        public static void DrawLivesAndScore(int score, int lives)
        {
            Printer.PrintAtPosition(0, 0, string.Format("Score: {0}", score), ConsoleColor.DarkRed);
            Printer.PrintAtPosition(0, 1, string.Format("Lives: {0}", lives), ConsoleColor.DarkMagenta);
        }


        /// <summary>
        ///     Draws edges of the game field.
        /// </summary>
        /// <param name="bufferWidth">Console buffer width</param>
        /// <param name="bufferHeight">Console buffer height</param>
        public static void DrawEdges(int bufferWidth, int bufferHeight)
        {
            for (int i = 5; i < bufferHeight - 1; i++)
            {
                /* Left edge */
                Printer.PrintAtPosition(1, i, '║', ConsoleColor.Cyan);
                /* Right edge */
                Printer.PrintAtPosition(bufferWidth - 2, i, '║', ConsoleColor.Cyan);
            }

            /* TOP & bottom edge */
            for (int i = 1; i < bufferWidth - 1; i++)
            {
                /* TOP egde - score divider */
                if (i == 1)
                {
                    /* TOP edge */
                    Printer.PrintAtPosition(i, 5, '╔', ConsoleColor.Cyan);
                    /* Bottom edge */
                    Printer.PrintAtPosition(i, bufferHeight - 2, '╚', ConsoleColor.Cyan);
                    continue;
                }
                if (i == bufferWidth - 2)
                {
                    /* TOP edge */
                    Printer.PrintAtPosition(i, 5, '╗', ConsoleColor.Cyan);
                    /* Bottom edge */
                    Printer.PrintAtPosition(i, bufferHeight - 2, '╝', ConsoleColor.Cyan);
                    continue;
                }
                Printer.PrintAtPosition(i, 5, '═', ConsoleColor.Cyan);

                /* Bottom egde */
                Printer.PrintAtPosition(i, bufferHeight - 2, '═', ConsoleColor.Cyan);
            }
        }


        /// <summary>
        ///     Draws the ball.
        /// </summary>
        /// <param name="ball"></param>
        public static void DrawBall(BallObject ball)
        {
            Printer.PrintAtPosition(ball.XColumn, ball.YRow, ball.ObjectChar, ball.Color);
        }


        /// <summary>
        ///     Draws the board.
        /// </summary>
        public static void DrawBoard(BoardObject board)
        {
            if (board.Exists)
            {
                string boardString = null;
                for (int i = 0; i < board.Width; i++)
                {
                    boardString += board.ObjectChar;
                }
                Printer.PrintAtPosition(board.XColumn, board.YRow, boardString, board.Color);
            }
        }
    }
}