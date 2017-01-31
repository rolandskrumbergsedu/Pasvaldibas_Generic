using System;
using System.Collections.Generic;
using PdfParsing.Logic.Handlers;

namespace PdfParsing.Results
{
    public class MarupeHandler : GeneralHandler
    {
        private static readonly List<string> AttendedStartIndexMark = new List<string>
        {
            "Ārkārtas Domes sēdē piedalās deputāti",
            "Domes sēdē piedalās deputāti",
            "Arkartas Domes sede piedalas deputati",
            "Domes sede piedalas deputati",
            "Ārkārtas domes sēdē piedalās deputāti",
            "Arkartas domes sede piedalas deputati",
        };
        private static readonly List<string> AttendedEndIndexMark = new List<string>
        {
            "Ārkārtas Domes sēdē nepiedalās",
            "Domes sēdē nepiedalās",
            "Arkartas Domes sede nepiedalas",
            "Domes sede nepiedalas",
            "Ārkārtas domes sēdē nepiedalās",
            "Arkartas domes sede nepiedalas",
            "NOTHING"
        };
        private static readonly List<string> NotAttendedStartIndexMark = new List<string>
        {
            "Ārkārtas Domes sēdē nepiedalās",
            "Domes sēdē nepiedalās",
            "Arkartas Domes sede nepiedalas",
            "Domes sede nepiedalas",
            "Ārkārtas domes sēdē nepiedalās",
            "Arkartas domes sede nepiedalas",
        };
        private static readonly List<string> NotAttendedEndIndexMark = new List<string>
        {
            "NOTHING"
        };
        private static readonly string[] AttendedSplitOptions = { ", ", "," };
        private static readonly string[] NotAttendedSplitOptions = { ", ", "; ", "," };
        private static readonly string[] NotAttendedInternalSplitOptions = { "-", "–" };
        private static readonly Dictionary<string, string> Deputati = new Dictionary<string, string>
        {
            { "liga kadige", "Līga Kadiģe" },
            { "peteris pikse", "Pēteris Pikše" },
            { "andrejs kirillovs", "Andrejs Kirillovs" },
            { "edgars jansons", "Edgars Jansons" },
            { "normunds orleans", "Normunds Orleāns" },
            { "ira duduma", "Ira Dūduma" },
            { "aleksandrs mihailovs", "Aleksandrs Mihailovs" },
            { "andris puide", "Andris Puide" },
            { "raivis zeltits", "Raivis Zeltīts" },
            { "janis rusenieks", "Jānis Rušenieks" },
            { "maija bauda", "Maija Bauda" },
            { "janis lagzdkalns", "Jānis Lagzdkalns" },
            { "gatis vacietis", "Gatis Vācietis" },
            { "renars freibergs", "Renārs Freibergs" }
        };

        public MarupeHandler() : base(
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

        public string Name => "Marupe";
        public string Prieksedetajs => "martins bojars";
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