using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfParsing.Logic.Handlers
{
    public class GeneralHandler
    {
        private readonly List<string> _attendedStartIndexMark;
        private readonly List<string> _attendedEndIndexMark;
        private readonly List<string> _notAttendedStartIndexMark;
        private readonly List<string> _notAttendedEndIndexMark;
        private readonly string[] _attendedSplitOptions;
        private readonly string[] _notAttendedSplitOptions;
        private readonly string[] _notAttendedInternalSplitOptions;

        public GeneralHandler(List<string> attendedStartIndexMark, 
            List<string> attendedEndIndexMark,
            List<string> notAttendedStartIndexMark,
            List<string> notAttendedEndIndexMark,
            string[] attendedSplitOptions,
            string[] notAttendedSplitOptions,
            string[] notAttendedInternalSplitOptions)
        {
            _attendedStartIndexMark = attendedStartIndexMark;
            _attendedEndIndexMark = attendedEndIndexMark;
            _notAttendedStartIndexMark = notAttendedStartIndexMark;
            _notAttendedEndIndexMark = notAttendedEndIndexMark;
            _attendedSplitOptions = attendedSplitOptions;
            _notAttendedSplitOptions = notAttendedSplitOptions;
            _notAttendedInternalSplitOptions = notAttendedInternalSplitOptions;
        }

        public void Handle(List<string> rawData, out List<string> attended, out Dictionary<string, string> notAttended)
        {
            // Get attended start index
            var attendedStartIndex = GetIndex(rawData, _attendedStartIndexMark);

            // Get attended end index
            var attendedEndIndex = GetIndex(rawData, _attendedEndIndexMark);

            // Concatenate and clean attened 
            var attendedConcatenated = ConcatenateRows(rawData, attendedStartIndex, attendedEndIndex);
            var attendedCleaned = CleanData(attendedConcatenated, _attendedStartIndexMark);

            // Split attended
            attended = Split(attendedCleaned, _attendedSplitOptions);

            // Get not attended start index
            var notAttendedStartIndex = GetIndex(rawData, _notAttendedStartIndexMark);

            // Get not attended end index
            var notAttendedEndIndex = GetIndex(rawData, _notAttendedEndIndexMark);

            // Concatenate and clean not attened 
            var notAttendedConcatenated = ConcatenateRows(rawData, notAttendedStartIndex, notAttendedEndIndex);
            var notAttendedCleaned = CleanData(notAttendedConcatenated, _notAttendedStartIndexMark);
            
            // Split not attended
            notAttended = Split(notAttendedCleaned, _notAttendedSplitOptions, _notAttendedInternalSplitOptions);
        }

        private static int GetIndex(IList<string> rawData, IEnumerable<string> marks)
        {
            foreach (var mark in marks)
            {
                var index = rawData.IndexOf(rawData.FirstOrDefault(x => x.StartsWith(mark)));
                if (index != -1)
                {
                    return index;
                }
            }

            return -1;
        }

        private static int GetIndex(IList<string> rawData, List<string> marks, int startIndex)
        {
            var counter = startIndex;

            for (var i = startIndex; i < rawData.Count; i++)
            {
                if (marks.Any(mark => rawData[i].StartsWith(mark)))
                {
                    return counter;
                }

                counter++;
            }

            return counter;
        }

        private static string ConcatenateRows(IReadOnlyList<string> rawData, int startIndex, int endIndex)
        {
            var result = string.Empty;

            for (var i = startIndex; i < endIndex; i++)
            {
                result += rawData[i];
            }

            return result;
        }

        private static string CleanData(string data, IEnumerable<string> removableItems)
        {
            return removableItems.Aggregate(data, (current, removableItem) => current.Replace(removableItem, string.Empty));
        }

        private static List<string> Split(string data, string[] splitters)
        {
            return data.Split(splitters, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        private static Dictionary<string, string> Split(string data, string[] splitters, string[] internalSplitters)
        {
            return data.Split(splitters, StringSplitOptions.RemoveEmptyEntries)
                .Select(deputats => deputats.Split(internalSplitters, StringSplitOptions.RemoveEmptyEntries)).ToDictionary(splitted => splitted[0], splitted => splitted[1]);
        }
    }
}
