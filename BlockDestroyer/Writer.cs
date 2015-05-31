using System;

/*
 * Columns stands for xColumn axis
 * Rows stands for yRow axis
 */

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

        public static void PrintAtPosition(int col, int row, string stringToPrint,
            ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(col, row);
            Console.ForegroundColor = color;
            Console.Write(stringToPrint);
            Console.ResetColor();
        }

        public static void ClearPosition(int col, int row, int chars = 1)
        {
            Console.SetCursorPosition(col, row);
            if (chars <= 0)
                return;
            for (int i = 0; i < chars; i++)
                Console.Write(' ');
        }
    }
}