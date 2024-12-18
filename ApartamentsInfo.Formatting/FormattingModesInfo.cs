using Common.ConsoleIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ApartamentsInfo.Formatting
{
    public static class FormattingModesInfo
    {
        public static int Count { get; private set; }
        public static string[] Names { get; private set; }
        public static int AllFlagsNumber { get; private set; }
        public static string[] Headers { get; private set; }
        static FormattingModesInfo()
        {
            Names = Enum.GetNames(typeof(FormattingMode));
            Count = Names.Length;
            AllFlagsNumber = (1 << (Count - 1)) - 1;
            Headers = ((FormattingMode)AllFlagsNumber).ToFlags().Select(f => f.ToHeader()).ToArray();
        }

        public static IEnumerable<FormattingMode> ToFlags(this FormattingMode mode)
        {
            for (int i = 0; i < Count; i++)
            {
                FormattingMode flag = (FormattingMode)(1 << i);
                if (mode.HasFlag(flag))
                {
                    yield return flag;
                }
            }
        }

        public static readonly Dictionary<FormattingMode, string> _headers =
            new Dictionary<FormattingMode, string> {
                { FormattingMode.None, "Не створювати представлення об'єктів ПО" },
                { FormattingMode.DataStrings, "Дані про об'єкти ПО \"Інформація про квартири\"" },
                { FormattingMode.Statistic, "Статистичні дані про об'єкти ПО" },
                { FormattingMode.OwnersTable, "Таблиця даних про власників" },
                { FormattingMode.ApartamentsTable, "Таблиця даних про квартири" },
            };

        public static string ToHeader(this FormattingMode flag)
        {
            return _headers[flag];
        }

        public static IEnumerable<FormattingMode> Values
        {
            get
            {
                foreach (FormattingMode mode in Enum.GetValues(typeof(FormattingMode)))
                {
                    yield return mode;
                }
            }
        }

        public static bool IsSelected(this FormattingMode member, FormattingMode mode)
        {
            if (member == FormattingMode.None)
            {
                return mode.Equals(member);
            }
            return mode.HasFlag(member);
        }

        public static void SetHeader(Dictionary<FormattingMode, string> dict, FormattingMode mode, string NewString) 
        {
            if (dict.ContainsKey(mode))
            {
                dict[mode] = NewString;
                Headers = ((FormattingMode)AllFlagsNumber).ToFlags().Select(f => f.ToHeader()).ToArray();
            }
            else
            {
                Console.WriteLine("Заданий режим форматування не знайдено.");
            }
        }
    }
}
