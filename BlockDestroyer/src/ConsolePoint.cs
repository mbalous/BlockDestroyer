namespace BlockDestroyer
{
    /// <summary>
    ///     Class representing exact position in console windows.
    /// </summary>
    internal struct ConsolePoint
    {
        public readonly int X;
        public readonly int Y;

        public ConsolePoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
