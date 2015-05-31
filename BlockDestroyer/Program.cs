using System;
using System.Threading;

namespace BlockDestroyer
{
    internal static class Program
    {
        public static void Main()
        {
            Initialize();
            SoundTrack soundTrack = new SoundTrack();
            Thread soundtrackThread = new Thread(soundTrack.StartLoop);
            soundtrackThread.Start();
#if !DEBUG
            Menu mainMenu = new Menu();
            mainMenu.Run();
#endif
            Game game = new Game();
            /* Parameter is game speed, 20 is optimal */
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