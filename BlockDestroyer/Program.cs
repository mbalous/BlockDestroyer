using System;

namespace BlockDestroyer
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "BlockDestroyer";
            Console.CursorVisible = false;

            Game game = new Game();
            game.Start();
        }
    }
}
