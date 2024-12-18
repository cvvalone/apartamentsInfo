using Common.ConsoleIO.Interfaces;
using Common.ConsoleUI;
using System;
using System.Collections.Generic;

namespace Common.ConsoleIO
{
    public class FileTypeSelector
    {
        Driver _driver;

        public void Run()
        {
            _driver.Run();
        }

        MenuItem[] _menuItems;
        IFileTypeInformer[] _fileTypeInformers;

        public IFileTypeInformer CurrentInformer { get; private set; }

        private void IniMenuItems()
        {
            List<MenuItem> list = new List<MenuItem>();
            list.Add(new MenuItem("відмінити", null));
            foreach (var el in _fileTypeInformers)
            {
                list.Add(new MenuItem(
                    string.Format("{0} ({1})", el.FileTypeCaption, el.FileExtension),
                    SetCurrentInformer, tag: el));
            }
            _menuItems = list.ToArray();
        }

        private void PrepareScreen()
        {
            Console.Clear();
        }

        public FileTypeSelector(IFileTypeInformer[] fileTypeInformers)
        {
            _fileTypeInformers = fileTypeInformers;
            IniMenuItems();
            _driver = new Driver(_menuItems, PrepareScreen)
            {
                OneCommandOnly = true,
            };
        }

        private void SetCurrentInformer()
        {
            CurrentInformer = _driver.Tag as IFileTypeInformer;
        }
    }
}
