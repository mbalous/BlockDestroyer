using System;

namespace BlockDestroyer
{
    internal class Menu
    {
        public void Display()
        {
            Console.Clear();
            DisplayGraphics();
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
