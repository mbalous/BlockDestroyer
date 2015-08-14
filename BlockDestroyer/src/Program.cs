using System;
using System.Threading;

namespace BlockDestroyer
{
    internal static class Program
    {
        public static void Main()
        {
            Initialize();
            Thread soundtrackThread = new Thread(SoundTrack.StartLoop) { Name = "soundTrackThread" };
            soundtrackThread.Start();
            ShowMenu();
        }

        public static void ShowMenu()
        {
            Menu mainMenu = new Menu();
            mainMenu.Display();
        }

        private static void Initialize()
        {
            Console.Title = "**Block Destroyer**";
            Console.CursorVisible = false;

            Console.BufferHeight = Console.WindowHeight = 40;
            Console.BufferWidth = Console.WindowWidth = 125;

            Console.CursorVisible = false;
        }
    }
}