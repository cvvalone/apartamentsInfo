using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ConsoleUI.Selecting
{
    public delegate void Filter<T>(ref IEnumerable<T> objects);
    public class FilterInfo<T> : IKeyable
    {
        public string Name { get; private set; }
        public Filter<T> Filter { get; private set; }
        public Action EnterData { get; private set; }
        public Action DisplayData { get; private set; }
        public bool Selected {  get; set; }

        public FilterInfo(string name, Filter<T> filter, Action enterData = null, Action displayData = null)
        {
            this.Name = name;
            this.Filter = filter;
            this.EnterData = enterData;
            this.Selected = false;
            this.DisplayData = displayData;

        }

        public string Key { get { return Name; } }
    }
}
