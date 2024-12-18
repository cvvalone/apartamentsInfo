using Common.Entities;

namespace ApartamentsInfo
{
    [System.SerializableAttribute]
    public class Apartament : Entity
    {
        public string houseNum {  get; set; }
        public int? houseFloor {  get; set; }
        public int? apartNum { get; set; }
        public int? numOfRooms { get; set; }
        public string Description { get; set; }
        public Owner Owner { get; set; }

        public override string Key
        {
            get { return $"{houseNum} квартира {apartNum}"; }
        }

        public Apartament(string HouseNum, int? HouseFloor, int? ApartNum, int? NumOfRooms, Owner owner)
        {
            houseNum = HouseNum;
            houseFloor = HouseFloor;
            apartNum = ApartNum;
            numOfRooms = NumOfRooms;
            Owner = owner;
        }

        public Apartament(string HouseNum, int? HouseFloor, int ApartNum) 
            : this(HouseNum, HouseFloor, ApartNum, null, null) { }

        public Apartament() : this (null, null, null, null, null ) { }

        public override string ToMembersString()
        {
            return string.Format(
                $"{Indent}Адреса: {houseNum}" +
                $"{Indent}Поверх: {houseFloor}" +
                $"{Indent}Номер квартири: {apartNum}" +
                $"{Indent}Кількість кімнат: {numOfRooms}" +
                $"{Indent}Опис: {Description}" +
                $"{Indent}Власник: {Owner.Key}"
            );
        }
    }
}
