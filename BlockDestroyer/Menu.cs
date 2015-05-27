using System;
using System.Collections.Generic;
using System.Threading;

namespace BlockDestroyer
{
    internal class Menu
    {
        private bool Choosed { get; set; }
        private int _option;

        private int Option
        {
            get { return _option; }
            set
            {
                switch (Option)
                {
                    case 0:
                        _option = 1;
                        break;
                    case 5:
                        _option = 4;
                        break;
                    default:
                        _option = value;
                        break;
                }
            }
        }

        public void Run()
        {
            /* First option is selected */
            Option = 1; 

            Console.Clear();
            DisplayGraphics();

            Thread readInput = new Thread(KeyReader);
            readInput.Start();
            
            Thread printMenu = new Thread(PrintMenu);
            printMenu.Start();            
        }

        private void PrintMenu()
        {
            Dictionary<int, string> menuItems = new Dictionary<int, string>
            {
                {1, "Play Game"},
                {2, "High scores"},
                {3, "About"},
                {4, "Exit"}
            };

            while (true)
            {
                Console.SetCursorPosition(0, 20);

                for (int i = 1; i <= menuItems.Count; i++)
                {
                    if (i == Option)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(menuItems[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(menuItems[i]);
                    }
                }

                Thread.Sleep(50);
            }
        }


        private void KeyReader()
        {
            while (true)
            {
                if (Choosed)
                    break;

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
                        Choosed = true;
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
            string[] blockStrings =
            {
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
