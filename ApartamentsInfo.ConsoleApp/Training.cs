using ApartamentsInfo.ConsoleApp.Selecting;
using ApartamentsInfo.Data;
using ApartamentsInfo.Data.Extensions;
using ApartamentsInfo.Data.Interfaces;
using ApartamentsInfo.Data.IO;
using ApartamentsInfo.Formatting;
using Common.ConsoleUI.Controls;
using Common.Entities;
using Common.Extensions;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;


namespace ApartamentsInfo.ConsoleApp
{
    public static class Training
    {

        internal static void Run()
        {
            Console.WriteLine(" Дослідження роботи програмних засобів");

            //StudyEntities();
            //StudyIDataSet();
            //StudyKeyablesMethods();
            //StudyTestingData();
            //StudyLinq();
            //StudyIterators();
            //StudyFileIo();
            //StudyContextIo();
            //StudyXmlFileIo();
            //StudySelecting();
            //StudyMultiselecting();
            //StudyFiltering();
            //StudyFormattingMethods();
            StudyFormattingModesInfo();

            Console.ReadKey(true);
        }

        static void StudyEntities()
        {
            Console.WriteLine("---StudyEntities---");

            Owner obj1 = new Owner();
            Console.WriteLine($"obj1.Key: {obj1.Key}");
            Console.WriteLine($"obj.ToMembersString: {obj1.ToMembersString()}");

            obj1.name = "Приймук Павло Сергійович";
            obj1.LegalEnity = true;
            obj1.PhoneNumber = "+380680030775";

            Console.WriteLine(new string('-', Console.BufferWidth - 1));
            Console.WriteLine($"obj1.Id: {obj1.Id}");
            obj1.Id = 1;
            Console.WriteLine($"obj1.Id: {obj1.Id}");

            Entity ent = obj1;
            Console.WriteLine($"ent.Id: {ent.Id}");
            Console.WriteLine($"((Entity)obj1).Id:{((Entity)obj1).Id}");

            Console.WriteLine($"ent.ToMembersString:{ent.ToMembersString()}");

            Console.WriteLine($"obj1.ToString:{obj1.ToString()}");
            Console.WriteLine($"obj1:{obj1}");

            Console.WriteLine($"obj1.ToString:{ent.ToString()}");
            Console.WriteLine($"ent:{ent}");
            Console.WriteLine($"obj:{obj1}");

            Console.WriteLine(new string('=', Console.BufferWidth - 1));

            Owner obj2 = new Owner("Мартинович Назар Олександрович", true, "+380991732114", "Гарний ріелтор, має чималі зв'язки на ринку") { Id = 2 };
            Console.WriteLine($"obj2:{obj2}");

            Owner obj3 = new Owner("Табашнюк Антон Тарасович", false) { Id = 3 };
            Console.WriteLine($"obj3: {obj3}");

            List<Entity> entities = new List<Entity>()
            {
                obj1, obj2, obj3, new Owner("Антонюк Олександра Олександрівна", true) { Id = 4 },
            };

            Console.WriteLine("entities:");

            foreach (Entity el in entities)
            {
                Console.WriteLine($"\t{el.Key}");
            }

            ent = entities[entities.Count - 1];

            Console.WriteLine($"ent: {ent}");
        }

        static void StudyIDataSet()
        {
            Console.WriteLine(" --- StudyIDataSet --- ");
            DataSet dataSet = new DataSet();
            Console.WriteLine(dataSet.ToDataString("dataSet"));
            CreateTestingData(dataSet);
            Console.WriteLine("dataSet:\n" + dataSet.ToDataString());
            Console.WriteLine(new string('-', Console.BufferWidth - 1));
            DataContext dataContext = new DataContext();
            Console.WriteLine("dataContext:\n" + dataContext.ToDataString());
            CreateTestingData(dataContext);
            Console.WriteLine("dataContext:\n" + dataContext.ToDataString());

        }

        static void CreateTestingData(IDataSet dataSet)
        {
            Owner obj1 = new Owner("Василенко Іван Іванович", false, "+380632355253", "") { Id = 1 };
            dataSet.Owners.Add(obj1);
            Owner obj2 = new Owner("Стець Андрій Михайлович", true, "+380660352314", "") { Id = 2 };
            dataSet.Owners.Add(obj2);
            Owner obj3 = new Owner("Крисак Іван Андрійович", false, "+380991273116", "") { Id = 3 };
            dataSet.Owners.Add(obj3);

            dataSet.Apartaments.Add(new Apartament("Василя Жеврука 31", 2, 8, 3, obj3) { Id = 1 });
        }

        static void StudyKeyablesMethods()
        {
            Console.WriteLine(" ---StudyKeyableMethods--- ");

            DataSet dataSet = new DataSet();
            CreateTestingData(dataSet);
            IEnumerable<string> keys1 = dataSet.Owners.ToKeys();
            Console.WriteLine("key1s:\t");
            foreach (var key in keys1)
            {
                Console.WriteLine($"{key}, ");
            }
            Console.WriteLine("\b\b");

            Console.WriteLine(dataSet.Apartaments.ToKeys().ToLine("ApartamentsKeys"));
            Console.WriteLine(dataSet.Owners.ToKeys().ToLine("OwnersKeys"));

            List<IKeyable> keyables = new List<IKeyable>(dataSet.Owners);
            keyables.AddRange(dataSet.Apartaments);
            Console.WriteLine(keyables.ToKeys().ToLine("keyableKeys"));

            Console.WriteLine(new string('-', Console.BufferWidth - 1));
            Console.WriteLine("Ієрархію ключів:");
            foreach (var obj in dataSet.Owners)
            {
                Console.WriteLine("\t" + obj.ToKeysHierarchy());
            }
        }

        static void StudyTestingData()
        {
            Console.WriteLine("---StudyTestingData---");
            List<Owner> list1 = new List<Owner>();
            list1.CreateTestingOwners();
            Console.WriteLine(list1.ToLineList("list1"));

            Console.WriteLine(new string('-', Console.BufferWidth - 1));

            DataContext dataContext = new DataContext();
            dataContext.CreateTestingData();
            Console.WriteLine($"dataContext:\n{dataContext.ToDataString()}");
        }

        static void StudyLinq()
        {
            Console.WriteLine("---StudyLinq---");
            DataContext dataContext = new DataContext();
            dataContext.CreateTestingData();

            IEnumerable<Owner> objects = dataContext.Owners.OrderBy(e => e.Key);
            objects.OutKeyList("objects");

            var Notes = objects.Where(e => !string.IsNullOrEmpty(e.Note)).Select(e => e.Note).OrderBy(e => e);

            Notes.OutLine("Notes");

            var Notes2 = from e in objects
                         where !string.IsNullOrEmpty(e.Note)
                         orderby e.Note
                         select e.Note;
            Notes2.OutLine("Notes2");

            var ids = objects.Select(e => e.Id).OrderByDescending(e => e);
            ids.OutLine("ids");

            var first = objects.First();
            Console.WriteLine($"first:\t{first.Key}");

            var secondTwo = objects.Skip(2).Take(2);
            secondTwo.OutLine("secondTwo");

            var secondTwoArray = secondTwo.ToArray();
            secondTwoArray.OutLine("secondTwoArray");

            Console.WriteLine(new string('-', Console.BufferWidth - 1));
            dataContext.Owners.Add(new Owner("Людмила Василівна Петренко", false) { Id = 6 });
            objects.OutKeyList("objects");
        }

        static void StudyIterators()
        {
            DataContext dataContext = new DataContext();
            dataContext.CreateTestingData();
            IEnumerable<Owner> objects = dataContext.Owners;
            objects.OutKeyList("objects");
            var res1 = objects.ToKeys().FindFirstOrDefault(e => e.Contains("Пет"));
            Console.WriteLine($"res1:\t{res1 ?? "(?)"}");
            var res2 = objects.ToKeys().FindFirstOrDefault(e => e.Contains("Авг"));
            Console.WriteLine($"res2:\t{res2 ?? "(?)"}");
        }

        static void StudyFileIo()
        {
            Console.WriteLine("---StudyFileIo---");
            DataSet dataSet1 = new DataSet();
            dataSet1.CreateTestingData();
            Console.WriteLine($"dataSet1:\n" + dataSet1.ToDataString());

            string fileName = "DataSetInfo";
            BinnaryFileIoController binController = new BinnaryFileIoController();
            binController.Save(dataSet1, fileName);

            Console.WriteLine(new string('-', Console.BufferWidth - 1));
            DataSet dataSet2 = new DataSet();
            bool res2 = binController.Load(dataSet2, fileName);
            Console.WriteLine($"res2:{res2}");
            Console.WriteLine($"dataSet2:\n{dataSet2.ToDataString()}");

            DataSet dataSet3 = new DataSet();
            bool res3 = binController.Load(dataSet3, "ErrFileName");
            Console.WriteLine($"res3:{res3}");
            Console.WriteLine($"dataSet3:\n{dataSet3.ToDataString()}");
        }

        static void StudyContextIo()
        {
            Console.WriteLine("---StudyContextIo---");
            DataContext dataContext = new DataContext();
            dataContext.CreateTestingData();
            Console.WriteLine("dataContext:\n" + dataContext.ToDataString());
            BinnaryFileIoController binController = new BinnaryFileIoController();

            string fileName1 = "DataContextInfo1";
            DataSet dataSet1 = (DataSet)dataContext;
            binController.Save(dataSet1, fileName1);

            Console.WriteLine(new string('-', Console.BufferWidth - 1));
            dataContext.FileIoController = binController;
            Console.WriteLine($"dataContext.FilePath:\n\t" + dataContext.FilePath);
            dataContext.Save();

            dataContext.Clear();
            Console.WriteLine($"dataContext:{dataContext.ToDataString()}");
            dataContext.Load();
            Console.WriteLine($"dataContext:{dataContext.ToDataString()}");

        }

        static void StudyXmlFileIo()
        {
            Console.WriteLine("---StudyXmlFileIo---");

            string directoryName = @"..\..\files";

            DataContext dataContext = new DataContext(directoryName);

            XmlFileIoController xmlController = new XmlFileIoController();

            dataContext.FileIoController = xmlController;
            Console.WriteLine("dataContext.FilePath:\n\t" + dataContext.FilePath);

            dataContext.CreateTestingData();
            Console.WriteLine("dataContext:\n\t" + dataContext.ToDataString());
            dataContext.Save();

            Console.WriteLine(new string('-', Console.BufferWidth - 1));

            dataContext.Clear();
            Console.WriteLine($"dataContext:\n{dataContext.ToDataString()}");

            dataContext.Load();
            Console.WriteLine($"dataContext:\n{dataContext.ToDataString()}");

        }

        static void StudySelecting()
        {
            Console.WriteLine("---StudySelecting---");
            //int[] arr1 = { 33, 111, 2 };
            //ListBox<int> lb1 = new ListBox<int> (arr1);
            //Console.WriteLine("Select: ");
            //lb1.SetPostition(Console.CursorLeft, Console.CursorTop);
            //lb1.Focus();

            //int sel1 = lb1.SelectionValue;
            //Console.WriteLine($"sel1:\t{sel1}");

            int[] arr2 = { 88, 66, 7 };
            ListBox<int> lb2 = new ListBox<int>(arr2);
            Console.WriteLine("Select: ");
            lb2.SetPostition(Console.CursorLeft, Console.CursorTop);
            lb2.Focus();

            int sel2 = lb2.SelectionValue;
            Console.WriteLine($"sel2:\t{sel2}");

            Console.WriteLine(new string('-', Console.BufferWidth - 1));

            DataContext dataContext = new DataContext();
            dataContext.CreateTestingData();


            ListBox<Owner> lb3 = new ListBox<Owner>(dataContext.Owners);
            Console.Write("Select: ");

            lb3.SetPostition(Console.CursorLeft, Console.CursorTop);
            lb3.Focus();

            Owner sel3 = lb3.SelectionValue;
            Console.WriteLine($"sel3: {sel3}");

            Console.WriteLine(new string('=', Console.BufferWidth - 1));

            ListBox<Apartament> lb4 = new ListBox<Apartament>(dataContext.Apartaments, e => string.Format($"{e.Key} - {e.Owner.Key}"));
            Console.Write("Select: ");
            lb4.SetPostition(Console.CursorLeft, Console.CursorTop);
            lb4.Focus();

            Apartament sel4 = lb4.SelectionValue;
            Console.WriteLine($"sel4: {sel4}");
        }

        static void StudyMultiselecting()
        {
            Console.WriteLine("---StudyMultiselecting---");

            int[] arr1 = { 33, 111, 2 };
            MultiselectListBox<int> lb1 = new MultiselectListBox<int>(arr1);
            Console.WriteLine("Multiselect:");
            lb1.SetPostition(Console.CursorLeft, Console.CursorTop);
            lb1.Focus();

            IEnumerable<int> selected1 = lb1.SelectedValues;
            Console.WriteLine(selected1.ToLineList("\nselected1:"));

        }

        static void StudyFiltering()
        {
            DataContext dataContext = new DataContext();
            dataContext.CreateTestingData();
            IEnumerable<Apartament> objects = dataContext.Apartaments.OrderBy(e => e.Key);
            Console.WriteLine(objects.ToTable());
            ApartamentsFilteringController controller = new ApartamentsFilteringController();
            controller.SelectFilter();
            IEnumerable<Apartament> selectedObjects = controller.ApplyFilters(objects);
            Console.WriteLine();
            Console.WriteLine(selectedObjects.ToTable());
        }

        static void StudyFormattingMethods()
        {
            Console.WriteLine("---StudyFormattingMethods---");
            DataSet dataSet = new DataSet();
            dataSet.CreateTestingData();
            Console.WriteLine(dataSet.ToDataString());
            Console.WriteLine(dataSet.ToStatisticString());

            Console.WriteLine(new string('-', Console.BufferWidth - 1));
            Console.WriteLine(dataSet.ToOwnersTable());
            Console.WriteLine(dataSet.ToApartamentsTable());
        }

        static void StudyFormattingModesInfo()
        {
            Console.WriteLine("---StudyFormattingModesInfo---");
            Console.WriteLine($"Count:\t{FormattingModesInfo.Count}");
            Console.WriteLine(FormattingModesInfo.Names.ToLineList("Names", "\t"));
            Console.WriteLine("AllFlagsNumber:\t{0} = 0x{1:X} = {2}b",
                FormattingModesInfo.AllFlagsNumber,
                FormattingModesInfo.AllFlagsNumber,
                Convert.ToString(FormattingModesInfo.AllFlagsNumber, 2));
            Console.WriteLine(((FormattingMode)FormattingModesInfo.AllFlagsNumber).ToFlags().ToLineList("AllFlags", "\t"));
            Console.WriteLine(FormattingModesInfo.Headers.ToLineList("Headers", "\t"));
            Console.WriteLine(FormattingModesInfo.Values.ToLineList("Values", "\t"));
            string a = "Стат. дані про об'єкти ПО \"Інформація про квартири\"";
            FormattingModesInfo.SetHeader(FormattingModesInfo._headers, FormattingMode.Statistic, a);
            Console.WriteLine(FormattingModesInfo.Headers.ToLineList("Headers", "\t"));
        }
    }
}
