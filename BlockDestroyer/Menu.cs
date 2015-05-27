using System;
using System.Threading;

namespace BlockDestroyer
{
    internal class Menu
    {
        private int _option;

        public void Run()
        {
            Console.Clear();

            Thread readInput = new Thread(KeyReader);
            readInput.Start();

            Option = 1;

            DisplayGraphics();
        }

        private int Option
        {
            get
            {
                return _option;
            }
            set
            {
                switch (Option)
                {
                    case 0:
                        Option = 1;
                        break;
                    case 5:
                        Option = 4;
                        break;
                    default:
                        _option = value;
                        break;
                }
            }
        }

        private void KeyReader()
        {
            while (true)
            {
                ConsoleKey pressedKey = Console.ReadKey().Key;
                /* In order to detect numerous keys pressed at once */
                while (Console.KeyAvailable)
                    Console.ReadKey(true);

                switch (pressedKey)
                {
                    case ConsoleKey.UpArrow:
                        Option--;
                        break;
                    case ConsoleKey.DownArrow:
                        Option++;
                        break;
                    case ConsoleKey.Enter:
                        break;
                }
            }
        }

        /// <summary>
        ///     Function writing graphics stuff on screen.
        /// </summary>
        private static void DisplayGraphics()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            string[] blockStrings = {
                @"______  _               _     ______            _                                  ",
                @"| ___ \| |             | |    |  _  \          | |                                 ",
                @"| |_/ /| |  ___    ___ | | __ | | | | ___  ___ | |_  _ __  ___   _   _   ___  _ __ ",
                @"| ___ \| | / _ \  / __|| |/ / | | | |/ _ \/ __|| __|| '__|/ _ \ | | | | / _ \| '__|",
                @"| |_/ /| || (_) || (__ |   <  | |/ /|  __/\__ \| |_ | |  | (_) || |_| ||  __/| |   ",
                @"\____/ |_| \___/  \___||_|\_\ |___/  \___||___/ \__||_|   \___/  \__, | \___||_|   ",
                @"                                                                  __/ |            ",
                @"                                                                 |___/"
            };
            foreach (string row in blockStrings)
                Console.WriteLine(row);

            for (int i = 0; i < Console.WindowWidth; i++) // Divider
                Writer.PrintAtPosition(i, 10, '-');

            Console.ResetColor();
        }

    }
}
