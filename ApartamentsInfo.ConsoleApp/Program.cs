using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApartamentsInfo.Data;
using ApartamentsInfo.Data.Interfaces;
using ApartamentsInfo.Data.IO;
using Common.ConsoleIO;
using Common.ConsoleIO.Interfaces;
using Common.ConsoleUI;

namespace ApartamentsInfo.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CtrlEventHandling.SetConsoleCtrlHandler(Handler, true);
            Settings.SetConsoleParam("ApartamentsInfo.ConsoleApp (Приймук П.С.)");
            Console.WriteLine("Використання об'єкто-орієнтованого підходу");



            RunProgram();
            //Training.Run();
        }

        static MainController _mainController = null;
        static DataContext _dataContext = null;
        //static IFileIoController _fileIoController = null;
        static XmlFileIoController _xmlFileIoController = new XmlFileIoController();
        static BinnaryFileIoController _binnaryFileIoController = new BinnaryFileIoController();
        static IFileTypeInformer[] _fileTypeInformers = new IFileTypeInformer[]
        {
            _binnaryFileIoController, _xmlFileIoController
        };
        static FileTypeSelector _fileTypeSelector = new FileTypeSelector(_fileTypeInformers);

        public static object ConfigurationUserlevel { get; private set; }
        private static void RunProgram()
        {
            _dataContext = new DataContext();
            //_fileIoController = new BinnaryFileIoController();
            //_fileIoController = new XmlFileIoController();
            //_dataContext.FileIoController = _fileIoController;
            _dataContext.FileIoController = GetFileIoController();
            _dataContext.DirectoryName = @"..\..\files";
            _mainController = new MainController(_dataContext); //
            _mainController.FileTypeSelector = _fileTypeSelector;
            _mainController.Run();
            SaveConfiguration();
        }

        private static void SaveConfiguration()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);
            config.AppSettings.Settings["FileExtension"].Value =
                _dataContext.FileIoController.FileExtension;
            //config.Save(ConfigurationSaveMode.Full, true);
            config.Save(ConfigurationSaveMode.Modified);
        }
        private static IFileIoController GetFileIoController()
        {
            string fileExt = ConfigurationManager.AppSettings.Get("FileExtension");
            if (fileExt == null)
            {
                fileExt = ".bin";
            }
            return _fileTypeInformers.First(e => e.FileExtension == fileExt)
                as IFileIoController;
        }
        private static bool Handler(CtrlType signal)
        {
            switch (signal)
            {
                case CtrlType.CTRL_BREAK_EVENT:
                case CtrlType.CTRL_C_EVENT:
                case CtrlType.CTRL_LOGOFF_EVENT:
                case CtrlType.CTRL_SHUTDOWN_EVENT:
                case CtrlType.CTRL_CLOSE_EVENT:
                    Console.WriteLine("\nЗавершення роботи програми");
                    Environment.Exit(0);
                    return false;
                default:
                    return false;
            }
        }
    }
}
