using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartamentsInfo
{
    public class Limitation
    {
        public static int HouseNumLength = 20;
        public static int minHouseFloor = 1;
        public static int maxHouseFloor = 9;
        public static int minApartNum = 1;
        public static int maxApartNum = 34;
        public static int minCountOfRooms = 1;
        public static int maxCoutOfRooms = 4;
        public static int maxDescriptionLength = 1023;
        public static int minNameLength = 4;
        public static int maxNameLength = 50;
        public static string PhoneNumRegex = @"^\+\d{12}$";
    }
}
