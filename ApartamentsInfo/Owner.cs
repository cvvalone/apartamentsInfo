using Common.Entities;
using Common.Interfaces;

namespace ApartamentsInfo
{
    [System.SerializableAttribute]
    public class Owner : Entity, IHierarchical<Owner>
    {
        public string name { get; set; }
        public bool? LegalEnity { get; set; }
        public string PhoneNumber { get; set; }
        public string Note { get; set; }

        public override string Key
        {
            get { return name; }
        }

        public Owner Parent { get; }

        public override string ToMembersString()
        {
            return string.Format(
                $"\n{Indent}Власник: {name}\n" +
                $"{Indent}Є юридичною особою: {LegalEnity}\n" +
                $"{Indent}Номер: {PhoneNumber}\n" +
                $"{Indent}Примітка: {Note}\n"
            );
        }

        public Owner(string owner, bool legalEnity, string phoneNumber, string note)
        {
            name = owner;
            LegalEnity = legalEnity;
            PhoneNumber = phoneNumber;
            Note = note;
        }

        public Owner(Owner owner) { }

        public Owner(string name, bool legalEnity) : this(name, legalEnity, null, null)
        {
            
        }

        public Owner() { }
    }
}
