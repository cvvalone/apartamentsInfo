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
using System.Threading;

namespace ApartamentsInfo.ConsoleApp.Editing
{
    public class OwnersEditor
    {
        Driver _driver;

        MenuItem[] _menuItems;

        readonly IDataSet _dataSet;

        ICollection<Owner> _collection;

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
                new MenuItem("сортувати...", Sort),
            };
        }

        void PrepareScreen()
        {
            Console.Clear();
            Console.WriteLine(_collection.ToTable());
        }

        public OwnersEditor(IDataSet dataSet)
        {
            if (dataSet == null)
            {
                throw new ArgumentNullException();
            }
            _dataSet = dataSet;
            _collection = dataSet.Owners;
            _sortedObjects = _collection;
            IniMenuItems();
            _driver = new Driver(_menuItems, PrepareScreen);

        }

        bool CollectionIsNotEmpty()
        {
            return _collection.Count != 0;
        }

        private void ShowAsText()
        {
            Console.WriteLine(_collection.ToLineList("Власник"));
        }

        private void ShowObjectsDetails()
        {
            int id = Entering.EnterInt("Введіть Id запису");
            Owner obj = _collection.FirstOrDefault(e => e.Id == id);
            if (obj != null)
            {
                Console.WriteLine(obj);
                return;
            }
            Console.WriteLine($"В списку немає запису з Id рівним {id}");

        }
        private void Add()
        {
            Entering.SetPromptWidth(25);
            Console.WriteLine();
            Owner obj = new Owner();
            obj.name = Entering.EnterString("П.І.Б власника", Limitation.minNameLength, Limitation.maxNameLength);
            Owner IsInCollection = _collection.FirstOrDefault(e => e.name == obj.name);
            if (IsInCollection != null)
            {
                Console.WriteLine($"Запис із іменем {obj.name} вже існує. Спробуйте ще раз!");
                return;
            }
            obj.LegalEnity = Entering.EnterBoolean("Є юридичною особою (Так або Ні)");
            obj.PhoneNumber = Entering.EnterString("Номер", Limitation.PhoneNumRegex, "Введено не вірний формат номеру", RegexOptions.IgnoreCase);
            obj.Note = Entering.EnterString("Примітка");
            obj.Id = _collection.Any() ? _collection.Select(e => e.Id).Max() + 1 : 1;
            _collection.Add(obj);
        }

        private void Remove()
        {
            int id = Entering.EnterInt("Введіть номер запису");
            Owner obj = _collection.FirstOrDefault(e => e.Id == id);
            if(obj != null)
            {
                _collection.Remove(obj);
                return;
            }
            Console.WriteLine($"В списку немає запису з Id рівним {id}");
        }

        public event EventHandler<EventArgs> Saving;
        private void Save()
        {
            if (Saving != null)
            {
                Saving(this, EventArgs.Empty);
            }
        }

        IEnumerable<Owner> _sortedObjects;

        Lazy<OwnersSortingController> _sortingController = new Lazy<OwnersSortingController>();

        void Sort()
        {
            Console.Clear();
            _sortingController.Value.Run();
            _sortedObjects = _sortingController.Value.Sort(_collection);
        }
    }
}
