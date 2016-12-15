using System;
using System.Collections.Generic;
using System.Linq;

namespace PdfParsing.Logic.Handlers
{
    public class TukumsHandler : GeneralHandler
    {
        private static readonly List<string> AttendedStartIndexMark = new List<string> { "Sede piedalas", "Sēdē piedalās" };
        private static readonly List<string> AttendedEndIndexMark = new List<string> { "Nepiedalas" };
        private static readonly List<string> NotAttendedStartIndexMark = new List<string> { "(nepiedalās", "(nepiedalas" };
        private static readonly List<string> NotAttendedEndIndexMark = new List<string> { "Darba kartiba", "Uzaicinats" };
        private static readonly string[] AttendedSplitOptions = { ", ", "," };
        private static readonly string[] NotAttendedSplitOptions = { ", ", "; ", "," };
        private static readonly string[] NotAttendedInternalSplitOptions = { "-" };
        private static readonly Dictionary<string, string> Deputati = new Dictionary<string, string>
        {
            { "agris baumanis", "Agris Baumanis" },
            { "juris boguzs", "Juris Bogužs" },
            { "arvids drikis", "Arvīds Driķis" },
            { "arvīds driķis", "Arvīds Driķis" },
            { "gunta kalvina", "Gunta Kalviņa" },
            { "gunta kalviņa", "Gunta Kalviņa" },
            { "dace lebeda", "Dace Lebeda" },
            { "modris liepins", "Modris Liepiņš" },
            { "eriks lukmans", "Ēriks Lukmans" },
            { "dace pole", "Dace Pole" },
            { "normunds recs", "Normunds Rečs" },
            { "ludmila reimate", "Ludmila Reimate" },
            { "vladimirs skuja", "Vladimirs Skuja" },
            { "dagnija stake", "Dagnija Staķe" },
            { "juris sulcs", "Juris Šulcs" },
            { "aivars volfs", "Aivars Volfs" },
            { "agris zvaigzneskalns", "Agris Zvaigzneskalns" },
            { "janis rosickis", "Jānis Rosickis" },
            { "indulis zarins", "Indulis Zariņš" },
            { "indulis zariņs", "Indulis Zariņš" },
            { "linda zemite", "Linda Zemīte" }
        };

        public TukumsHandler() : base(
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
