using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Common.ConsoleUI.Controls
{
    public class ListBox<T>
    {
        public int Left { get; private set; }
        public int Top { get; private set; }
        public int Width { get; protected set; }
        public int Height { get; private set; }

        public int Bottom
        {
            get { return Top + Height - 1;}
        }

        public void SetPostition(int left, int top)
        {
            Left = left;
            Top = top;
        }

        protected ListItem<T>[] _items;

        protected virtual ListItem<T>[] CreateItems(IEnumerable<T> values, Func<T, string> keySelector = null)
        {
            if(keySelector != null)
            {
                return values.Select(e => new ListItem<T>(keySelector(e), e)).ToArray();
            }
            T value = values.FirstOrDefault(e => e != null);
            if(value is IKeyable)
            {
                return values.Select(e => new ListItem<T>(((IKeyable)e).Key, e)).ToArray();
            }
            return values.Select(e => new ListItem<T>(e.ToString(), e)).ToArray();
        }

        public ListBox(IEnumerable<T> values, Func<T, string> keySelector = null)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            _items = CreateItems(values, keySelector);
            Width = _items.Select(x => x.Key.Length).Max();
            Height = _items.Length;
        }

        public static ConsoleColor TextColor = ConsoleColor.DarkGray;
        public static ConsoleColor SelectionColor = ConsoleColor.Green;

        public int SelectionIndex { get; private set; }
        public virtual void Show()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                Console.SetCursorPosition(Left, Top + i);
                ShowKey(i);
            }
            Visible = true;
        }

        protected virtual void ShowKey(int itemIndex)
        {
            Console.ForegroundColor = itemIndex == SelectionIndex ? SelectionColor : TextColor;
            Console.Write(_items[itemIndex].Key);
        }

        protected ConsoleColor _fgColor;

        public void Focus()
        {
            _fgColor = Console.ForegroundColor;
            Console.CursorVisible = false;
            Show();
            HandleKeyPress();
            Console.ForegroundColor = _fgColor;
            Console.CursorVisible = true;
        }

        protected void Hide()
        {
            string str = new string(' ', Width);
            for (int i = 0; i < _items.Length; i++)
            {
                Console.SetCursorPosition(Left, Top + i);
                Console.Write(str);
            }
            Visible = false;
        }

        protected ListItem<T> CurrentItem
        {
            get { return _items[SelectionIndex]; }
        }

        public string SelectionKey
        {
            get { return CurrentItem.Key; }
        }

        private void ShowSelectionKey()
        {
            Console.ForegroundColor = _fgColor;
            Console.SetCursorPosition(Left, Top);
            Console.WriteLine(SelectionKey);
        }

        protected virtual bool HandlePressAndComplete(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Spacebar:
                case ConsoleKey.Enter:
                    Hide();
                    ShowSelectionKey();
                    return true;
                case ConsoleKey.UpArrow:
                    SelectionIndex = (SelectionIndex - 1) % _items.Length;
                    Show();
                    break;
                case ConsoleKey.DownArrow:
                    SelectionIndex = (SelectionIndex + 1) % _items.Length;
                    Show();
                    break;
            }
            return false;
        }

        private void HandleKeyPress()
        {
            while (true)
            {
                Thread.Sleep(0);
                if (!Console.KeyAvailable)
                {
                    continue;
                }
                var cki = Console.ReadKey(true);
                if (cki.Modifiers != 0)
                {
                    continue;
                }
                if (HandlePressAndComplete(cki.Key))
                    return;
            }
        }

        public T SelectionValue {
            get { return CurrentItem.Value; }
        }

        public virtual bool Visible { get; protected set; }

    }
}
