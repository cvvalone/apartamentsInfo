using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartamentsInfo.ConsoleApp
{
    public static class DataIo
    {
        public static void OutKeyList(this IEnumerable<Entity> objects, string prompt)
        {
            Console.WriteLine($"{prompt}:\n\t{string.Join("\n\t", objects.Select(e => e.Key))}");
        }

        public static void OutLine<T>(this IEnumerable<T> objects, string prompt)
        {
            Console.WriteLine($"{prompt}:\t{string.Join(",",objects)}");
        }
    }
}
