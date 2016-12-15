using System;
using System.Collections.Generic;
using System.Linq;

namespace PdfParsing.Logic.Handlers
{
    public class SkriveriHandler : GeneralHandler
    {
        private static readonly List<string> AttendedStartIndexMark = new List<string> { "Sede piedalas", "Sēdē piedalās" };
        private static readonly List<string> AttendedEndIndexMark = new List<string> { "Pašvaldības", "Pašvaldibas", "Pasvaldibas", "Nepiedalas" };
        private static readonly List<string> NotAttendedStartIndexMark = new List<string> { "Nepiedalas", "Nepiedalās" };
        private static readonly List<string> NotAttendedEndIndexMark = new List<string> { "Klausas", "Klausās" };
        private static readonly string[] AttendedSplitOptions = { ", ", "," };
        private static readonly string[] NotAttendedSplitOptions = { "nepiedalas", "nepiedalās", ", ", "; ", "," };
        private static readonly string[] NotAttendedInternalSplitOptions = { "–", "-" };
        private static readonly Dictionary<string, string> Deputati = new Dictionary<string, string>
        {
            { "iveta bikerniece", "Iveta Biķerniece" },
            { "janis brokans", "Jānis Brokāns" },
            { "inara dika", "Ināra Dika" },
            { "uldis dzerve", "Uldis Dzērve" },
            { "sarmite jansone", "Sarmīte Jansone" },
            { "udo persis", "Udo Pērsis" },
            { "deniss vigovskis", "Deniss Vigovskis" },
            { "arnis zitans", "Arnis Zitāns" }
        };

        public SkriveriHandler() : base(
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
                        .Replace("Deputati:", string.Empty)
                        .Replace("deputati:  ", string.Empty)
                        .Replace("deputati: ", string.Empty)
                        .Replace("nepiedalas: ", string.Empty),
                    item.Value);
            }

            return newNotAttended;
        }
    }
}
