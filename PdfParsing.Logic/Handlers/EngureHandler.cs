using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfParsing.Logic.Handlers
{
    public class EngureHandler : GeneralHandler
    {
        private static readonly List<string> AttendedStartIndexMark = new List<string>
        {
            "Deputati",
            "Sede piedalas deputati",
            "Sede piedalas",
            //"Ieradusies",
            //"Ieradušies",
            //"Domes deputati",
            //"Sede piedalas Beverinas",
            //"Piedalas domes deputati",
            "Piedalas",
            //"PIEDALAS",
        };
        private static readonly List<string> AttendedEndIndexMark = new List<string>
        {
            "Sede nepiedalas",
            //"Sēdē nepiedalās",
            //"Domes darbinieki",
            //"Sede piedalas",
            //"SEDE PIEDALAS:",
            //"Pasvaldibas darbinieki",
            //"Administracijas darbinieki",
            //"Pašvaldibas darbinieki",
            //"Pasvaldibas administracijas",
            //"Pašvaldibas administracijas",
            //"Sede piedalas administracijas",
            //"Sede piedalas pašvaldibas",
            "Nepiedalas deputati",
            //"Nepiedalas",
            //"Neieradas",
            //"NEPIEDALAS",
        };
        private static readonly List<string> NotAttendedStartIndexMark = new List<string>
        {
            "Sede piedalas:",
            //"Sēdē nepiedalās",
            //"SEDE NEPIEDALAS:"
            //"Sede nepiedalas",
            //"Sēdē nepiedalās",
            //"Nepiedalas",
            //"Neieradas",
            //"NEPIEDALAS",
            "Nepiedalas deputati",
        };
        private static readonly List<string> NotAttendedEndIndexMark = new List<string>
        {
            "Pašvaldibas",
            //"Domes darbinieki",
            "NOTHING"
        };
        private static readonly string[] AttendedSplitOptions = { ", ", "," };
        private static readonly string[] NotAttendedSplitOptions = { ", ", "; ", "," };
        private static readonly string[] NotAttendedInternalSplitOptions = { "-", "–" };
        //private static readonly string[] NotAttendedInternalSplitOptions = { "(", ")" };
        private static readonly Dictionary<string, string> Deputati = new Dictionary<string, string>
        {
            //{ "", "" },
            { "andris kalnozols", "Andris Kalnozols" },
            { "normunds sers", "Normunds Šērs" },
            { "aija rudovska", "Aija Rudovska" },
            { "janis tomels", "Jānis Tomels" },
            { "oskars kambala", "Oskars Kambala" },
            { "atis berzins", "Atis Bērziņš" },
            { "edmunds petersons", "Edmunds Pētersons" },
            { "normunds velps", "Normunds Velps" },
            { "janis rekis", "Jānis Reķis" },
            { "kaspars karklins", "Kaspars Kārkliņš" },
            { "girts krumins", "Ģirts Krūmiņš" },
            { "ģirts krumins", "Ģirts Krūmiņš" },
            { "viesturs osnieks", "Viesturs Ošnieks" },
            { "gatis buraks", "Gatis Buraks" },
            { "gunta lace", "Gunta Lāce" }
        };

        public EngureHandler() : base(
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

        public string Name => "Engure";
        public string Prieksedetajs => "gundars vaza";
        public int DeputatuSkaits => 14;
        public bool AttendedSplit => false;
        public bool NotAttendedSplit => false;
        public bool AttendedNextLine => false;
        public bool NotAttendedNextLine => false;

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
            return s.Replace("– ", string.Empty)
                .Replace("domes priekssedetaja", string.Empty)
                .Replace("priekssedetajas vietnieks", string.Empty)
                .Replace("priekssedetajas vietnieki", string.Empty)
                .Replace("Deputati", string.Empty)
                .Replace("deputati", string.Empty)
                .Replace("domes  ", string.Empty)
                .Replace("14 ", string.Empty)
                .Replace("13 ", string.Empty)
                .Replace("12 ", string.Empty)
                .Replace("11 ", string.Empty)
                .Replace("10 ", string.Empty)
                .Replace("9 ", string.Empty)
                .Replace("8 ", string.Empty)
                .Replace("7 ", string.Empty)
                .Replace("6 ", string.Empty)
                .Replace("5 ", string.Empty)
                .Replace("4 ", string.Empty)
                .Replace("3 ", string.Empty)
                .Replace("2 ", string.Empty)
                .Replace("1 ", string.Empty);
        }

        private static string ReplaceNotAttended(string s)
        {
            return s.Replace("nepiedalas", string.Empty)
                        .Replace(": ", string.Empty)
                        .Replace("priekssedetajas vietnieki", string.Empty)
                        .Replace("Deputati", string.Empty)
                        .Replace("deputati", string.Empty)
                        .Replace("sede  ", string.Empty)
                        .Replace("14 ", string.Empty)
                .Replace("13 ", string.Empty)
                .Replace("12 ", string.Empty)
                .Replace("11 ", string.Empty)
                .Replace("10 ", string.Empty)
                .Replace("9 ", string.Empty)
                .Replace("8 ", string.Empty)
                .Replace("7 ", string.Empty)
                .Replace("6 ", string.Empty)
                .Replace("5 ", string.Empty)
                .Replace("4 ", string.Empty)
                .Replace("3 ", string.Empty)
                .Replace("2 ", string.Empty)
                .Replace("1 ", string.Empty);
        }
    }
}
