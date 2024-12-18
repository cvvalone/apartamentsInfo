using System.Collections.Generic;

namespace ApartamentsInfo.Data.Interfaces
{
    public interface IDataSet
    {
        ICollection<Apartament> Apartaments { get; }
        ICollection<Owner> Owners { get; }
        bool IsEmpty();
    }
}
