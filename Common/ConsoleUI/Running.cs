using System;

namespace Common.ConsoleUI
{
    public static class Running
    {
        public delegate void Command();
        public static void CountinueGoing(Command command)
        {
            ConsoleKey key = ConsoleKey.Escape;
            do
            {
                command?.Invoke();

                Console.WriteLine("Для продовження натисніть клавішу \"Enter\": ");
                key = Console.ReadKey().Key;
                Console.Clear();
            } while (key == ConsoleKey.Enter);
        }
    }
}