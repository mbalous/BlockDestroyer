using System;

namespace BlockDestroyer
{
    /// <summary>
    ///     Class using for writing strings or characters to line and row given in parameters.
    /// </summary>
    internal static class Writer
    {
        public static void PrintAtPosition(int col, int row, char charToPrint, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(col, row);
            Console.ForegroundColor = color;
            Console.Write(charToPrint);
            Console.ResetColor();
        }

        public static void PrintAtPosition(int col, int row, string stringToPrint, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(col, row);
            Console.ForegroundColor = color;
            Console.Write(stringToPrint);
            Console.ResetColor();
        }
    }
}