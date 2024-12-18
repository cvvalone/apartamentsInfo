using ApartamentsInfo.Data.Interfaces;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ApartamentsInfo.Data.IO
{
    public class BinnaryFileIoController : IFileIoController
    {
        public string FileExtension
        {
            get { return ".bin"; }
        }

        public string FileTypeCaption
        {
            get { return "Файл формату Bin"; }
        }

        public bool Load(IDataSet dataSet, string filePath)
        {
            filePath = Path.ChangeExtension(filePath, FileExtension);
            if (!File.Exists(filePath))
            {
                return false;
            }
            DataSet newDataSet = null;
            BinaryFormatter formatter = new BinaryFormatter();
            
            using(FileStream fStream = File.OpenRead(filePath))
            {
                newDataSet = (DataSet)formatter.Deserialize(fStream);
            }
            if(newDataSet == null)
            {
                return false;
            }
            foreach(Owner el in newDataSet.Owners)
            {
                dataSet.Owners.Add(el);
            }
            foreach(Apartament el in newDataSet.Apartaments)
            {
                dataSet.Apartaments.Add(el);
            }
            return true;
        }

        public void Save(IDataSet dataSet, string filePath)
        {
            filePath = Path.ChangeExtension(filePath, FileExtension);
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (FileStream fStream = File.OpenWrite(filePath))
            {
                binaryFormatter.Serialize(fStream, dataSet);
            }
        }
    }
}
