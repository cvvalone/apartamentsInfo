using ApartamentsInfo.Data.Interfaces;
using Common.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApartamentsInfo.Data.Extensions
{
    public static class FormattingMethods
    {
        public static string ToDataString(this IDataSet dataSet, string header = null)
        {
            if(header == null)
            {
                header = "ПО \"Інформація про квартири\"";
            }
            return string.Concat(header, ":\n",
                dataSet.Apartaments.ToLineList(" Квартири"), "\n",
                dataSet.Owners.ToLineList(" Власники"));
        }

        public static string ToStatisticString(this IDataSet dataSet, string header = null)
        {
            if (header == null) header = "Статистичні дані про об'єкти ПО";
            return string.Format($"{header}\n" + 
                " Представлено:\n" + 
                $"{dataSet.Owners.Count(),7} власників\n" +
                $"{dataSet.Apartaments.Count(),7} квартир"
                );
        }

        public static string ToTable(this IEnumerable<Owner> objects, string header = null)
        {
            if (header == null) header = "Власники";
            StringBuilder sb = new StringBuilder();
            sb.Append(header);
            sb.AppendLine();
            if(!objects.Any())
            {
                sb.AppendLine("\t(дані відсутні)");
                return sb.ToString();
            }
            string format = (" {0,5} {1,-29} {2,-20} {3,12}\n");
            sb.AppendFormat(format, "Id", "Власник", "Є юридичною особою", "Номер");
            sb.AppendFormat($" {new string('-', 70)}\n");
            foreach (var obj in objects)
            {
                sb.AppendFormat(format,
                    obj.Id,
                    obj.name,
                    obj.LegalEnity,
                    obj.PhoneNumber
                    );
            }
            sb.Length--;
            return sb.ToString();
        }

        public static string ToTable(this IEnumerable<Apartament> objects, string header = null)
        {
            if (header == null) header = "Квартири";
            StringBuilder sb = new StringBuilder();
            sb.Append(header);
            sb.AppendLine();
            if (!objects.Any())
            {
                sb.AppendLine("\t(дані відсутні)");
                return sb.ToString();
            }
            string format = (" {0,5} {1,-29} {2,-20} {3,12}\n");
            sb.AppendFormat(format, "Id", "Адреса", "Поверх", "Квартира", "Кімнат", "Власник");
            sb.AppendFormat($" {new string('-', 70)}\n");
            foreach (var obj in objects)
            {
                sb.AppendFormat(format,
                    obj.Id,
                    obj.houseNum,
                    obj.houseFloor,
                    obj.apartNum,
                    obj.numOfRooms,
                    obj.Owner
                    );
            }
            sb.Length--;
            return sb.ToString();
        }

        public static string ToOwnersTable(this IDataSet dataSet, string header = null)
        {
            return dataSet.Owners.ToTable(header);
        }

        public static string ToApartamentsTable(this IDataSet dataSet, string header = null)
        {
            return dataSet.Apartaments.ToTable(header);
        }

        
    }
}
