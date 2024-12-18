using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ConsoleUI.Controls
{
    public class ListItem <T>
    {
        public string Key { get; set; }
        public T Value { get; set; }
        public ListItem(string key, T value)
        {
            Key = key;
            Value = value;
        }

        public bool Selected {  get; set; }
    }
}
