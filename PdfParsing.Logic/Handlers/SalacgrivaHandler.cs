using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfParsing.Logic.Handlers
{
    public class SalacgrivaHandler : GeneralHandler
    {
        private static readonly List<string> AttendedStartIndexMark = new List<string> { "Deputati:", " Deputati:" };
        private static readonly List<string> AttendedEndIndexMark = new List<string> { "Pašvaldibas administracijas darbinieki:" };
        private static readonly List<string> NotAttendedStartIndexMark = new List<string> { "Nepiedalas deputati - ", "Nepiedalas deputati- ", "Nepiedalas- " };
        private static readonly List<string> NotAttendedEndIndexMark = new List<string> { " " };
        private static readonly string[] AttendedSplitOptions = { ", " };
        private static readonly string[] NotAttendedSplitOptions = { ", ", "; " };
        private static readonly string[] NotAttendedInternalSplitOptions = { "(", ")" };

        public SalacgrivaHandler() : base(AttendedStartIndexMark, 
            AttendedEndIndexMark,
            NotAttendedStartIndexMark,
            NotAttendedEndIndexMark,
            AttendedSplitOptions,
            NotAttendedSplitOptions,
            NotAttendedInternalSplitOptions)
        {
            
        }

        public string GetDate()
        {
            return "111";
        }
    }
}
