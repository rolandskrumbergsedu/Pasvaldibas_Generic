using System;
using PdfParsing.Logic.Handlers;
using System.Collections.Generic;

namespace PdfParsing.Results
{
    public class KegumsHandler : GeneralHandler
    {
        private static readonly List<string> AttendedStartIndexMark = new List<string>
        {
            "Sede piedalas",
            "Sēdē piedalās"
        };
        private static readonly List<string> AttendedEndIndexMark = new List<string>
        {
            "administracijas",
            "Nepiedalās",
            "Nepiedalas",
            "NOTHING"
        };
        private static readonly List<string> NotAttendedStartIndexMark = new List<string>
        {
            "Nepiedalās",
            "Nepiedalas",
            "Sede nepiedalas",
            "Sede nepiedalas"
        };
        private static readonly List<string> NotAttendedEndIndexMark = new List<string>
        {
            "NOTHING"
        };
        private static readonly string[] AttendedSplitOptions = { ", ", "," };
        private static readonly string[] NotAttendedSplitOptions = { ", ", "; ", "," };
        private static readonly string[] NotAttendedInternalSplitOptions = { "-", "–" };
        //private static readonly string[] NotAttendedInternalSplitOptions = { "(", ")" };
        //private static readonly string[] NotAttendedInternalSplitOptions = { "/" };
        private static readonly Dictionary<string, string> Deputati = new Dictionary<string, string>
        {
            //{ "", "" },
            { "valdis kalnins", "Valdis Kalniņš" },
            { "pavels kotans", "Pāvels Kotāns" },
            { "dace malina", "Dace Māliņa" },
            { "janis prizevoits", "Jānis Priževoits" },
            { "kristaps rude", "Kristaps Rūde" },
            { "liga strauss", "Līga Strauss" },
            { "viesturs teicans", "Viesturs Teicāns" },
            { "raivis uzuls", "Raivis Ūzuls" },
            { "tadeuss valevko", "Tadeušs Vaļevko" },
            { "daina vanaga", "Daina Vanaga" },
            { "ilmars zemnieks", "Ilmārs Zemnieks" },
            { "andris balodis", "Andris Balodis" },
            { "rita reinsone", "Rita Reinsone" },
            { "valentins pastars", "Valentīns Pastars" }
        };

        public KegumsHandler() : base(
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

        public string Name => "Kegums";
        public string Prieksedetajs => "ŗoberts ozols";
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