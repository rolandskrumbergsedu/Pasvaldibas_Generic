using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfParsing.Logic.Handlers
{
    public class ViesiteHandler : GeneralHandler
    {
        private static readonly List<string> AttendedStartIndexMark = new List<string> { "Piedalas novada" };
        private static readonly List<string> AttendedEndIndexMark = new List<string> { "Nepiedalas" };
        private static readonly List<string> NotAttendedStartIndexMark = new List<string> { "Nepiedalas" };
        private static readonly List<string> NotAttendedEndIndexMark = new List<string> { "Piedalas pasvaldibas" };
        private static readonly string[] AttendedSplitOptions = { ", ", "," };
        private static readonly string[] NotAttendedSplitOptions = { ", ", "; ", "," };
        private static readonly string[] NotAttendedInternalSplitOptions = { "-" };
        private static readonly Dictionary<string, string> Deputati = new Dictionary<string, string>
        {
            { "valda buzijana", "Valda Buzijana" },
            { "leonids cvetkovs", "Leonids Cvetkovs" },
            { "alberts dravins", "Alberts Draviņš" },
            { "jaroslavs kozlovs", "Jaroslavs Kozlovs" },
            { "andis locmelis", "Andis Ločmelis" },
            { "uldis matisans", "Uldis Matisāns" },
            { "aldis puspurs", "Aldis Pušpurs" },
            { "raimonds slisans", "Raimonds Slišāns" },
            { "inara sokirka", "Ināra Sokirka" },
            { "jelena suhiha", "Jeļena Suhiha" },
            { "sarmite saicane", "Sarmīte Šaicāne" },
            { "regina brokane", "Regīna Brokāne" },
            { "anita kokorevica", "Anita Kokoreviča" },
            { "ilze saicane", "Ilze Šaicāne" }
        };

        public ViesiteHandler() : base(
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


        public List<string> CleanAttended(List<string> attended)
        {
            var newAttended = new List<string>();

            foreach (var item in attended)
            {
                var splitted = item.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                if (splitted.Count() > 1)
                {
                    newAttended.Add(Normalize(splitted[1]));
                }
                else
                {
                    newAttended.Add(Normalize(item));
                }
            }

            return newAttended;

        }

        public Dictionary<string, string> CleanNotAttended(Dictionary<string, string> notAttended)
        {
            var newNotAttended = new Dictionary<string, string>();

            foreach (var item in notAttended)
            {
                newNotAttended.Add(
                    item.Key.Replace("nepiedalas (attaisnotu iemeslu del): ", string.Empty)
                        .Replace(".", string.Empty)
                        .Replace("deputati:  ", string.Empty)
                        .Replace("deputati: ", string.Empty),
                    item.Value);
            }

            return newNotAttended;
        }
    }
}
