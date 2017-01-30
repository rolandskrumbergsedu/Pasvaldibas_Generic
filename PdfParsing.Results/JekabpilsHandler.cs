using System;
using System.Collections.Generic;
using PdfParsing.Logic.Handlers;

namespace PdfParsing.Results
{
    public class JekabpilsHandler : GeneralHandler
    {
        private static readonly List<string> AttendedStartIndexMark = new List<string>
        {
            "Piedalās",
            "Piedalas",
            "Sēdē piedalījās",
            "Sede piedalijas"
        };
        private static readonly List<string> AttendedEndIndexMark = new List<string>
        {
            "Nepiedalās",
            "Nepiedalas",
            "Sēdē attaisnojošu",
            "Sede attaisnojošu",
            "NOTHING"
        };
        private static readonly List<string> NotAttendedStartIndexMark = new List<string>
        {
            "Nepiedalās",
            "Nepiedalas",
            "Sēdē attaisnojošu",
            "Sede attaisnojošu"
        };
        private static readonly List<string> NotAttendedEndIndexMark = new List<string>
        {
            "Sēdē piedalās",
            "Sede piedalas",
            "Sēdē piedalījās amatpersonas",
            "Sede piedalijas amatpersonas",
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
            { "i.blumberga", "Ināra Blumberga" },
            { "u.auzans", "Uldis Auzāns" },
            { "a.kokins", "Aivis Kokins" },
            { "o.speka", "Olita Spēka" },
            { "d.geidane", "Daiga Ģeidāne" },
            { "a.bikaunieks", "Andis Bikaunieks" },
            { "g.audzitis", "Gints Audzītis" },
            { "m.balodis", "Māris Balodis" },
            { "s.vecumniece", "Sandra Vecumniece" },
            { "a.lemaka", "Anita Lemaka" },
            { "m.pavars", "Modris Pavārs" },
            { "a.vanags", "Aivars Vanags" },
            { "i.jatniece", "Ieva Jātniece" },
            { "a.kiploks", "Armands Ķiploks" },
            { "inara blumberga", "Ināra Blumberga" },
            { "uldis auzans", "Uldis Auzāns" },
            { "aivis kokins", "Aivis Kokins" },
            { "olita speka", "Olita Spēka" },
            { "daiga geidane", "Daiga Ģeidāne" },
            { "andis bikaunieks", "Andis Bikaunieks" },
            { "gints audzitis", "Gints Audzītis" },
            { "maris balodis", "Māris Balodis" },
            { "sandra vecumniece", "Sandra Vecumniece" },
            { "anita lemaka", "Anita Lemaka" },
            { "modris pavars", "Modris Pavārs" },
            { "aivars vanags", "Aivars Vanags" },
            { "ievajatniece", "Ieva Jātniece" },
            { "armands kiploks", "Armands Ķiploks" }
        };

        public JekabpilsHandler() : base(
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

        public string Name => "Jekabpils";
        public string Prieksedetajs => "e.menkis";
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