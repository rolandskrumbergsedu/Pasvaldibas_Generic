using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfParsing.Logic.Handlers
{
    public class PlavinasHandler : GeneralHandler
    {
        private static readonly List<string> AttendedStartIndexMark = new List<string>
        {
            //"Sēdē piedalās",
            //"Sede piedalas",
            //"Deputati",
            //"Deputāti",
            //"Ieradusies",
            //"Ieradušies",
            "Domes deputati",
            "Domes deputāti"
        };
        private static readonly List<string> AttendedEndIndexMark = new List<string>
        {
            "Sēdē nepiedalās",
            "Sede nepiedalas",
            "Domes darbinieki",
            //"Pasvaldibas darbinieki",
            //"Pašvaldības darbinieki",
            //"Pašvaldibas darbinieki"
        };
        private static readonly List<string> NotAttendedStartIndexMark = new List<string>
        {
            "Sēdē nepiedalās",
            "Sede nepiedalas",
            //"Nepiedalas",
            //"Nepiedalās"
        };
        private static readonly List<string> NotAttendedEndIndexMark = new List<string>
        {
            //"Darba",
            "Domes darbinieki",
            //"NOTHING"
        };
        private static readonly string[] AttendedSplitOptions = { ", ", "," };
        private static readonly string[] NotAttendedSplitOptions = { ", ", "; ", "," };
        private static readonly string[] NotAttendedInternalSplitOptions = { "-", "–" };
        private static readonly Dictionary<string, string> Deputati = new Dictionary<string, string>
        {
            { "aigars lukss", "Aigars Lukss" },
            { "alita klusa", "Alita Kluša" },
            { "silga stucka", "Silga Stučka" },
            { "ainars arnitis", "Ainārs Arnītis" },
            { "andrejs kackans", "Andrejs Kačkāns" },
            { "ivans burikins", "Ivans Burikins" },
            { "aldis lazda", "Aldis Lazda" },
            { "imants silins", "Imants Siliņš" },
            { "imants kravalis", "Imants Kravalis"},
            { "juris ikaunieks", "Juris Ikaunieks"},
            { "janis caune", "Jānis Caune"},
            { "vladimirs samohins", "Vladimirs Samohins"},
            { "ansis salins", "Ansis Saliņš"},
            { "peteris kalnins", "Pēteris Kalniņš" }
        };

        public PlavinasHandler() : base(
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

        public string Name => "Plavinas";
        public string Prieksedetajs => "gunta zilde";
        public int DeputatuSkaits => 14;
        public bool AttendedSplit => false;
        public bool NotAttendedSplit => true;
        public bool AttendedNextLine => false;
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
            return s.Replace("domes priekssedetaja", string.Empty)
                        .Replace("priekssedetajas vietnieks", string.Empty)
                        .Replace("priekssedetajas vietnieki", string.Empty)
                        .Replace("Deputati", string.Empty)
                        .Replace("deputati", string.Empty)
                        .Replace("sede nepiedalas:", string.Empty);
        }
    }
}
