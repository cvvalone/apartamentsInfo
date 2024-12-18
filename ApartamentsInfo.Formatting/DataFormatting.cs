using ApartamentsInfo.Data.Extensions;
using ApartamentsInfo.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ApartamentsInfo.Formatting
{
    public static class DataFormatting
    {
        public static readonly Dictionary<FormattingMode, Func<IDataSet, string, string>> Formatters = new Dictionary<FormattingMode, Func<IDataSet, string, string>>()
        {
            { FormattingMode.None, null },
            { FormattingMode.DataStrings, FormattingMethods.ToDataString },
            { FormattingMode.Statistic, FormattingMethods.ToStatisticString },
            { FormattingMode.OwnersTable, FormattingMethods.ToOwnersTable },
            { FormattingMode.ApartamentsTable, FormattingMethods.ToApartamentsTable },
        };

        public static string Format(this IDataSet dataSet, FormattingMode mode)
        {
            StringBuilder sb = new StringBuilder();
            var flags = mode.ToFlags();
            foreach (var flag in flags)
            {
                if(sb.Length > 0)
                {
                    sb.AppendLine();
                }
                string header = flag.ToHeader();
                if (Formatters[flag] != null)
                {
                    sb.AppendLine(Formatters[flag](dataSet, header));
                }
            }
            return sb.ToString();
        }


    }
}
