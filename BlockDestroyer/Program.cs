using System;

namespace BlockDestroyer
{
    static class Program
    {
        static void Main(string[] args)
        {
            Initialize();
            
            Menu mainMenu = new Menu();
            mainMenu.Display();
            
            Game game = new Game();
            game.Start();
        }

        private static void Initialize()
        {
            Console.Title = "**Block Destroyer**";
            Console.CursorVisible = false;

            Console.BufferHeight = Console.WindowHeight = 40;
            Console.BufferWidth = Console.WindowWidth = 100;
        }
    }
}