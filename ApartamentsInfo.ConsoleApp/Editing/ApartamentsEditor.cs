using ApartamentsInfo.ConsoleApp.Selecting;
using ApartamentsInfo.ConsoleApp.Sorting;
using ApartamentsInfo.Data.Extensions;
using ApartamentsInfo.Data.Interfaces;
using Common.ConsoleIO;
using Common.ConsoleUI;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ApartamentsInfo.ConsoleApp.Editing
{
    public class ApartamentsEditor
    {
        Driver _driver;

        MenuItem[] _menuItems;

        readonly IDataSet _dataSet;

        ICollection<Apartament> _collection;

        public void Run()
        {
            _driver.Run();
        }

        void IniMenuItems()
        {
            _menuItems = new MenuItem[]
            {
                new MenuItem("назад", null),
                new MenuItem("дані як текст", ShowAsText, CollectionIsNotEmpty, true),
                new MenuItem("детально про квартиру...", ShowObjectsDetails, CollectionIsNotEmpty, true),
                new MenuItem("додати запис", Add, stopping: true),
                new MenuItem("видалити запис", Remove, CollectionIsNotEmpty),
                new MenuItem("зберегти дані", Save, CollectionIsNotEmpty, stopping: true),
                new MenuItem("увімкнути/вимкнути фільтри ►", CheckUncheckFilter, CollectionIsNotEmpty),
                new MenuItem("сортувати ... ►", Sort),
            };
        }

        void PrepareScreen()
        {
            Console.Clear();
            ApplyFilters();
            OutTable();
        }

        private void OutTable()
        {
            Console.WriteLine(_selectedObjects.ToTable());
        }

        public ApartamentsEditor(IDataSet dataSet)
        {
            if(dataSet == null)
            {
                throw new ArgumentNullException();
            }
            _dataSet = dataSet;
            _collection = dataSet.Apartaments;
            _sortedObjects = _collection;
            _selectedObjects = _sortedObjects;
            IniMenuItems();
            _driver = new Driver(_menuItems, PrepareScreen);
        }
        bool CollectionIsNotEmpty()
        {
            return _collection.Count != 0;
        }

        private void ShowAsText()
        {
            Console.WriteLine(_selectedObjects.ToLineList("Квартири"));
        }

        private void ShowObjectsDetails()
        {
            int id = Entering.EnterInt("Введіть Id запису");
            Apartament obj = _collection.FirstOrDefault(e => e.Id == id);
            if(obj != null)
            {
                Console.WriteLine(obj);
                return;
            }
            Console.WriteLine($"В списку немає запису з Id рівним {id}");
        }
        
        private Owner SelectOwner()
        {
            return _dataSet.Owners.Select();
        }
        private void Add()
        {
            Entering.SetPromptWidth(25);
            Console.WriteLine();
            Apartament obj = new Apartament();
            obj.Owner = SelectOwner();
            obj.houseNum = Entering.EnterString("Адреса", 1, Limitation.HouseNumLength);
            obj.apartNum = Entering.EnterInt("Номер квартири", Limitation.minApartNum, Limitation.maxApartNum);
            Apartament IsInCollection = _collection.FirstOrDefault(e => e.houseNum == obj.houseNum && e.apartNum == obj.apartNum);
            if(IsInCollection != null)
            {
                Console.WriteLine($"\tЗапис з адерсою: \"{obj.houseNum}\" і квартирою номер {obj.apartNum} вже існує.\n\t" +
                    $"Додавання однакових записів неможливе.");
                return;
            }
            obj.houseFloor = Entering.EnterInt("Поверх", Limitation.minHouseFloor, Limitation.maxHouseFloor);
            obj.numOfRooms = Entering.EnterInt("Кількість кімнат", Limitation.minCountOfRooms, Limitation.maxCoutOfRooms);
            obj.Description = Entering.EnterString("Опис");
            obj.Id = _collection.Any() ? _collection.Select(e => e.Id).Max() + 1 : 1;
            _collection.Add(obj);
        }

        private void Remove()
        {
            int id = Entering.EnterInt("Введіть номер запису");
            Apartament obj = _collection.FirstOrDefault(e => e.Id == id);
            if (obj != null)
            {
                _collection.Remove(obj);
                return;
            }
            Console.WriteLine($"В списку немає запису з Id рівним {id}");
        }

        public event EventHandler<EventArgs> Saving;

        private void Save()
        {
            if(Saving != null)
            {
                Saving(this, EventArgs.Empty);
            }
        }

        IEnumerable<Apartament> _selectedObjects;
        IEnumerable<Apartament> _sortedObjects;

        Lazy<ApartamentsSortingController> _sortingController = new Lazy<ApartamentsSortingController>();

        void Sort()
        {
            Console.Clear();
            _sortingController.Value.Run();
            _sortedObjects = _sortingController.Value.Sort(_collection);
        }

        ApartamentsFilteringController _filteringController;

        private void CheckUncheckFilter()
        {
            if(_filteringController == null)
            {
                _filteringController = new ApartamentsFilteringController();
                _filteringController.OwnerSelector = SelectOwner;
            }
            _filteringController.SelectFilter();
        }

        private void ApplyFilters()
        {
            if(_filteringController == null)
            {
                _selectedObjects = _sortedObjects;
            }
            else
            {
                _selectedObjects = _filteringController.ApplyFilters(_sortedObjects);
            }
        }
    }
}
