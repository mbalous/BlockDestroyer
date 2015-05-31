using System;
using System.Collections.Generic;

namespace BlockDestroyer.GameObjects.Ball
{
    class CollisionEngine
    {
        private ConsolePoint ActualBallPosition { get; set; }
        private ConsolePoint NextBallPosition { get; set; }
        private List<ConsolePoint> BoardPosition { get; set; }
        private List<BlockObject> BlockList { get; set; }

        

        public CollisionEngine()
        {
            
        }

        public Collision DetectCollision(ConsolePoint actualBallPosition, ConsolePoint nextBallPosition, BoardObject board, List<BlockObject> blockList)
        {
            ActualBallPosition = actualBallPosition;
            NextBallPosition = nextBallPosition;
            BoardPosition = board.AbsolutXyPoints;
            BlockList = blockList;

            Collision cornerCollision = CheckForCornerCollision();
            if (cornerCollision != null)
            {
                return cornerCollision;
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
                    return new TopCollision();
                }

                // ignored
            
            return null;
        }
    }
}