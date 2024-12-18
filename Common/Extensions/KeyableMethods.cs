using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Extensions
{
    public static class KeyableMethods
    {
        public static IEnumerable<string> ToKeys(this IEnumerable<IKeyable> keys)
        {
            foreach (var key in keys)
            {
                yield return key.Key;
            }
        }

        public static string ToKeysHierarchy<T>(this T obj) where T : IHierarchical<T>
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(obj.Key);
            T parent = obj.Parent;
            while (parent != null)
            {
                sb.AppendFormat($">- {parent.Key}");
                parent = parent.Parent;
            }
            return sb.ToString();
        }

        public static T FindFirstOrDefault<T>(this IEnumerable<T> objects, Func<T, bool> selector)
        {
            IEnumerator<T> enumerator = objects.GetEnumerator();
            while(enumerator.MoveNext())
            {
                T item = enumerator.Current;
                if(selector(item))
                {
                    return item;
                }
            }
            return default(T);
        }
    }
}
