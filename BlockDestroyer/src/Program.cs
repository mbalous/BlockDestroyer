using System;
using System.Threading;

namespace BlockDestroyer
{
    internal static class Program
    {
        public static void Main()
        {
            Initialize();            
            Thread soundtrackThread = new Thread(SoundTrack.StartLoop);
            soundtrackThread.Start();

            Menu mainMenu = new Menu();
            mainMenu.Run();

            Game game = new Game();
            /* Parameter is game speed, 60 is optimal */
            game.Start(60);
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