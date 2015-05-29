namespace BlockDestroyer.GameObjects.Ball
{
    class CollisionEngine
    {
        public CollisionEngine()
        {
            
        }

        public bool FindCollision(int ballXposition, int ballYposition)
        {
            return true;
        }

        public Collision GetCollisionType()
        {
            return new BottomCollision();

        }
    }
}