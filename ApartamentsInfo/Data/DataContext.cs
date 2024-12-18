using ApartamentsInfo.Data.Interfaces;
using ApartamentsInfo.Data.Extensions;
using System.Collections.Generic;


namespace ApartamentsInfo.Data
{
    public partial class DataContext : IDataSet
    {
        readonly IDataSet _dataSet = new DataSet();

        public ICollection<Apartament> Apartaments
        {
            get { return _dataSet.Apartaments; }
        }

        public ICollection<Owner> Owners
        {
            get { return _dataSet.Owners; }
        }

        public bool IsEmpty()
        {
            return _dataSet.IsEmpty();
        }
        public bool CreateTestingData()
        {
            return _dataSet.CreateTestingData();
        }

        public override string ToString()
        {
            return _dataSet.ToString();
        }

        public void Clear()
        {
            Apartaments.Clear();
            Owners.Clear();
        }

        public static explicit operator DataSet(DataContext context)
        {
            return new DataSet(context.Apartaments, context.Owners);
        }
    }
}
