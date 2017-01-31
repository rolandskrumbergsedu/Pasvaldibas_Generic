using System;
using System.Collections.Generic;
using PdfParsing.Logic.Handlers;

namespace PdfParsing.Results
{
    public class LivaniHandler : GeneralHandler
    {
        private static readonly List<string> AttendedStartIndexMark = new List<string>
        {
            "Sēdē piedalās deputāti:",
            "Sede piedalas deputati:"
        };
        private static readonly List<string> AttendedEndIndexMark = new List<string>
        {
            "Nav ieradušies deputāti",
            "Nav ieradušies deputati",
            "Nav ieradusies deputati",
            "NOTHING"
        };
        private static readonly List<string> NotAttendedStartIndexMark = new List<string>
        {
            "Nav ieradušies deputāti",
            "Nav ieradušies deputati",
            "Nav ieradusies deputati",
        };
        private static readonly List<string> NotAttendedEndIndexMark = new List<string>
        {
            "NOTHING"
        };
        private static readonly string[] AttendedSplitOptions = { ", ", "," };
        private static readonly string[] NotAttendedSplitOptions = { ", ", "; ", "," };
        //private static readonly string[] NotAttendedInternalSplitOptions = { "-", "–" };
        private static readonly string[] NotAttendedInternalSplitOptions = { "," };
        //private static readonly string[] NotAttendedInternalSplitOptions = { "/" };
        private static readonly Dictionary<string, string> Deputati = new Dictionary<string, string>
        {
            { "valdis anspaks", "Valdis Anspaks" },
            { "maris grigalis", "Māris Grigalis" },
            { "inese jaunusane", "Inese Jaunusne" },
            { "inara kalvane", "Ināra Kalvāne" },
            { "juris kirillovs", "Juris Kirillovs" },
            { "janis magdalenoks", "Jānis Magdaļenoks" },
            { "peteris romanovskis", "Pēteris Romanovskis" },
            { "eriks salcevics", "Ēriks Salcevičs" },
            { "levs troskovs", "Ļevs Troškovs" },
            { "ainis veigurs", "Ainis Veigurs" },
            { "marite vilcane", "Mārīte Vilcāne" },
            { "galina krjukova", "Gaļina Krjukova" },
            { "normunds liepnieks", "Normunds Liepnieks" },
            { "aija usane", "Aija Usāne" },
        };

        public LivaniHandler() : base(
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

        public string Name => "Livani";
        public string Prieksedetajs => "andris vaivods";
        public int DeputatuSkaits => 14;
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