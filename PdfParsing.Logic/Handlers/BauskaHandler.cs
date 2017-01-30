using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfParsing.Logic.Handlers
{
    public class BauskaHandler : GeneralHandler
    {
        private static readonly List<string> AttendedStartIndexMark = new List<string>
        {
            "Sede piedalas",
            "SEDE PIEDALAS",
            //" Sede piedalas",
            //"  Sede piedalas",
            //"Deputati",
            //"Ieradusies",
            //"Ieradušies",
            //"Domes deputati",
            //"Pavilostas novada domes deputati",
            //"Piedalas deputati",
            "Piedalas",
        };
        private static readonly List<string> AttendedEndIndexMark = new List<string>
        {
            //"Sede nepiedalas",
            //"Domes darbinieki",
            "Sede piedalas",
            "SEDE PIEDALAS:",
            //"Pasvaldibas darbinieki",
            //"Pašvaldibas darbinieki",
            //"Pasvaldibas administracijas",
            //"Pašvaldibas administracijas",
            //"Amatas novada pašvaldibas izpilddirektors",
            //"Nepiedalas deputati",
            "Nepiedalas",
        };
        private static readonly List<string> NotAttendedStartIndexMark = new List<string>
        {
            "Sede nepiedalas",
            "SEDE NEPIEDALAS:"
            //"Nepiedalas",
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
        private static readonly string[] NotAttendedInternalSplitOptions = { "-", "–" };
        //private static readonly string[] NotAttendedInternalSplitOptions = { "(", ")" };
        private static readonly Dictionary<string, string> Deputati = new Dictionary<string, string>
        {
            //{ "", "" },
            { "voldemars cacs", "Voldemārs Čačs" },
            { "rudite cakane", "Rudīte Cakāne" },
            { "alvis feldmanis", "Alvis Feldmanis" },
            { "vera grigorjeva", "Vera Grigorjeva" },
            { "daira jatniece", "Daira Jātniece" },
            { "arnolds jatnieks", "Arnolds Jātnieks" },
            { "solvita jirgensone", "Solvita Jirgensone" },
            { "uldis koluzs", "Uldis Kolužs" },
            { "aivija kursite", "Aivija Kursīte" },
            { "marite kikure", "Mārīte Ķikure" },
            { "juris landorfs", "Juris Landorfs" },
            { "inita nagnibeda", "Inita Nagņibeda" },
            { "aleksandrs novickis", "Aleksandrs Novickis" },
            { "janis rumba", "Jānis Rumba" },
            { "martins ruza", "Mārtiņš Ruža" },
            { "raimonds zabovs", "Raimonds Žabovs" },
        };

        public BauskaHandler() : base(
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

        public string Name => "Bauska";
        public string Prieksedetajs => "raitis abelnieks";
        public int DeputatuSkaits => 16;
        public bool AttendedSplit => true;
        public bool NotAttendedSplit => true;
        public bool AttendedNextLine => true;
        public bool NotAttendedNextLine => true;

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
                .Replace("domes  ", string.Empty)
                .Replace("– ", string.Empty);
        }

        private static string ReplaceNotAttended(string s)
        {
            return s.Replace("nepiedalas : ", string.Empty)
                        .Replace("priekssedetajas vietnieks", string.Empty)
                        .Replace("priekssedetajas vietnieki", string.Empty)
                        .Replace("Deputati", string.Empty)
                        .Replace("deputati", string.Empty)
                        .Replace("sede nepiedalas:", string.Empty);
        }
    }
}
