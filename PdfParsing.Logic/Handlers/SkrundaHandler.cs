using System;
using System.Collections.Generic;
using System.Linq;

namespace PdfParsing.Logic.Handlers
{
    public class SkrundaHandler : GeneralHandler
    {
        private static readonly List<string> AttendedStartIndexMark = new List<string> { "1 Piedalas", "Piedalas", "Piedalās" };
        private static readonly List<string> AttendedEndIndexMark = new List<string> { "Nepiedalas", "Nepiedalās", "Sede nepiedalas", "Sēdē nepiedalās" };
        private static readonly List<string> NotAttendedStartIndexMark = new List<string> { "Nepiedalas", "Nepiedalās" };
        private static readonly List<string> NotAttendedEndIndexMark = new List<string> { "Klausas", "Klausās" };
        private static readonly string[] AttendedSplitOptions = { "piedalas", "piedalās", ", ", "," };
        private static readonly string[] NotAttendedSplitOptions = { "nepiedalas", "nepiedalās", ", ", "; ", "," };
        private static readonly string[] NotAttendedInternalSplitOptions = { "(", ")" };
        private static readonly Dictionary<string, string> Deputati = new Dictionary<string, string>
        {
            { "aldis balodis", "Aldis Balodis" },
            { "ivo bars", "Ivo Bārs" },
            { "juris jaunzems", "Juris Jaunzems" },
            { "zigurds purins", "Zigurds Puriņš" },
            { "andris vilnis sadovskis", "Andris Vilnis Sadovskis" },
            { "aivars sebezs", "Aivars Sebežs" },
            { "aldis zalgauckis", "Aldis Zalgauckis" },
            { "ainars zankovskis", "Ainārs Zankovskis" },
            { "gunars zeme", "Gunārs Zeme" },
            { "andris bergmanis", "Andris Bergmanis" },
            { "ivars grundmanis", "Ivars Grundmanis" },
            { "ainars pileckis", "Ainars Piļeckis" },
            { "gunta stepanova", "Gunta Stepanova" },
            { "loreta robezniece", "Loreta Robežniece" },
            { "loreta robežniece", "Loreta Robežniece" }
        };

        public SkrundaHandler() : base(
            AttendedStartIndexMark, 
            AttendedEndIndexMark,
            NotAttendedStartIndexMark,
            NotAttendedEndIndexMark,
            AttendedSplitOptions,
            NotAttendedSplitOptions,
            NotAttendedInternalSplitOptions,
            Deputati)
        {

        }


        public List<string> CleanAttended(List<string> attended)
        {
            var newAttended = new List<string>();

            foreach (var item in attended)
            {
                var splitted = item.Split(new[] { ":", "-" }, StringSplitOptions.RemoveEmptyEntries);
                if (splitted.Count() > 1)
                {
                    newAttended.Add(Normalize(splitted[1]));
                }
                else
                {
                    newAttended.Add(Normalize(item));
                }
            }

            return newAttended;

        }

        public Dictionary<string, string> CleanNotAttended(Dictionary<string, string> notAttended)
        {
            var newNotAttended = new Dictionary<string, string>();

            foreach (var item in notAttended)
            {
                newNotAttended.Add(
                    item.Key
                        .Replace(".", string.Empty)
                        .Replace("Deputati:", string.Empty)
                        .Replace("deputati:  ", string.Empty)
                        .Replace("deputati: ", string.Empty)
                        .Replace("nepiedalas: ", string.Empty),
                    item.Value);
            }

            return newNotAttended;
        }
    }
}
