using System;
using System.Collections.Generic;
using PdfParsing.Logic.Handlers;

namespace PdfParsing.Results
{
    public class LudzaHandler : GeneralHandler
    {
        private static readonly List<string> AttendedStartIndexMark = new List<string>
        {
            "Ludzas novada domes deputāti",
            "Sēdē piedalās - novada domes deputāti",
            "Ludzas novada domes deputati",
            "Sede piedalas - novada domes deputati",
            "Ludzasnovadadomesdeputati:",
            "Ludzas novada domes deputti"
        };
        private static readonly List<string> AttendedEndIndexMark = new List<string>
        {
            "Sēdē nepiedalās",
            "Sede nepiedalas",
            "Sedenepiedalas:",
            "NOTHING"
        };
        private static readonly List<string> NotAttendedStartIndexMark = new List<string>
        {
            "Sēdē nepiedalās",
            "Sede nepiedalas",
            "Sedenepiedalas:"
        };
        private static readonly List<string> NotAttendedEndIndexMark = new List<string>
        {
            "NOTHING"
        };
        private static readonly string[] AttendedSplitOptions = { ", ", "," };
        private static readonly string[] NotAttendedSplitOptions = { ", ", "; ", "," };
        private static readonly string[] NotAttendedInternalSplitOptions = { "," };
        private static readonly Dictionary<string, string> Deputati = new Dictionary<string, string>
        {
            //{ "", "" },
            { "andrejs andrejevs", "Andrejs Andrejevs" },
            { "juris atstupens", "Juris Atstupens" },
            { "olita baklane", "Olita Baklāne" },
            { "voldemars dibains", "Voldemārs Dibaņins" },
            { "voldemars dibanins", "Voldemārs Dibaņins" },
            { "lolita greitane", "Lolita Greitāne" },
            { "janis ivulans", "Jānis Ivulāns" },
            { "jevgenija kusca", "Jevgenija Kušča" },
            { "igors lapsovs", "Igors Lapšovs" },
            { "aivars meiksans", "Aivars Meikšāns" },
            { "edgars mekss", "Edgars Mekšs" },
            { "eleonora obrumane", "Eleonora Obrumāne" },
            { "olga petrova", "Olga Petrova" },
            { "svetlana rjutkinena", "Svetlana Rjutkinena" },
            { "regna vorobjova", "Regina Vorobjova" },
            { "regina vorobjova", "Regina Vorobjova" },
        };

        public LudzaHandler() : base(
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

        public string Name => "Ludza";
        public string Prieksedetajs => "alina gendele";
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