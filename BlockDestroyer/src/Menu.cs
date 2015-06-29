using System;
using System.Collections.Generic;
using System.Threading;

namespace BlockDestroyer
{
    internal class Menu
    {
        private int _option;
        private bool DidChoose { get; set; }

        public void Run()
        {
            /* First option is selected */
            _option = 0;

            Console.Clear();
            DisplayGraphics();

            Thread readInput = new Thread(KeyReader) {Name = "readInputThread"};
            readInput.Start();

            MenuPrinter();
        }

        private void MenuPrinter()
        {
            Dictionary<int, string> menuItems = new Dictionary<int, string>
            {
                {0, "Play Game"},
                {1, "High scores"},
                {2, "About"},
                {3, "Sound"},
                {4, "Exit"}
            };

            while (true)
            {
                lock (this)
                {
                    if (DidChoose)
                        return;

                    Console.SetCursorPosition(0, 20);
                    for (int i = 0; i < menuItems.Count; i++)
                    {
                        if (i == _option)
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
                }
                Thread.Sleep(50);
            }
        }

        private void NextOption()
        {
            if (_option < 4)
                _option++;
        }

        private void PrevOption()
        {
            if (_option > 0)
                _option--;
        }

        private void KeyReader()
        {
            while (true)
            {
                ConsoleKey pressedKey = Console.ReadKey(true).Key;

                /* In order to detect numerous keys pressed at once */
                switch (pressedKey)
                {
                    case ConsoleKey.UpArrow:
                        PrevOption();
                        break;
                    case ConsoleKey.DownArrow:
                        NextOption();
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        DidChoose = true;
                        OptionSelected();
                        return;
                }
            }
        }

        private void OptionSelected()
        {
            switch (_option)
            {
                case 0: //Play
                    Console.Clear();
                    break;
                case 1: //High Score
                    /* TODO: Implement high scores */
                    Environment.Exit(_option);
                    break;
                case 2: //About
                    /* TODO: Implement about */
                    Environment.Exit(_option);
                    break;
                case 3: //Sound
                    SoundTrack.StopLoop();
                    break;
                case 4: //Exit
                    Environment.Exit(_option);
                    break;
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
                Printer.PrintAtPosition(i, 10, '-');

            Console.ResetColor();
        }
    }
}