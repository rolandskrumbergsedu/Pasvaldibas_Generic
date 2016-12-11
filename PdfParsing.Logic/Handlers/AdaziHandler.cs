using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfParsing.Logic.Handlers
{
    public class AdaziHandler : GeneralHandler
    {
        private static readonly List<string> AttendedStartIndexMark = new List<string> { "Domes sede piedalas", "Sede piedalas" };
        private static readonly List<string> AttendedEndIndexMark = new List<string> { "nepiedalas (attaisnotu iemeslu del):" };
        private static readonly List<string> NotAttendedStartIndexMark = new List<string> { "nepiedalas (attaisnotu iemeslu del): ", "nepiedalas (neattaisnotu iemeslu del): ", "nepiedalas (neattaisnotu iemeslu del):" };
        private static readonly List<string> NotAttendedEndIndexMark = new List<string> { "Sede piedalas" };
        private static readonly string[] AttendedSplitOptions = { ", " };
        private static readonly string[] NotAttendedSplitOptions = { ", ", "; " };
        private static readonly string[] NotAttendedInternalSplitOptions = { "NOTHING" };
        private static readonly Dictionary<string, string> Deputati = new Dictionary<string, string>
        {
            { "peteris balzans", "Pēteris Balzāns" },
            { "artis bruvers", "Artis Brūveris" },
            { "valerijs bulans", "Valērijs Bulāns" },
            { "kerola davidsone", "Kerola Davidsone" },
            { "adrija keisa", "Adrija Keiša" },
            { "ilze petersone", "Ilze Pētersone" },
            { "edmunds plumite", "Edmunds Plūmīte" },
            { "liana pumpure", "Liāna Pumpure" },
            { "peteris pultraks", "Pēteris Pultraks" },
            { "janis neilands", "Jānis Neilands" },
            { "karina sprude", "Karina Sprūde" },
            { "normunds zviedris", "Normunds Zviedris" },
            { "juris antonovs", "Juris Antonovs" },
            { "edvins sepers", "Edvīns Šēpers" },
            { "janis ruks", "Jānis Ruks" },
            { "ugis dambis", "Uģis Dambis" },
            { "karina tinamagomedova", "Karīna Tinamagomedova" },
            { "p.balzans", "Pēteris Balzāns" },
            { "a.bruvers", "Artis Brūveris" },
            { "v.bulans", "Valērijs Bulāns" },
            { "k.davidsone", "Kerola Davidsone" },
            { "a.keisa", "Adrija Keiša" },
            { "i.petersone", "Ilze Pētersone" },
            { "e.plumite", "Edmunds Plūmīte" },
            { "l.pumpure", "Liāna Pumpure" },
            { "p.pultraks", "Pēteris Pultraks" },
            { "j.neilands", "Jānis Neilands" },
            { "k.sprude", "Karina Sprūde" },
            { "n.zviedris", "Normunds Zviedris" },
            { "j.antonovs", "Juris Antonovs" },
            { "e.sepers", "Edvīns Šēpers" },
            { "j.ruks", "Jānis Ruks" },
            { "u.dambis", "Uģis Dambis" },
            { "k.tinamagomedova", "Karīna Tinamagomedova" },
        };

        public AdaziHandler() : base(
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

        public Dictionary<string, string> CleanNotAttended(Dictionary<string, string> notAttended)
        {
            var newNotAttended = new Dictionary<string, string>();

            foreach (var item in notAttended)
            {
                var newItem = Normalize(item.Key.Replace("nepiedalas (attaisnotu iemeslu del): ", string.Empty)
                        .Replace(".", string.Empty));

                newNotAttended.Add(
                    item.Key.Replace("nepiedalas (attaisnotu iemeslu del): ", string.Empty)
                        .Replace(".", string.Empty), 
                    item.Value);
            }

            return newNotAttended;
        }
    }
}
