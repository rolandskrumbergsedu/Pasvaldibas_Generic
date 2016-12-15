using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfParsing.Logic.Handlers
{
    public class VainodeHandler : GeneralHandler
    {
        private static readonly List<string> AttendedStartIndexMark = new List<string> { "Piedalas", "Piedalās" };
        private static readonly List<string> AttendedEndIndexMark = new List<string> { "Nepiedalas" };
        private static readonly List<string> NotAttendedStartIndexMark = new List<string> { "Nepiedalas" };
        private static readonly List<string> NotAttendedEndIndexMark = new List<string> { "Darba kartiba", "Uzaicinats" };
        private static readonly string[] AttendedSplitOptions = { ", ", "," };
        private static readonly string[] NotAttendedSplitOptions = { ", ", "; ", "," };
        private static readonly string[] NotAttendedInternalSplitOptions = { "-" };
        private static readonly Dictionary<string, string> Deputati = new Dictionary<string, string>
        {
            { "aiga jaunzeme", "Aiga Jaunzeme" },
            { "olegs jurjevs", "Oļegs Jurjevs" },
            { "vitauts pragulbickis", "Vitauts Pragulbickis" },
            { "sandra grosberga", "Sandra Grosberga" },
            { "teodors roze", "Teodors Roze" },
            { "aigars diks", "Aigars Dīks" },
            { "sandra kempe", "Sandra Ķempe" },
            { "zigmunds mickus", "Zigmunds Mickus" }
        };

        public VainodeHandler() : base(
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
