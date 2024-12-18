using ApartamentsInfo.ConsoleApp.Editing;
using ApartamentsInfo.ConsoleApp.Selecting;
using ApartamentsInfo.Data;
using ApartamentsInfo.Data.Extensions;
using ApartamentsInfo.Data.Interfaces;
using ApartamentsInfo.Formatting;
using Common.ConsoleIO;
using Common.ConsoleUI;
using System;

namespace ApartamentsInfo.ConsoleApp
{
    class MainController
    {
        Driver _driver;

        public void Run()
        {
            _driver.Run();
        }

        MenuItem[] _menuItems;

        private void IniMenuItems()
        {
            _menuItems = new MenuItem[]
            {
                new MenuItem("вийти", null),
                new MenuItem("створити тестові дані", CreateTestingData, DataSetIsEmpty),
                new MenuItem("видалити усі дані", Clear, DataSetIsNotEmpty),
                new MenuItem("змінити формат збереження даних ►", ChangeIoFormat),
                new MenuItem("зберегти дані", Save, stopping: true),
                new MenuItem("редагувати дані про квартири ►", RunApartamentsEditing),
                new MenuItem("редагувати дані про власників ►", RunOwnersEditing),
                new MenuItem("вибір способів відображення ►", SelectFormattingMode),

            };

        }

        private void PrepareScreen()
        {
            Console.Clear();
            Console.WriteLine(_dataSet.ToStatisticString());
            Console.WriteLine("ПО \"Інформація про квартири\"\n");
            OutObjectsInfo();
        }

        private void PrepareRunning()
        {
            if (_dataContext.Load())
            {
                //Console.WriteLine("Дані завантажено");
                Console.WriteLine($"Дані завантажено з файлу формату \"{_dataContext.FileIoController.FileTypeCaption} ({_dataContext.FileIoController.FileExtension})\"");
            }
            else
            {
                Console.WriteLine("Файл з даними відсутній");
            }
            Driver.StopToView();
        }

        public MainController(DataContext dataContext)
        {
            IniMenuItems();
            _driver = new Driver(_menuItems, PrepareScreen, PrepareRunning);
            if (dataContext == null)
            {
                throw new ArgumentNullException("dataContext");
            }
            _dataContext = dataContext;
            _dataSet = (DataSet)_dataContext;
            CreateEditors();
        }

        readonly DataContext _dataContext;
        readonly IDataSet _dataSet;

        bool DataSetIsEmpty() { return  _dataSet.IsEmpty(); }
        bool DataSetIsNotEmpty() { return !DataSetIsEmpty(); }

        private void CreateTestingData()
        {
            if (_dataSet.IsEmpty())
            {
                _dataContext.CreateTestingData();
            }
            else
            {
                Console.WriteLine("\nТестові дані не створені, оскільки сховище порожнє.");
                Driver.StopToView();
            }
        }

        private void ShowAsText()
        {
            Console.WriteLine();    
            Console.WriteLine(_dataContext.ToDataString());
        }

        private void Clear()
        {
            _dataContext.Clear();
        }

        private void Save()
        {
            _dataContext.Save();
            Console.WriteLine($"Дані збережено у форматі \"{_dataContext.FileIoController.FileTypeCaption} ({_dataContext.FileIoController.FileExtension})\"");
        }

        private ApartamentsEditor _apartamentsEditor;
        private OwnersEditor _ownersEditor;
        private void CreateEditors()
        {
            _apartamentsEditor = new ApartamentsEditor(_dataSet);
            _apartamentsEditor.Saving += Editor_Saving;
            _ownersEditor = new OwnersEditor(_dataSet);
            _ownersEditor.Saving += Editor_Saving;
        }

        private void Editor_Saving(object sender, EventArgs e)
        {
            Save();
        }
        private void RunApartamentsEditing() { _apartamentsEditor.Run(); }

        private void RunOwnersEditing() { _ownersEditor.Run(); }

        public FileTypeSelector FileTypeSelector { get; set; }

        private void ChangeIoFormat()
        {
            FileTypeSelector.Run();
            _dataContext.FileIoController = FileTypeSelector.CurrentInformer as IFileIoController;
        }

        FormattingMode _formattingMode = FormattingMode.Statistic | FormattingMode.ApartamentsTable | FormattingMode.OwnersTable;

        private void OutObjectsInfo()
        {
            int _left = Console.CursorLeft;
            int _top = Console.CursorTop;
            Console.SetCursorPosition(0, _top + _menuItems.Length + 3);
            Console.WriteLine("--------------Дані--------------\n");
            Console.WriteLine(DataFormatting.Format(_dataSet, _formattingMode));
            Console.SetCursorPosition(_left, _top);
        }

        FormattingModeController _formattingModeController;

        private void SelectFormattingMode()
        {
            Console.Clear();
            //_formattingMode = SelectingMethods.SelectFormattingMode();
            if(_formattingModeController == null)
            {
                _formattingModeController = new FormattingModeController(_formattingMode);
                _formattingModeController.FormattingModeChanged += _formattingModeController_FormattingModeChanged;
            }
            _formattingMode = _formattingModeController.Select();
        }

        private void _formattingModeController_FormattingModeChanged(object sender, EventArgs e)
        {
            _formattingModeController.ShowExample(
                DataFormatting.Format(_dataSet, _formattingModeController.FormattingMode));
        }


    }
}
