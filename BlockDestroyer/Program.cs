using System;

namespace BlockDestroyer
{
    static class Program
    {
        static void Main()
        {
            Initialize();
            
#if !DEBUG
            Menu mainMenu = new Menu();
            mainMenu.Run();
#endif   
            Game game = new Game();
            game.Start();
        }

        private static void Initialize()
        {
            Console.Title = "**Block Destroyer**";
            Console.CursorVisible = false;

            Console.BufferHeight = Console.WindowHeight = 40;
            Console.BufferWidth = Console.WindowWidth = 100;

            Console.CursorVisible = false;
        }
    }
}