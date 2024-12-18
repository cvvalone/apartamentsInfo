using Common.ConsoleIO;
using Common.ConsoleUI.Selecting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApartamentsInfo.ConsoleApp.Selecting
{
    class ApartamentsFilteringController : FilteringController<Apartament>
    {
        protected override void IniFiltersInfo(out FilterInfo<Apartament>[] filtersInfo)
        {
            filtersInfo = new FilterInfo<Apartament>[]
            {
                new FilterInfo<Apartament>("відмінити вибір фільтрів", null),
                new FilterInfo<Apartament>("не вказаний опис", HasDescription),
                new FilterInfo<Apartament>("назва починається з ...", AdressStartWith, EnterAdressStart, () => Console.Write(adressStart)),
                new FilterInfo<Apartament>("опис містить ...", DescriptionContatins, EnterDescriptonSubString, () => Console.Write(DescriptionSubString)),
                new FilterInfo<Apartament>("кількість комнат в діапазоні ...", CountOfRoomsInRange, EnterCountOfRooms, () => Console.Write($"від {minCountOfRooms} до {maxCountOfRooms}")),
                new FilterInfo<Apartament>("власник ...", LocatedIn, SelectOwner, () => Console.Write(owner == null ? "" : owner.Key)),

            };
        }

        private void HasDescription(ref IEnumerable<Apartament> objects)
        {
            objects = objects.Where(e => string.IsNullOrEmpty(e.Description));
        }

        string adressStart = "";
        
        private void EnterAdressStart()
        {
            adressStart = Entering.EnterString("Початок адреси");
        }
        private void AdressStartWith(ref IEnumerable<Apartament> objects)
        {
            if (adressStart == null)
            {
                return;
            }
            objects = objects.Where(e => e.houseNum.StartsWith(adressStart, StringComparison.InvariantCultureIgnoreCase));
        }

        string DescriptionSubString = "";

        private void EnterDescriptonSubString()
        {
            DescriptionSubString = Entering.EnterString("Фрагмент опису");
        }

        private void DescriptionContatins(ref IEnumerable<Apartament> objects)
        {
            if(DescriptionSubString == null)
            {
                return;
            }
            objects = objects.Where(e => e.Description.IndexOf(DescriptionSubString, StringComparison.InvariantCultureIgnoreCase) >= 0);
        }

        int? minCountOfRooms;
        int? maxCountOfRooms;

        private void EnterCountOfRooms()
        {
            minCountOfRooms = Entering.EnterNullableInt32("Мінімальна к-сть кімнат");
            maxCountOfRooms = Entering.EnterNullableInt32("Максимальна к-сть кімнат");
        }

        private void CountOfRoomsInRange(ref IEnumerable<Apartament> objects)
        {
            if(minCountOfRooms.HasValue)
            {
                objects = objects.Where(e => e.numOfRooms >= minCountOfRooms.Value);
            }
            if (maxCountOfRooms.HasValue)
            {
                objects = objects.Where(e => e.numOfRooms >=  maxCountOfRooms.Value);
            }
        }

        Owner owner;

        public Func<Owner> OwnerSelector;

        private void SelectOwner()
        {
            if (OwnerSelector != null)
            {
                owner = OwnerSelector();
            }
        }

        private void LocatedIn(ref IEnumerable<Apartament> objects)
        {
            objects = objects.Where(e => e.Owner == owner);
        }

    }
}
