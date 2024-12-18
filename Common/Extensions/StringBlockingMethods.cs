using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class StringBlockingMethods
    {
        public static int MaxLineLength = 79;
        public static string ToTextBlock(this string text, string indent)
        {
            StringBuilder sb = new StringBuilder(text.Length * 2);
            int pos = 0;
            int len = MaxLineLength - indent.Length - 1;
            while (pos < text.Length) {
                sb.Append(indent);
                if (text.Length - pos >= len) {
                    sb.AppendFormat("{0}\n", text.Substring(pos, len));
                }
                else
                {
                    sb.Append(text.Substring(pos));
                }
                pos += len;
            }
            return sb.ToString();
        }

        public static string ToTextBlocks(this string text, string indent)
        {
            if (text == null)
                return indent;
            StringBuilder sb = new StringBuilder(text.Length * 2);
            string[] strings = text.Split(new char[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in strings)
            {
                sb.Append(str.ToTextBlock(indent));
                sb.AppendLine();
            }
            sb.Length--;
            return sb.ToString();
        }
    }


}
