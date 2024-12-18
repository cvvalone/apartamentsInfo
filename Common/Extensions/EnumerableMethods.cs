using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class EnumerableMethods
    {

        public static string ToLineList<T>(this IEnumerable<T> objects, string prompt, string indent = "    ")
        {
            return string.Concat(prompt, ":\n", indent, string.Join(string.Format($"\n{indent}"), objects));
        }

        public static string ToLine<T>(this IEnumerable<T> objects, string prompt)
        {
            return string.Format($"{prompt}:\t {string.Join(",", objects)}");
        }
    }

}
