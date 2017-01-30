using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfParsing.Logic.Handlers
{
    public class BroceniHandler : GeneralHandler
    {
        private static readonly List<string> AttendedStartIndexMark = new List<string>
        {
            //"Sede piedalas",
            //"SEDE PIEDALAS",
            //" Sede piedalas",
            //"  Sede piedalas",
            //"Deputati",
            //"Ieradusies",
            //"Ieradušies",
            //"Domes deputati",
            //"Sede piedalas Beverinas",
            //"Piedalas deputati",
            "Piedalas",
            "PIEDALAS",
        };
        private static readonly List<string> AttendedEndIndexMark = new List<string>
        {
            //"Sede nepiedalas",
            //"Domes darbinieki",
            //"Sede piedalas",
            //"SEDE PIEDALAS:",
            //"Pasvaldibas darbinieki",
            //"Pašvaldibas darbinieki",
            //"Pasvaldibas administracijas",
            //"Pašvaldibas administracijas",
            //"Amatas novada pašvaldibas izpilddirektors",
            //"Nepiedalas deputati",
            "Nepiedalas",
            "NEPIEDALAS",
        };
        private static readonly List<string> NotAttendedStartIndexMark = new List<string>
        {
            //"Sede nepiedalas",
            //"SEDE NEPIEDALAS:"
            "Nepiedalas",
            "NEPIEDALAS",
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
            //{ "", "" },Ināra Bērziņa,
            //Solvita Dūklava, Ainis Bruzinskis 
            { "asprogis", "Ārijs Sproģis" },
            { "ibirgersone", "ibirgersone" },
            { "dfridmane", "Daila Frīdmane" },
            { "ameters", "Arvīds Mēters" },
            { "jsergejenko", "Jānis Sergejenko" },
            { "vsalava", "Vineta Sālava" },
            { "ididrihsone", "Inese Didrihsone" },
            { "aabuls", "Ansis Ābuls" },
            { "zgfirers", "Zigmunds Georgs Fīrers" },
            { "rrullis", "Rinalds Rullis" },
            { "mgolubeva", "Maija Golubeva" },
            { "irulle", "Inita Rulle" },
            { "dgredzena", "Daina Gredzena" },
            { "gbalahovics", "Gatis Balahovičs" },
            { "arijs sproģis", "Ārijs Sproģis" },
            { "daila fridmane", "Daila Frīdmane" },
            { "arvids meters", "Arvīds Mēters" },
            { "janis sergejenko", "Jānis Sergejenko" },
            { "vineta salava", "Vineta Sālava" },
            { "inese didrihsone", "Inese Didrihsone" },
            { "ansis abuls", "Ansis Ābuls" },
            { "rinalds rullis", "Rinalds Rullis" },
            { "maija golubeva", "Maija Golubeva" },
            { "inita rulle", "Inita Rulle" },
            { "daina gredzena", "Daina Gredzena" },
            { "gatis balahovics", "Gatis Balahovičs" },
            { "a.sprogis", "Ārijs Sproģis" },
            { "i.birgersone", "ibirgersone" },
            { "d.fridmane", "Daila Frīdmane" },
            { "a.meters", "Arvīds Mēters" },
            { "j.sergejenko", "Jānis Sergejenko" },
            { "v.salava", "Vineta Sālava" },
            { "i.didrihsone", "Inese Didrihsone" },
            { "a.abuls", "Ansis Ābuls" },
            { "z.gfirers", "Zigmunds Georgs Fīrers" },
            { "r.rullis", "Rinalds Rullis" },
            { "m.golubeva", "Maija Golubeva" },
            { "i.rulle", "Inita Rulle" },
            { "d.gredzena", "Daina Gredzena" },
            { "g.balahovics", "Gatis Balahovičs" },
        };

        public BroceniHandler() : base(
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

        public string Name => "Broceni";
        public string Prieksedetajs => "s.duklava";
        public int DeputatuSkaits => 14;
        public bool AttendedSplit => false;
        public bool NotAttendedSplit => true;
        public bool AttendedNextLine => true;
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
            return s.Replace("nepiedalas", string.Empty)
                        .Replace(": ", string.Empty)
                        .Replace("priekssedetajas vietnieki", string.Empty)
                        .Replace("Deputati", string.Empty)
                        .Replace("deputati", string.Empty)
                        .Replace("nepiedalas deputats ", string.Empty);
        }
    }
}
