using System;

namespace BlockDestroyer
{
    /// <summary>
    ///     Class using for writing strings or characters to a desired place.
    /// </summary>
    static class Writer
    {

        public static void WriteCharAtPosition(int x, int y, char @char, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(@char);
            Console.ResetColor();
        }

        public static void WriteCharAtPosition(int x, int y, char @char)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(@char);
        }

        public static void WriteStringAtPosition(int x, int y, string text, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void WriteStringAtPosition(int x, int y, string text)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(text);
        }
    }
}
