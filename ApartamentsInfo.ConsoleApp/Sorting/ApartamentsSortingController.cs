using Common.ConsoleUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartamentsInfo.ConsoleApp.Sorting
{
    public class ApartamentsSortingController
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
                new MenuItem("сортувати за адресою", SetKeySelector,tag:_byAdress),
                new MenuItem("сортувати за власником", SetKeySelector,tag:_byOwner),
                new MenuItem("сортувати за номером квартири", SetKeySelector,tag:_byApartNum),
            };
        }

        void PrepareScreen()
        {
            Console.Clear();
        }

        public ApartamentsSortingController()
        {
            IniMenuItems();
            _driver = new Driver(_menuItems, PrepareScreen)
            {
                OneCommandOnly = true
            };
        }

        Func<Apartament, dynamic> _keySelector = null;
        public IEnumerable<Apartament> Sort(IEnumerable<Apartament> items)
        {
            if(items == null)
            {
                throw new ArgumentNullException("items");
            }
            if(_keySelector == null)
            {
                return items;
            }
            return items.OrderBy(_keySelector);
        }

        void SetKeySelector()
        {
            _keySelector = _driver.Tag as Func<Apartament, dynamic>;
        }

        Func<Apartament, dynamic> _byAdress = e => e.houseNum;
        Func<Apartament, dynamic> _byOwner = e => e.Owner == null ? null : e.Owner;
        Func<Apartament, dynamic> _byApartNum = e => e.apartNum;

    }
}
