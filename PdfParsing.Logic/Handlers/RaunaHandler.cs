using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfParsing.Logic.Handlers
{
    public class RaunaHandler : GeneralHandler
    {
        private static readonly List<string> AttendedStartIndexMark = new List<string>
        {
            "Sēdē piedalās",
            "Sede piedalas",
            //"Ieradusies",
            //"Ieradušies",
        };
        private static readonly List<string> AttendedEndIndexMark = new List<string>
        {
            "Sēdē nepiedalās",
            "Sede nepiedalas"
        };
        private static readonly List<string> NotAttendedStartIndexMark = new List<string>
        {
            "Sēdē nepiedalās",
            "Sede nepiedalas"
        };
        private static readonly List<string> NotAttendedEndIndexMark = new List<string>
        {
            "Darba"
        };
        private static readonly string[] AttendedSplitOptions = { ", ", "," };
        private static readonly string[] NotAttendedSplitOptions = { ", ", "; ", "," };
        private static readonly string[] NotAttendedInternalSplitOptions = { "-", "–" };
        private static readonly Dictionary<string, string> Deputati = new Dictionary<string, string>
        {
            { "andris neimanis", "Andris Neimanis" },
            { "andris abramovs", "Andris Abrāmovs" },
            { "aivars damroze", "Aivars Damroze" },
            { "anita lubuze", "Anita Lubūze" },
            { "plijeva", "Dace Sarkane-Plijeva" },
            { "dace sarkane", "Dace Sarkane-Plijeva" },
            { "uldis kalnins", "Uldis Kalniņš" },
            { "alda raiskuma", "Alda Raiskuma" },
            { "ilmars vecgailis", "Ilmārs Vecgailis" },
            { "aleksandrs katisevs", "Aleksandrs Katiševs"}
        };

        public RaunaHandler() : base(
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
        
        public List<string> CleanAttended(List<string> attended, string prieksedetajs)
        {
            var newAttended = new List<string>();

            foreach (var item in attended)
            {
                var splitted = item.Split(new[] { ":", "-" }, StringSplitOptions.RemoveEmptyEntries);

                var toAdd = splitted.Length > 1
                    ? ReplaceAttended(Normalize(splitted[1]))
                    : ReplaceAttended(Normalize(item));

                    newAttended.Add(toAdd);
            }

            return newAttended;

        }

        public Dictionary<string, string> CleanNotAttended(Dictionary<string, string> notAttended)
        {
            var newNotAttended = new Dictionary<string, string>();

            foreach (var item in notAttended)
            {
                newNotAttended.Add(
                    ReplaceNotAttended(item.Key),
                    item.Value);
            }

            return newNotAttended;
        }

        private static string ReplaceAttended(string s)
        {
            return s.Replace(".", string.Empty)
                .Replace("domes priekssedetaja", string.Empty)
                .Replace("priekssedetajas vietnieks", string.Empty)
                .Replace("priekssedetajas vietnieki", string.Empty)
                .Replace("Deputati", string.Empty)
                .Replace("deputati", string.Empty)
                .Replace("domes  ", string.Empty);
        }

        private static string ReplaceNotAttended(string s)
        {
            return s.Replace("domes priekssedetaja", string.Empty)
                        .Replace("priekssedetajas vietnieks", string.Empty)
                        .Replace("priekssedetajas vietnieki", string.Empty)
                        .Replace("Deputati", string.Empty)
                        .Replace("deputati", string.Empty)
                        .Replace("sede nepiedalas:", string.Empty);
        }
    }
}
