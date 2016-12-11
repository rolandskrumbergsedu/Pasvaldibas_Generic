using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfParsing.Logic.Handlers
{
    public class AglonaHandler : GeneralHandler

    {
        private static readonly List<string> AttendedStartIndexMark = new List<string> { "Piedalas " };
        private static readonly List<string> AttendedEndIndexMark = new List<string> { "Nepiedalas " };
        private static readonly List<string> NotAttendedStartIndexMark = new List<string> { "Nepiedalas " };
        private static readonly List<string> NotAttendedEndIndexMark = new List<string> { "Pašvaldibas" };
        private static readonly string[] AttendedSplitOptions = { ", ", "," };
        private static readonly string[] NotAttendedSplitOptions = { ", ", "; ", "," };
        private static readonly string[] NotAttendedInternalSplitOptions = { "(", ")" };
        private static readonly Dictionary<string, string> Deputati = new Dictionary<string, string>
        {
            { "helena streike", "Helēna Streiķe" },
            { "osvalds satilovs", "Osvalds Šatilovs" },
            { "lolita solima", "Lolita Solima" },
            { "andris girss", "Andris Girss" },
            { "aleksandrs dimpers", "Aleksandrs Dimpers" },
            { "juris butevics", "Juris Butēvics" },
            { "aivars rivars", "Aivars Rivars" },
            { "igors rescenko", "Igors Reščenko" },
            { "vadims krimans", "Vadims Krimans" },
            { "feoktists pusnakovs", "Feoktists Pušņakovs" }
        };

        public AglonaHandler() : base(
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
                var splitted = item.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
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
                var newItem = Normalize(item.Key.Replace("nepiedalas (attaisnotu iemeslu del): ", string.Empty)
                        .Replace(".", string.Empty));

                newNotAttended.Add(
                    item.Key.Replace("nepiedalas (attaisnotu iemeslu del): ", string.Empty)
                        .Replace(".", string.Empty),
                    item.Value);
            }

            return newNotAttended;
        }
    }
}
