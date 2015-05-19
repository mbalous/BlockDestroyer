using System;

namespace BlockDestroyer
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "BlockDestroyer";
            Game game = new Game();
            game.Start();
        }
    }
}
