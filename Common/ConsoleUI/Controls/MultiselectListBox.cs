using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ConsoleUI.Controls
{

    public enum SelectionMode { None, OnePerSession, SeveralPerSession }
    public class MultiselectListBox<T> : ListBox<T>
    {
        public MultiselectListBox(IEnumerable<T> values, Func<T, string> keySelector = null) : base(values, keySelector)
        {
            Width += 3;
            SelectionMode = SelectionMode.SeveralPerSession;
        }

        protected override void ShowKey(int itemIndex)
        {
            bool selected = _items[itemIndex].Selected;
            Console.Write($" {(selected ? '√' : ' ')} ");
            base.ShowKey(itemIndex);
        }

        protected void InvertItemSelected()
        {
            CurrentItem.Selected = !CurrentItem.Selected;
        }

        protected override bool HandlePressAndComplete(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Spacebar:
                    if (SelectionMode == SelectionMode.None)
                        break;
                    InvertItemSelected();
                    Show();
                    if(SelectionMode == SelectionMode.OnePerSession) 
                        return true;
                    break;
                case ConsoleKey.Enter:
                    if (SelectionMode == SelectionMode.OnePerSession)
                        goto case ConsoleKey.Spacebar;
                    return true;
                default:
                    return base.HandlePressAndComplete(key);
            }
            return false;
        }

        public IEnumerable<T> SelectedValues
        {
            get { return _items.Where(e => e.Selected).Select(e => e.Value); }
        }

        public SelectionMode SelectionMode { get; set; }

        public IEnumerable<int> SelectedIndexes
        {
            get
            {
                for(int i = 0; i < _items.Length; i++)
                {
                    if (_items[i].Selected)
                    {
                        yield return i;
                    }
                }
            }
        }
    }
}
