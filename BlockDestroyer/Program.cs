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
            /* Parameter is game speed, 20 is optimal */
            game.Start(20);
        }

        private static void Initialize()
        {
            Console.Title = "**BlockObject Destroyer**";
            Console.CursorVisible = false;

            Console.BufferHeight = Console.WindowHeight = 40;
            Console.BufferWidth = Console.WindowWidth = 125;

            Console.CursorVisible = false;
        }
    }
}