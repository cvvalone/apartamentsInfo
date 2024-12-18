using ApartamentsInfo.Data.Interfaces;
using System.Collections.Generic;


namespace ApartamentsInfo.Data
{
    [System.SerializableAttribute]
    public class DataSet : IDataSet
    {

        private readonly ICollection<Apartament> _apartaments;
        private readonly ICollection<Owner> _owners;

        public DataSet(ICollection<Apartament>apartaments, ICollection<Owner>owners)
        {
            this._apartaments = apartaments;
            this._owners = owners;
        }

        public DataSet() : this(new List<Apartament>(), new List<Owner>()) { }

        public ICollection<Apartament> Apartaments
        {
            get { return _apartaments; }
        }

        public ICollection<Owner> Owners
        {
            get { return _owners; }
        }

        public bool IsEmpty()
        {
            return Apartaments.Count == 0 && Owners.Count == 0;
        }
    }
}
