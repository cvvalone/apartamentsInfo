using ApartamentsInfo.Data.Extensions;
using ApartamentsInfo.Formatting;
using Common.ConsoleIO;
using Common.ConsoleUI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartamentsInfo.ConsoleApp.Selecting
{
    public static class SelectingMethods
    {

        public static Owner Select(this IEnumerable<Owner> objects)
        {
            Console.Write(Entering.Format, "Власники");
            IEnumerable<Owner> values= objects.OrderBy(e => e.Key);
            ListBox<Owner> listBox = new ListBox<Owner>(values);
            listBox.SetPostition(Console.CursorLeft, Console.CursorTop);
            listBox.Focus();
            return listBox.SelectionValue;
        }

        public static FormattingMode SelectFormattingMode()
        {
            Console.WriteLine(" Вибір способів відображення даних\n");
            MultiselectListBox<string> listBox = new MultiselectListBox<string>(FormattingModesInfo.Headers);
            listBox.SetPostition(Console.CursorLeft, Console.CursorTop);
            listBox.Focus();
            FormattingMode mode = FormattingMode.None;
            foreach(var i in listBox.SelectedIndexes)
            {
                mode |= (FormattingMode)(1 << i);
            }
            return mode;
        }
    }
}
