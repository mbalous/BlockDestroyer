using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Threading;

namespace BlockDestroyer
{
    internal class Menu
    {
        private Choices _selectedChoice;
        private Difficulty _selectedDifficulty;
        private readonly List<Choice> _choices;

        struct Choice
        {
            public readonly string name;
            public readonly Choices choice;

            public Choice(Choices choice)
            {
                this.choice = choice;
                this.name = Enum.GetName(typeof(Choices), choice)?.Replace('_', ' ');
            }
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        enum Choices
        {
            Play_game,
            Change_difficulty,
            High_scores,
            Toggle_sound,
            About,
            Exit_game
        }

        public Menu()
        {
            _choices = new List<Choice>
            {
                new Choice(Choices.Play_game),
                new Choice(Choices.Change_difficulty),
                new Choice(Choices.High_scores),
                new Choice(Choices.Toggle_sound),
                new Choice(Choices.About),
                new Choice(Choices.Exit_game)
            };
        }

        public void Display()
        {
            Console.Clear();
            DisplayMenuGfx();

            // First option is selected
            _selectedChoice = 0;
            _selectedDifficulty = Difficulty.Easy;

            Thread readInputThread = new Thread(KeyReader) { Name = "readInputThread" };
            readInputThread.Start();
        }

        private void DisplayMenu()
        {
            // Loop which changes color of selected item.
            Console.SetCursorPosition(0, 20);
            for (int i = 0; i < _choices.Count; i++)
            {
                if (_selectedChoice == _choices[i].choice)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(_choices[i].name);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(_choices[i].name);
                }
            }
        }

        private void NextOption()
        {
            if (_selectedChoice < (Choices)_choices.Count)
                _selectedChoice++;
        }

        private void PreviousOption()
        {
            if (_selectedChoice > 0)
                _selectedChoice--;
        }

        private void KeyReader()
        {
            while (true)
            {
                DisplayMenu();
                ConsoleKey pressedKey = Console.ReadKey(true).Key;
                /* In order to detect numerous keys pressed at once */
                switch (pressedKey)
                {
                    case ConsoleKey.UpArrow:
                        PreviousOption();
                        break;
                    case ConsoleKey.DownArrow:
                        NextOption();
                        break;
                    case ConsoleKey.Enter:
                        SelectOption();
                        return;
                }
            }
        }

        private void SelectOption()
        {
            switch (_selectedChoice)
            {
                
            }
        }

        /// <summary>
        ///     Function writing graphics stuff on screen.
        /// </summary>
        private static void DisplayMenuGfx()
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