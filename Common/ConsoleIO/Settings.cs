using System;
using System.Text;

namespace Common.ConsoleIO
{
    public static class Settings
    {
        public static void SetConsoleParam()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
        } 

        public static void SetConsoleParam(string prompt)
        {
            Console.Title = $"{prompt}";
            SetConsoleParam();
        }
    }
}