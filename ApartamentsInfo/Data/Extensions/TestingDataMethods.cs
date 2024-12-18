using ApartamentsInfo.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ApartamentsInfo.Data.Extensions
{
    public static class TestingDataMethods
    {

        public static void CreateTestingOwners(this ICollection<Owner> owners)
        {
            owners.Add(new Owner("Петренко Василь Олександрович", false, "+380662353245", "Доволі відома людині у сфері ріелтингу, має велику кількість продажів за спиною.") { Id = 1 });
            owners.Add(new Owner("Василенко Ірина Петрівна", true, "+380662353245", "Рієлторша з великою кількістю продажів упродовж багатьох років досвіду.") { Id = 2 });
            owners.Add(new Owner("Шевченко Наталя Олегівна", true, "++380671234567", "Наталя Шевченко — досвідчена рієлторка з багаторічним стажем у сфері нерухомості. Її професійний підхід і відповідальність зробили її одним із найуспішніших агентів у місті. Наталя завжди готова надати кваліфіковану допомогу у виборі найкращого житла або інвестиційного об'єкту.") { Id = 3 });
            owners.Add(new Owner("Олексій Михайлович Сидоров", true, "+380998765432", "Олексій Михайлович — відомий серед місцевих мешканців рієлтор з бездоганною репутацією. Завдяки своєму досвіду та відмінним комунікативним навичкам, він завжди знаходить ідеальне рішення для кожного клієнта. Олександр відомий своєю вмінням знаходити вигідні угоди та ефективно вирішувати будь-які питання, пов'язані з нерухомістю.") { Id = 4 });
            owners.Add(new Owner("Петров Олександр Іванович", true, "+380665432109", "Олександр Іванович - професіонал у справі нерухомості з бездоганною репутацією та теплим ставленням до клієнтів. Завдяки своєму досвіду та знанням ринку вона знаходить ідеальні варіанти для кожного клієнта.") { Id = 5 });
        }
        public static bool CreateTestingData(this IDataSet dataSet)
        {
            if (dataSet.IsEmpty())
            {
                CreateTestingOwners(dataSet.Owners);
                CreateTestingApartaments(dataSet);
                return true;
            }
            return false;
        }
        private static void CreateTestingApartaments(IDataSet dataSet)
        {
            dataSet.Apartaments.Add(new Apartament("Соборна 10", 5, 22, 2, dataSet.Owners.First(e => e.name == "Петренко Василь Олександрович")) { Id = 1 });
            dataSet.Apartaments.Add(new Apartament("Грушевського 5", 3, 12, 3, dataSet.Owners.First(e => e.name == "Шевченко Наталя Олегівна")) { Id = 2 });
            dataSet.Apartaments.Add(new Apartament("Пушкіна 18", 7, 28, 1, dataSet.Owners.First(e => e.name == "Василенко Ірина Петрівна")) { Id = 3 });
            dataSet.Apartaments.Add(new Apartament("Лермонтова 22", 1, 4, 4, dataSet.Owners.First(e => e.name == "Петров Олександр Іванович")) { Id = 4 });
        }
        

        
    }
}
