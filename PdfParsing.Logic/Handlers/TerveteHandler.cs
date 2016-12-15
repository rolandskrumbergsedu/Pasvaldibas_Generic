using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfParsing.Logic.Handlers
{
    public class TerveteHandler : GeneralHandler
    {
        private static readonly List<string> AttendedStartIndexMark = new List<string> { "Sede piedalas", "Sēdē piedalās", "Piedalas", "Piedalās" };
        private static readonly List<string> AttendedEndIndexMark = new List<string> { "Sede nepiedalas", "Sēdē nepiedalās", "Nepiedalas" };
        private static readonly List<string> NotAttendedStartIndexMark = new List<string> { "Nepiedalas", "Nepiedalās" };
        private static readonly List<string> NotAttendedEndIndexMark = new List<string> { "NOTHING" };
        private static readonly string[] AttendedSplitOptions = { "piedalas", "piedalās", ", ", "," };
        private static readonly string[] NotAttendedSplitOptions = { "nepiedalas", "nepiedalās", ", ", "; ", "," };
        private static readonly string[] NotAttendedInternalSplitOptions = { "-" };
        private static readonly Dictionary<string, string> Deputati = new Dictionary<string, string>
        {
            { "a.bruss", "Arvīds Brušs" },
            { "i.krumina", "Ieva Krūmiņa" },
            { "l.karlovica", "Linda Karloviča" },
            { "i.denisova", "Inese Deņisova" },
            { "e.upitis", "Edvīns Upītis" },
            { "n. namnieks", "Normunds Namnieks" },
            { "r.vazdike", "r.vazdike" },
            { "a.valdins", "Aivars Valdiņš" },
            { "arvids bruss", "Arvīds Brušs" },
            { "ieva krumina", "Ieva Krūmiņa" },
            { "linda karlovica", "Linda Karloviča" },
            { "inese denisova", "Inese Deņisova" },
            { "edvins upitis", "Edvīns Upītis" },
            { "normunds namnieks", "Normunds Namnieks" },
            { "normunds narvaiss", "Normunds Narvaišs" },
            { "aivars valdins", "Aivars Valdiņš" },
            { "dzintra sirsone", "Dzintra Sirsone" },
            { "sandris laizans", "Sandris Laizāns" },
            { "anitra skalbina", "Anitra Skalbiņa" },
            { "indrikis veveris", "Indriķis Vēveris" }
        };

        public TerveteHandler() : base(
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
                var splitted = item.Split(new[] { ":", "-" }, StringSplitOptions.RemoveEmptyEntries);
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
                    item.Key
                        .Replace(".", string.Empty)
                        .Replace("deputati:  ", string.Empty)
                        .Replace("deputati: ", string.Empty)
                        .Replace("nepiedalas: ", string.Empty),
                    item.Value);
            }

            return newNotAttended;
        }
    }
}
