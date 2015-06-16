using System.Collections.Generic;
using System.Linq;

namespace BlockDestroyer.GameObjects.Ball
{
    internal class CollisionEngine
    {
        private ConsolePoint ActualBallPosition { get; set; }
        private ConsolePoint NextBallPosition { get; set; }
        private List<ConsolePoint> BoardPosition { get; set; }
        private List<BlockObject> BlockList { get; set; }

        public Collision DetectCollision(ConsolePoint actualBallPosition, ConsolePoint nextBallPosition,
            BoardObject board, List<BlockObject> blockList)
        {
            ActualBallPosition = actualBallPosition;
            NextBallPosition = nextBallPosition;
            BoardPosition = board.AbsolutXyPoints;
            BlockList = blockList;

            /* If corner collision is null then check for block collision */
            Collision collision = CheckForCornerCollision() ?? CheckForBlockCollision();
            return collision;
        }

        private Collision CheckForBlockCollision()
        {
            for (int i = 0; i < BlockList.Count - 1; i++)
            {
                if (BlockList[i].Exists == false)
                    continue;
                int i1 = i;
                foreach (Collision collisionType in from point in BlockList[i].AbsolutXyPoints
                    where point.X == NextBallPosition.X && point.Y == NextBallPosition.Y
                    select FindBlockCollisionType(point, BlockList[i1], i1, ActualBallPosition))
                {
                    return collisionType;
                }
            }
            return null;
        }


        /// <summary>
        ///     Find which collision happened.
        /// </summary>
        /// <param name="collisionPoint"></param>
        /// <param name="block"></param>
        /// <param name="blockIndex"></param>
        /// <param name="actualBallPosition"></param>
        /// <returns></returns>
        private Collision FindBlockCollisionType(ConsolePoint collisionPoint, BlockObject block, int blockIndex,
            ConsolePoint actualBallPosition)
        {
            /* Ball impacted block from above */
            if (actualBallPosition.Y < collisionPoint.Y)
            {
                /* TopLeft impact */
                if (actualBallPosition.X < collisionPoint.X)
                {
                    /* Ball colided on the left side of the block */
                    if (collisionPoint == block.AbsolutXyPoints[0])
                    {
                        return new RightCollision(collisionPoint, blockIndex);
                    }
                    return new BottomCollision(collisionPoint, blockIndex);
                }

                /* TopRight impact */
                if (actualBallPosition.X > collisionPoint.X)
                {
                    /* Ball collided on the right side of the block*/
                    if (collisionPoint == block.AbsolutXyPoints[block.Width - 1])
                    {
                        return new LeftCollision(collisionPoint, blockIndex);
                    }
                    return new BottomCollision(collisionPoint, blockIndex);
                }
            }
            /* Ball impacted block from beneath. */
            else if (actualBallPosition.Y > collisionPoint.Y) // bottom
            {
                /* BottomLeft impact */
                if (actualBallPosition.X < collisionPoint.X)
                {
                    /* Ball colided on the left side of the block */
                    if (collisionPoint == block.AbsolutXyPoints[0])
                    {
                        return new RightCollision(collisionPoint, blockIndex);
                    }
                    return new TopCollision(collisionPoint, blockIndex);
                }

                /* BottomRight impact */
                if (actualBallPosition.X > collisionPoint.X)
                {
                    /* Ball collided on the right side of the block*/
                    if (collisionPoint == block.AbsolutXyPoints[block.Width - 1])
                    {
                        return new LeftCollision(collisionPoint, blockIndex);
                    }
                    return new TopCollision(collisionPoint, blockIndex);
                }
            }
            return null;
        }

        private Collision CheckForCornerCollision()
        {
            const int topCornerRow = 5;
            const int botCornerRow = 38;
            const int leftCornerCol = 1;
            const int rightCornerCol = 123;

            /* TOP */
            if (NextBallPosition.Y <= topCornerRow)
            {
                return new TopCollision(NextBallPosition);
            }

            /* Board */
            int width = BoardPosition.Count;
            if (NextBallPosition.Y == BoardPosition[0].Y && NextBallPosition.X >= BoardPosition[0].X &&
                NextBallPosition.X <= BoardPosition[width - 1].X)
            {
                return new BoardCollision(NextBallPosition);
            }

            /* Bottom */
            if (NextBallPosition.Y >= botCornerRow)
            {
                return new BottomEdgeCollision(NextBallPosition);
            }

            /* Left */
            if (NextBallPosition.X <= leftCornerCol)
            {
                return new LeftCollision(NextBallPosition);
            }

            /* Right */
            if (NextBallPosition.X >= rightCornerCol)
            {
                return new RightCollision(NextBallPosition);
            }
            return null;
        }
    }
}