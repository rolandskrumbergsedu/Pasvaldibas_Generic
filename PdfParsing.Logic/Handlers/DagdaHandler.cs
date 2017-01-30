using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfParsing.Logic.Handlers
{
    public class DagdaHandler : GeneralHandler
    {
        private static readonly List<string> AttendedStartIndexMark = new List<string>
        {
            "Sede piedalas",
            "Sēdē piedalās",
            //"SEDE PIEDALAS",
            //" Sede piedalas",
            //"  Sede piedalas",
            //"Deputati",
            //"Sede piedalas deputati",
            //"Ieradusies",
            //"Ieradušies",
            //"Domes deputati",
            //"Sede piedalas Beverinas",
            //"Piedalas deputati",
            "Piedalas",
            " Piedalas",
            "  Piedalas",
            //"PIEDALAS",
        };
        private static readonly List<string> AttendedEndIndexMark = new List<string>
        {
            "Sede nepiedalas",
            "Sēdē nepiedalās",
            //"Domes darbinieki",
            //"Sede piedalas",
            //"SEDE PIEDALAS:",
            //"Pasvaldibas darbinieki",
            "Administracijas darbinieki",
            "Pašvaldibas darbinieki",
            //"Pasvaldibas administracijas",
            //"Pašvaldibas administracijas",
            //"Sede piedalas administracijas",
            //"Sede piedalas pašvaldibas",
            //"Nepiedalas deputati",
            //"Nepiedalas",
            //"Neieradas",
            //"NEPIEDALAS",
        };
        private static readonly List<string> NotAttendedStartIndexMark = new List<string>
        {
            //"Sede nepiedalas",
            //"Sēdē nepiedalās",
            //"SEDE NEPIEDALAS:"
            "Nepiedalas",
            //"Neieradas",
            //"NEPIEDALAS",
            //"Nepiedalas deputati",
        };
        private static readonly List<string> NotAttendedEndIndexMark = new List<string>
        {
            //"Darba",
            //"Domes darbinieki",
            "NOTHING"
        };
        private static readonly string[] AttendedSplitOptions = { ", ", "," };
        private static readonly string[] NotAttendedSplitOptions = { ", ", "; ", "," };
        //private static readonly string[] NotAttendedInternalSplitOptions = { "-", "–" };
        private static readonly string[] NotAttendedInternalSplitOptions = { "(", ")" };
        private static readonly Dictionary<string, string> Deputati = new Dictionary<string, string>
        {
            //{ "", "" },
            { "aivars arnicans", "Aivars Arnicāns" },
            { "aivars platacis", "Aivars Platacis" },
            { "aivars plotka", "Aivars Plotka" },
            { "aleksandrs gzibovskis", "Aleksandrs Gžibovskis" },
            { "anatols viskurs", "Anatols Viškurs" },
            { "inese plesna", "Inese Plesņa" },
            { "irena gzibovska", "Irēna Gžibovska" },
            { "maija levkova", "Maija Ļevkova" },
            { "olga golube", "Olga Golube" },
            { "raimonds nipers", "Raimonds Nipers" },
            { "raitis azins", "Raitis Azins" },
            { "viktors krumins", "Viktors Krūmiņš" },
            { "viktors stikuts", "Viktors Stikuts" },
            { "vladislavs visnevskis", "Vladislavs Višņevskis" },
        };

        public DagdaHandler() : base(
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

        public string Name => "Dagda";
        public string Prieksedetajs => "sandra viškure";
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
                        .Replace("nepiedalas deputats ", string.Empty)
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
