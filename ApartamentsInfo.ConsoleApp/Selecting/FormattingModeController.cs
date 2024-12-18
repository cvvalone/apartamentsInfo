using ApartamentsInfo.Formatting;
using Common.ConsoleUI.Controls;
using Common.ConsoleUI.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ApartamentsInfo.ConsoleApp.Selecting
{
    public class FormattingModeController
    {
        public FormattingMode FormattingMode { get; private set; }

        InteractiveListBox<FormattingMode> _listBox;

        private void CreateLisBox()
        {
            _listBox = new InteractiveListBox<FormattingMode>(
                FormattingModesInfo.Values.Skip(1),keySelector: e => e.ToHeader());
            _listBox.SetSelected(FormattingMode.ToFlags());
            _listBox.Selected += _listBox_Selected;
        }

        private void _listBox_Selected(object sender, SelectEventArgs<FormattingMode> e)
        {
            FormattingMode flag = e.Value;
            if (e.Selected)
            {
                FormattingMode |= flag;
            }
            else
            {
                FormattingMode &= ~flag;
            }
            OnFormattingModeChanged(EventArgs.Empty);
        }

        public FormattingModeController(FormattingMode formattingMode = FormattingMode.None)
        {
            FormattingMode = formattingMode;
            CreateLisBox();
        }

        public void ShowHeader()
        {
            Console.WriteLine(" Вибір способів відображення даних\n");
        }

        int _cursorLeft;
        int _cursorTop;

        private void RememberCurrentPosition()
        {
            _cursorLeft = Console.CursorLeft;
            _cursorTop = Console.CursorTop;
        }

        private void CalculateFormattingMode()
        {
            FormattingMode = _listBox.SelectedIndexes.Aggregate(FormattingMode.None, (result, next) => result |= (FormattingMode)(1 << next));
        }

        public FormattingMode Select()
        {
            ShowHeader();
            RememberCurrentPosition();
            _listBox.SetPostition(_cursorLeft, _cursorTop);
            _listBox.Focus();
            //CalculateFormattingMode();
            return FormattingMode;
        }

        public event EventHandler<EventArgs> FormattingModeChanged;

        protected virtual void OnFormattingModeChanged(EventArgs e)
        {
            if(FormattingModeChanged != null)
            {
                FormattingModeChanged(this, e);
            }
        }

        public void ShowExample(string example)
        {
            Console.Clear();
            ShowHeader();
            _listBox.Show();
            Console.SetCursorPosition(0, _listBox.Top + _listBox.Height + 2);
            Console.WriteLine(example);
            Console.SetCursorPosition(_cursorLeft, _cursorTop);
        }
    }

    
}
