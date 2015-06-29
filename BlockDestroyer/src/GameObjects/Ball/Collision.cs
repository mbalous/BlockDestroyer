namespace BlockDestroyer.GameObjects.Ball
{
    internal abstract class Collision
    {
        protected Collision(ConsolePoint collisionPoint, int blockIndex = 0)
        {
            CollisionPoint = collisionPoint;
            CollidedBlockIndex = blockIndex;
        }

        public int CollidedBlockIndex { get; set; }
        public ConsolePoint CollisionPoint { get; set; }
    }

    internal class LeftCollision : Collision
    {
        public LeftCollision(ConsolePoint collisionPoint, int blockIndex = 0) : base(collisionPoint, blockIndex)
        {
        }
    }

    internal class RightCollision : Collision
    {
        public RightCollision(ConsolePoint collisionPoint, int blockIndex = 0) : base(collisionPoint, blockIndex)
        {
        }
    }

    internal class TopCollision : Collision
    {
        public TopCollision(ConsolePoint collisionPoint, int blockIndex = 0) : base(collisionPoint, blockIndex)
        {
        }
    }

    internal class BottomCollision : Collision
    {
        public BottomCollision(ConsolePoint collisionPoint, int blockIndex = 0) : base(collisionPoint, blockIndex)
        {
        }
    }

    internal class BoardCollision : Collision
    {
        public BoardCollision(ConsolePoint collisionPoint, int blockIndex = 0) : base(collisionPoint, blockIndex)
        {
        }
    }

    internal class BottomEdgeCollision : Collision
    {
        public BottomEdgeCollision(ConsolePoint collisionPoint, int blockIndex = 0) : base(collisionPoint, blockIndex)
        {
        }
    }
}