using Common.ConsoleIO;
using Common.ConsoleUI.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Common.ConsoleUI
{
    public class Driver
    {
        MenuItem[] _menuItems;
        Action _prepareScreen;
        Action _prepareRunning;
        public Driver(MenuItem[] menuItems, Action prepareScreen, Action prepareRunning = null) 
        {
            _menuItems = menuItems;
            _prepareScreen = prepareScreen;
            _prepareRunning = prepareRunning;
        }

        //MenuItem SelectMenuItem()
        //{
        //    ShowMenu();
        //    int number = Entering.EnterInt("\n Введіть номер команди з меню", 0, _menuItems.Length - 1);
        //    return _menuItems[number];
        //}

        //void ShowMenu() {
        //    Console.WriteLine("\n Список команд меню:");
        //    for (int i = 0; i < _menuItems.Length; i++) {
        //        if (_menuItems[i].Displayed == null || _menuItems[i].Displayed()) {
        //            Console.WriteLine("\t{0,2} - {1}", i, _menuItems[i].CommandName);
        //        }
        //    }
        //}
        MenuItem SelectMenuItem()
        {
            Console.WriteLine("\nСписок команд меню:");
            IEnumerable<string> commandName = _menuItems.Where(e => e.Displayed == null || e.Displayed()).Select(e => e.CommandName);
            ListBox<string> listBox = new ListBox<string>(commandName);
            listBox.SetPostition(Console.CursorLeft + 2, Console.CursorTop);
            listBox.Focus();
            return _menuItems.First(e => e.CommandName == listBox.SelectionValue);
        }

        public static void StopToView()
        {
            Console.WriteLine("\n\tДля продовження натисніть довільну клавішу...");
            Console.ReadKey(true);
        }

        public void Run()
        {
            if(_prepareRunning != null )
            {
                _prepareRunning();
            }
            while(true)
            {
                Console.Clear();
                _prepareScreen();
                MenuItem menuItem = SelectMenuItem();
                Tag = menuItem.Tag;
                if(menuItem.Operation == null)
                {
                    return;
                }
                try
                {
                    menuItem.Operation();
                    if (menuItem.Stopping)
                    {
                        StopToView();
                    }
                    if (OneCommandOnly)
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    StopToView();
                }
            }
        }

        public bool OneCommandOnly {  get; set; }
        public object Tag {  get; set; }

    }
    
    
}
