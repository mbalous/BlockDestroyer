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

            /* If corner collision is null then chech for block collision */
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
                    where point.x == NextBallPosition.x && point.y == NextBallPosition.y
                    select FindBlockCollisionType(point, BlockList[i1], i1, ActualBallPosition))
                {
                    return collisionType;
                }
            }
            return null;
        }

        private Collision FindBlockCollisionType(ConsolePoint point, BlockObject block, int blockIndex,
            ConsolePoint actualBallPosition)
        {
            /* Ball impacted block from above */
            if (actualBallPosition.y < point.y)
            {
                /* TopLeft impact */
                if (actualBallPosition.x < point.x)
                {
                    /* Ball colided on the left side of the block */
                    if (point == block.AbsolutXyPoints[0])
                    {
                        return new RightCollision(point, blockIndex);
                    }
                    return new BottomCollision(point, blockIndex);
                }

                /* TopRight impact */
                if (actualBallPosition.x > point.x)
                {
                    /* Ball collided on the right side of the block*/
                    if (point == block.AbsolutXyPoints[block.Width - 1])
                    {
                        return new LeftCollision(point, blockIndex);
                    }
                    return new BottomCollision(point, blockIndex);
                }
            }
            /* Ball impacted block from beneath. */
            else if (actualBallPosition.y > point.y) // bottom
            {
                /* BottomLeft impact */
                if (actualBallPosition.x < point.x)
                {
                    /* Ball colided on the left side of the block */
                    if (point == block.AbsolutXyPoints[0])
                    {
                        return new RightCollision(point, blockIndex);
                    }
                    return new TopCollision(point, blockIndex);
                }

                /* BottomRight impact */
                if (actualBallPosition.x > point.x)
                {
                    /* Ball collided on the right side of the block*/
                    if (point == block.AbsolutXyPoints[block.Width - 1])
                    {
                        return new LeftCollision(point, blockIndex);
                    }
                    return new TopCollision(point, blockIndex);
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
            if (NextBallPosition.y <= topCornerRow)
            {
                return new TopCollision(NextBallPosition);
            }

            /* Board */
            int width = BoardPosition.Count;
            if (NextBallPosition.y == BoardPosition[0].y && NextBallPosition.x >= BoardPosition[0].x &&
                NextBallPosition.x <= BoardPosition[width - 1].x)
            {
                return new BoardCollision(NextBallPosition);
            }

            /* Bottom */
            if (NextBallPosition.y >= botCornerRow)
            {
                return new BottomEdgeCollision(NextBallPosition);
            }

            /* Left */
            if (NextBallPosition.x <= leftCornerCol)
            {
                return new LeftCollision(NextBallPosition);
            }

            /* Right */
            if (NextBallPosition.x >= rightCornerCol)
            {
                return new RightCollision(NextBallPosition);
            }
            return null;
        }
    }
}