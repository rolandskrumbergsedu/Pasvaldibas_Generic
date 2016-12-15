using System;
using System.Collections.Generic;
using System.Linq;

namespace PdfParsing.Logic.Handlers
{
    public class ViesiteHandler : GeneralHandler
    {
        private static readonly List<string> AttendedStartIndexMark = new List<string> { "Piedalas novada" };
        private static readonly List<string> AttendedEndIndexMark = new List<string> { "Nepiedalas" };
        private static readonly List<string> NotAttendedStartIndexMark = new List<string> { "Nepiedalas" };
        private static readonly List<string> NotAttendedEndIndexMark = new List<string> { "Piedalas pasvaldibas", "Piedalas pašvaldibas" };
        private static readonly string[] AttendedSplitOptions = { ", ", "," };
        private static readonly string[] NotAttendedSplitOptions = { ", ", "; ", "," };
        private static readonly string[] NotAttendedInternalSplitOptions = { "-" };
        private static readonly Dictionary<string, string> Deputati = new Dictionary<string, string>
        {
            { "andris baldunciks", "Andris Baldunčiks" },
            { "andris baldunčiks", "Andris Baldunčiks" },
            { "vita elksne", "Vita Elksne" },
            { "maris malcenieks", "Māris Malcenieks" },
            { "māris malcenieks", "Māris Malcenieks" },
            { "aina pecauska", "Aina Pečauska" },
            { "aina pečauska", "Aina Pečauska" },
            { "rimants sveds", "Rimants Šveds" },
            { "svetlana andruskevica", "Svetlana Andruškeviča" },
            { "svetlana andruskeviča", "Svetlana Andruškeviča" },
            { "juris licis", "Juris Līcis" },
            { "juris līcis", "Juris Līcis" },
            { "juri licis", "Juris Līcis" },
            { "roberts orups", "Roberts Orups" },
            { "rudite urbacane", "Rudīte Urbacāne" },
            { "rudīte urbacāne", "Rudīte Urbacāne" }
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
                        .Replace("deputati: ", string.Empty)
                        .Replace("nepiedalas: ", string.Empty),
                    item.Value);
            }

            return newNotAttended;
        }
    }
}
