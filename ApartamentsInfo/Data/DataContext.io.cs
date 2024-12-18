using ApartamentsInfo.Data.Interfaces;
using System.IO;

namespace ApartamentsInfo.Data
{
    partial class DataContext
    {
        private string _directoryName = "";
        public string DirectoryName
        {
            get { return _directoryName; }
            set
            {
                _directoryName = (value ?? "").Trim();
                if (!string.IsNullOrEmpty(_directoryName) && !Directory.Exists(_directoryName))
                {
                    Directory.CreateDirectory(_directoryName);
                }
            }
        }
        public string FileName { get; set; }
        public DataContext(string directoryName)
        {
            DirectoryName = directoryName;
            FileName = "ApartamentsInfo";
        }
        public DataContext() : this("") { }
        public IFileIoController FileIoController { get; set; }
        public string FilePath
        {
            get
            {
                return Path.Combine(DirectoryName, FileName + FileIoController.FileExtension);
            }
        }

        public void Save()
        {
            FileIoController.Save(_dataSet, FilePath);
        }

        public bool Load()
        {
            return FileIoController.Load(_dataSet, FilePath);
        }

    }
}
