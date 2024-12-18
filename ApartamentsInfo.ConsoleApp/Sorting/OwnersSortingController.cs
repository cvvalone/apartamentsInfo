using Common.ConsoleUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartamentsInfo.ConsoleApp.Sorting
{
    public class OwnersSortingController
    {
        Driver _driver;
        public void Run()
        {
            _driver.Run();

        }

        MenuItem[] _menuItems;
        public void IniMenuItems()
        {
            _menuItems = new MenuItem[]
            {
                new MenuItem("відмінити", null),
                new MenuItem("сортувати за ім'ям", SetKeySelector,tag:_byName),
                new MenuItem("сортувати за номером", SetKeySelector,tag:_byNumber),
            };
        }

        void PrepareScreen()
        {
            Console.Clear();
        }

        public OwnersSortingController()
        {
            IniMenuItems();
            _driver = new Driver(_menuItems, PrepareScreen)
            {
                OneCommandOnly = true
            };
        }

        Func<Owner, dynamic> _keySelector = null;
        public IEnumerable<Owner> Sort(IEnumerable<Owner> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }
            if (_keySelector == null)
            {
                return items;
            }
            return items.OrderBy(_keySelector);
        }

        void SetKeySelector()
        {
            _keySelector = _driver.Tag as Func<Owner, dynamic>;
        }

        Func<Owner, dynamic> _byName = e => e.name;
        Func<Owner, dynamic> _byNumber = e => e.PhoneNumber;
    }
}
