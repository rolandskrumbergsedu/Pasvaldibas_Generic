using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfParsing.Logic.Handlers
{
    public class AuceHandler : GeneralHandler 
    {
        private static readonly List<string> AttendedStartIndexMark = new List<string>
        {
            //"Sede piedalas",
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
            "Pasvaldibas darbinieki",
            "Pašvaldibas darbinieki",
            "Pasvaldibas administracijas",
            "Pašvaldibas administracijas",
            //"Amatas novada pašvaldibas izpilddirektors",
            //"Nepiedalas deputati",
            "Nepiedalas",
        };
        private static readonly List<string> NotAttendedStartIndexMark = new List<string>
        {
            //"Sede nepiedalas",
            "Nepiedalas",
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
            { "girts ante", "Ģirts Ante" },
            { "ģirts ante", "Ģirts Ante" },
            { "juris abele", "Juris Ābele" },
            { "andris egils abolins", "Andris Egils Āboliņš" },
            { "valdis bergmanis", "Valdis Bergmanis" },
            { "inita bilkste", "Inita Bilkšte" },
            { "ivita cerina", "Ivita Ceriņa" },
            { "maris eihmanis", "Māris Eihmanis" },
            { "arnis hofmanis", "Arnis Hofmanis" },
            { "vija kersus", "Vija Keršus" },
            { "vidmants krapauskis", "Vidmants Krapauskis" },
            { "aija mule", "Aija Mule" },
            { "indra spela", "Indra Špela" },
            { "aigars vernis", "Aigars Vernis" },
            { "valda zvirbule", "Valda Zvirbule" }
        };

        public AuceHandler() : base(
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

        public string Name => "Auce";
        public string Prieksedetajs => "gints kaminskis";
        public int DeputatuSkaits => 14;
        public bool AttendedSplit => false;
        public bool NotAttendedSplit => false;
        public bool AttendedNextLine => true;
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
