using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ConsoleUI.Events
{
    public class SelectEventArgs<T> : EventArgs
    {
        public readonly T Value;
        public readonly bool Selected;
        public readonly string Key;
        public readonly int ItemIndex;

        public SelectEventArgs(T value, bool selected, string key, int itemIndex)
        {
            Value = value;
            Selected = selected;
            Key = key;
            ItemIndex = itemIndex;
        }


    }
}
