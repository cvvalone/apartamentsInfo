using Common.ConsoleUI.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ConsoleUI.Controls
{
    public class InteractiveListBox<T> : MultiselectListBox<T>
    {
        public InteractiveListBox(IEnumerable<T> values, Func<T, string> keySelector = null) : base(values, keySelector)
        {
        }

        public virtual void SetSelected(IEnumerable<T> selectedValue)
        {
            foreach (var item in _items)
            {
                item.Selected = selectedValue.Contains(item.Value);
            }
        }

        public EventHandler<SelectEventArgs<T>> Selected;

        protected virtual void OnSelected(SelectEventArgs<T> eventArgs)
        {
            if(Selected != null)
            {
                Selected.Invoke(this, eventArgs);
            }
        }

        protected override bool HandlePressAndComplete(ConsoleKey key)
        {
            switch(key)
            {
                case ConsoleKey.Spacebar:
                    InvertItemSelected();
                    OnSelected(new SelectEventArgs<T>(CurrentItem.Value, CurrentItem.Selected, CurrentItem.Key, SelectionIndex));
                    Show();
                    break;
                default:
                    return base.HandlePressAndComplete(key);
            }
            return false;
        }

        protected override void ShowKey(int itemIndex)
        {
            base.ShowKey(itemIndex);
            Console.ForegroundColor = TextColor;
        }
    }
}
