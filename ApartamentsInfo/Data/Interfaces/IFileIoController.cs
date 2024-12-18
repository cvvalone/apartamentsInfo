using Common.ConsoleIO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartamentsInfo.Data.Interfaces
{
    public interface IFileIoController : IFileTypeInformer
    {
        //string FileExtension {  get; }
        void Save(IDataSet dataSet, string filePath);
        bool Load(IDataSet dataSet, string filePath);

    }
}
