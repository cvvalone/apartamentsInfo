using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.ConsoleIO
{
    public static class Entering
    {
        static string format = "{0,40}: ";

        public static string Format
        {
            get { return format; }
        }
        public static int EnterInt(string prompt)
        {
            while(true)
            {
                Console.Write($"\t{prompt}:");
                string str = Console.ReadLine();
                try 
                {
                    return Convert.ToInt32(str);
                } 
                catch (OverflowException)
                {
                    Console.WriteLine("Занадто велике значення для числа типу int");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Неправильний формат задання числа типу int");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\t\t{ex.GetType().Name}: {ex.Message}");
                }
            }
        }
        public static int EnterInt(string prompt, int minValue, int maxValue)
        {
            while(true)
            {
                int valueOf = EnterInt(prompt);
                if (valueOf >= minValue && valueOf <= maxValue)
                {
                    return valueOf;
                }
                Console.WriteLine($"\tВи ввели значення яке не входить в діапазон {minValue} <= x <= {maxValue}. Спробуйте ще раз.");
            }
        }
        public static int EnterInt(string prompt, Selector selector, string errorMessage)
        {
            while (true)
            {
                int valueOf = EnterInt(prompt);
                if (selector(valueOf))
                {
                    return valueOf;
                }
                Console.WriteLine(errorMessage);
            }
        }

        public static string EnterString(string prompt)
        {
            Console.Write(format,prompt);
            string str = Console.ReadLine();
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            if(str == "\"\"")
            {
                return "";
            }

            str = str.Trim();
            str = RemoveDuplicateSpaces(str);
            return str;
        }

        public static string EnterString(string prompt, int minLength, int maxLength)
        {
            while (true)
            {
                string str = EnterString(prompt);
                if(str == null ||  minLength <= str.Length && str.Length <= maxLength)
                {
                    return str;
                }
                Console.WriteLine($"Потрібно ввести значення котре не перевищує {maxLength}, і не менша {minLength} символів.");
            }
        }

        public static string EnterString(string prompt, string pattern, string errorMessage, RegexOptions options)
        {
            while(true)
            {
                string str = EnterString(prompt);
                if (str == null)
                {
                    return null;
                }
                if (Regex.IsMatch(str, pattern, options))
                {
                    return str;
                }
                Console.WriteLine($"\t\t{errorMessage}");
            }
        }

        public static void SetPromptWidth(int width)
        {
            format = string.Format("{{0,{0}}}: ", width);
        }

        public static string RemoveDuplicateSpaces(string str)
        {
            Regex regex = new Regex(@"\s+");
            str = regex.Replace(str, " ");
            return str;
        }

        public static int? EnterNullableInt32(string prompt)
        {
            while (true)
            {
                Console.WriteLine(format, string.Format($"{prompt} (<ENTER> - NULL)"));
                string str = Console.ReadLine();
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }
                try{
                    return Convert.ToInt32(str);
                }
                catch(Exception) 
                {
                    Console.WriteLine("\t\tпомилка введення цілого числа");
                }
            }
        }

        public static int? EnterNullableInt32(string prompt, int min, int max = int.MaxValue)
        {
            while (true)
            {
                int? value = EnterNullableInt32(prompt);
                if (value == null || min < value && value <= max)
                {
                    return value;
                }
                Console.WriteLine($"\tВведіть значення в межах від {min} до {max}");
            }
        }

        public static string EnterText(string prompt)
        {
            Console.WriteLine(format, prompt);
            string input = "";
            while(true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    input += Environment.NewLine;
                }
                else if (key.Key == ConsoleKey.Z && key.Modifiers == ConsoleModifiers.Control)
                {
                    break;
                }
                else
                {
                    input += key.KeyChar;
                }
            }
            return input;

        }

        public static bool EnterBoolean(string prompt)
        {
            Console.WriteLine(format, prompt);
            string result = Console.ReadLine();
            bool res = false;
            if (result != null && result == "Так") res = true;
            return res;
        }
    }
}
