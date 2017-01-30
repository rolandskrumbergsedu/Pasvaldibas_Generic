using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfParsing.Logic.Handlers
{
    public class IncukalnsHandler : GeneralHandler
    {
        private static readonly List<string> AttendedStartIndexMark = new List<string>
        {
            "Sede piedalas domes",
            //"Sēdē piedalās",
            //"SEDE PIEDALAS",
            //" Sede piedalas",
            //"  Sede piedalas",
            //"Deputati",
            //"Sede piedalas deputati",
            //"Ieradusies",
            //"Ieradušies",
            //"Domes deputati",
            //"Sede piedalas Beverinas",
            //"Piedalas domes deputati",
            //"Piedalas",
            //"PIEDALAS",
        };
        private static readonly List<string> AttendedEndIndexMark = new List<string>
        {
            //"Sede nepiedalas",
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
            //"Nepiedalas deputati",
            //"Nepiedalas",
            //"Neieradas",
            //"NEPIEDALAS",
            "NOTHING"
        };
        private static readonly List<string> NotAttendedStartIndexMark = new List<string>
        {
            //"Sede nepiedalas",
            //"Sēdē nepiedalās",
            //"SEDE NEPIEDALAS:"
            //"Sede nepiedalas",
            //"Sēdē nepiedalās",
            //"Nepiedalas",
            "nepiedalas",
            //"Neieradas",
            //"NEPIEDALAS",
            //"Nepiedalas deputati",
        };
        private static readonly List<string> NotAttendedEndIndexMark = new List<string>
        {
            "Sede piedalas:",
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
            { "a.freimane", "Alda Freimane" },
            { "a.geiba", "Antons Geiba" },
            { "a.cirulnieks", "Armands Cīrulnieks" },
            { "a.blaus", "Arvīds Blaus" },
            { "i.bernats", "Ivo Bernats" },
            { "j.liepins", "Jānis Liepiņš" },
            { "j.saroka", "Jekaterina Šaroka" },
            { "m.jaunups", "Modris Jaunups" },
            { "n.kozlovs", "Ņikita Kozlovs" },
            { "s.gorcinskis", "Sergejs Gorčinskis" },
            { "i.purmalis", "Igors Purmalis" },
            { "l.vorobjova", "Ludmila Vorobjova" },
            { "m.keiss", "Māris Keišs" },
            { "a.sica", "Anete Šica" },
            { "a.brinkis", "Andrs Briņķis" },
            { "j.rozitis", "Jānis Rozītis" },
            { "alda freimane", "Alda Freimane" },
            { "antons geiba", "Antons Geiba" },
            { "aramands cirulnieks", "Armands Cīrulnieks" },
            { "arvids blaus", "Arvīds Blaus" },
            { "ivo bernats", "Ivo Bernats" },
            { "janis liepins", "Jānis Liepiņš" },
            { "jekaterina saroka", "Jekaterina Šaroka" },
            { "modris jaunups", "Modris Jaunups" },
            { "nikita kozlovs", "Ņikita Kozlovs" },
            { "sergejs gorcinskis", "Sergejs Gorčinskis" },
            { "igors purmalis", "Igors Purmalis" },
            { "ludmila vorobjova", "Ludmila Vorobjova" },
            { "maris keiss", "Māris Keišs" },
            { "anete sica", "Anete Šica" },
            { "andrs brinkis", "Andrs Briņķis" },
            { "janis rozitis", "Jānis Rozītis" },
        };

        public IncukalnsHandler() : base(
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

        public string Name => "Incukalns";
        public string Prieksedetajs => "a.nalivaiko";
        public int DeputatuSkaits => 14;
        public bool AttendedSplit => false;
        public bool NotAttendedSplit => true;
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
