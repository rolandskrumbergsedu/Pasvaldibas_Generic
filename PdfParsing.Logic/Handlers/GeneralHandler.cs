using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        private readonly Dictionary<string, string> _deputati;

        public GeneralHandler(
            List<string> attendedStartIndexMark,
            List<string> attendedEndIndexMark,
            List<string> notAttendedStartIndexMark,
            List<string> notAttendedEndIndexMark,
            string[] attendedSplitOptions,
            string[] notAttendedSplitOptions,
            string[] notAttendedInternalSplitOptions,
            Dictionary<string, string> deputati)
        {
            _attendedStartIndexMark = attendedStartIndexMark;
            _attendedEndIndexMark = attendedEndIndexMark;
            _notAttendedStartIndexMark = notAttendedStartIndexMark;
            _notAttendedEndIndexMark = notAttendedEndIndexMark;

            _attendedSplitOptions = attendedSplitOptions;
            _notAttendedSplitOptions = notAttendedSplitOptions;

            _notAttendedInternalSplitOptions = notAttendedInternalSplitOptions;

            _deputati = deputati;
        }

        public void Handle(List<string> rawData, 
            out List<string> attended, 
            out Dictionary<string, string> notAttended, 
            bool attendedSplit,
            bool notAttendedSplit,
            bool attendedNextLine,
            bool notAttendedNextLine)
        {
            if (attendedSplit && notAttendedSplit)
            {
                HandleAttendedSplitNotAttendedSplit(rawData, out attended, out notAttended, attendedNextLine, notAttendedNextLine);
            }

            if (!attendedSplit && notAttendedSplit)
            {
                HandleAttendedNotSplitNotAttendedSplit(rawData, out attended, out notAttended, attendedNextLine, notAttendedNextLine);
            }

            if (attendedSplit && !notAttendedSplit)
            {
                HandleAttendedSplitNotAttendedNotSplit(rawData, out attended, out notAttended, attendedNextLine, notAttendedNextLine);
            }
            else
            {
                HandleAttendedNotSplitNotAttendedNotSplit(rawData, out attended, out notAttended, attendedNextLine, notAttendedNextLine);
            }
        }

        public void HandleAttendedSplitNotAttendedSplit(List<string> rawData, 
            out List<string> attended, 
            out Dictionary<string, string> notAttended, 
            bool attendedNextLine, 
            bool notAttendedNextLine)
        {
            // Get attended start index
            var startIndex = GetIndex(rawData, _attendedStartIndexMark, attendedNextLine);

            if (startIndex == -1)
            {
                throw new ApplicationException("Could not find start for attended!");
            }

            // Get attended end index
            var endIndex = GetEndIndex(rawData, _attendedEndIndexMark);

            if ((endIndex - startIndex > 20) || (endIndex == -1))
            {
                // Get end index from start index until finding empty row
                endIndex = GetEndIndex(rawData, startIndex);
            }

            attended = ConcatenateRowsAsLines(rawData, startIndex, endIndex);

            // Get not attended start index
            startIndex = GetIndex(rawData, _notAttendedStartIndexMark, notAttendedNextLine);

            if (startIndex > 0)
            {
                // Get attended end index
                endIndex = GetEndIndex(rawData, _notAttendedEndIndexMark);

                if ((endIndex - startIndex > 20) || (endIndex == -1))
                {
                    // Get end index from start index until finding empty row
                    endIndex = GetEndIndex(rawData, startIndex);
                }

                var notAttendedList = ConcatenateRowsAsLines(rawData, startIndex, endIndex);

                notAttended = GetNotAttended(notAttendedList);
            }
            else
            {
                notAttended = new Dictionary<string, string>();
            }
        }

        public void HandleAttendedNotSplitNotAttendedSplit(List<string> rawData,
            out List<string> attended,
            out Dictionary<string, string> notAttended,
            bool attendedNextLine,
            bool notAttendedNextLine)
        {
            // Get attended start index
            var startIndex = GetIndex(rawData, _attendedStartIndexMark, attendedNextLine);

            if (startIndex == -1)
            {
                throw new ApplicationException("Could not find start for attended!");
            }

            // Get attended end index
            var endIndex = GetEndIndex(rawData, _attendedEndIndexMark);

            if ((endIndex - startIndex > 20) || (endIndex == -1))
            {
                // Get end index from start index until finding empty row
                endIndex = GetEndIndex(rawData, startIndex);
            }

            var attendedList = ConcatenateRowsAsText(rawData, startIndex, endIndex);

            attended = GetAttended(attendedList, _attendedSplitOptions);

            // Get not attended start index
            startIndex = GetIndex(rawData, _notAttendedStartIndexMark, notAttendedNextLine);

            if (startIndex > 0)
            {
                // Get attended end index
                endIndex = GetEndIndex(rawData, _notAttendedEndIndexMark);

                if ((endIndex - startIndex > 20) || (endIndex == -1))
                {
                    // Get end index from start index until finding empty row
                    endIndex = GetEndIndex(rawData, startIndex);
                }

                var notAttendedList = ConcatenateRowsAsLines(rawData, startIndex, endIndex);

                notAttended = GetNotAttended(notAttendedList);
            }
            else
            {
                notAttended = new Dictionary<string, string>();
            }
        }

        public void HandleAttendedSplitNotAttendedNotSplit(List<string> rawData,
            out List<string> attended,
            out Dictionary<string, string> notAttended,
            bool attendedNextLine,
            bool notAttendedNextLine)
        {
            // Get attended start index
            var startIndex = GetIndex(rawData, _attendedStartIndexMark, attendedNextLine);

            if (startIndex == -1)
            {
                throw new ApplicationException("Could not find start for attended!");
            }

            // Get attended end index
            var endIndex = GetEndIndex(rawData, _attendedEndIndexMark);

            if ((endIndex - startIndex > 20) || (endIndex == -1))
            {
                // Get end index from start index until finding empty row
                endIndex = GetEndIndex(rawData, startIndex);
            }

            attended = ConcatenateRowsAsLines(rawData, startIndex, endIndex);

            // Get not attended start index
            startIndex = GetIndex(rawData, _notAttendedStartIndexMark, notAttendedNextLine);

            if (startIndex > 0)
            {
                // Get attended end index
                endIndex = GetEndIndex(rawData, _notAttendedEndIndexMark);

                if ((endIndex - startIndex > 20) || (endIndex == -1))
                {
                    // Get end index from start index until finding empty row
                    endIndex = GetEndIndex(rawData, startIndex);
                }

                var notAttendedList = ConcatenateRowsAsText(rawData, startIndex, endIndex);

                notAttended = GetNotAttended(notAttendedList, _notAttendedSplitOptions);
            }
            else
            {
                notAttended = new Dictionary<string, string>();
            }
        }

        public void HandleAttendedNotSplitNotAttendedNotSplit(List<string> rawData,
            out List<string> attended,
            out Dictionary<string, string> notAttended,
            bool attendedNextLine,
            bool notAttendedNextLine)
        {
            // Get attended start index
            var startIndex = GetIndex(rawData, _attendedStartIndexMark, attendedNextLine);

            if (startIndex == -1)
            {
                throw new ApplicationException("Could not find start for attended!");
            }

            // Get attended end index
            var endIndex = GetEndIndex(rawData, _attendedEndIndexMark);

            if ((endIndex - startIndex > 20) || (endIndex == -1))
            {
                // Get end index from start index until finding empty row
                endIndex = GetEndIndex(rawData, startIndex);
            }

            var attendedList = ConcatenateRowsAsText(rawData, startIndex, endIndex);

            attended = GetAttended(attendedList, _attendedSplitOptions);

            // Get not attended start index
            startIndex = GetIndex(rawData, _notAttendedStartIndexMark, notAttendedNextLine);

            if (startIndex > 0)
            {
                // Get attended end index
                endIndex = GetEndIndex(rawData, _notAttendedEndIndexMark);

                if ((endIndex - startIndex > 20) || (endIndex == -1))
                {
                    // Get end index from start index until finding empty row
                    endIndex = GetEndIndex(rawData, startIndex);
                }

                var notAttendedList = ConcatenateRowsAsText(rawData, startIndex, endIndex);

                notAttended = GetNotAttended(notAttendedList, _notAttendedSplitOptions);
            }
            else
            {
                notAttended = new Dictionary<string, string>();
            }
        }

        private static int GetIndex(IList<string> rawData, IEnumerable<string> marks, bool nextLine)
        {
            foreach (var mark in marks)
            {
                var index = rawData.IndexOf(rawData.FirstOrDefault(x => x.StartsWith(mark)));
                if (index != -1)
                {
                    if (nextLine)
                    {
                        index++;
                    }
                    return index;
                }
            }

            return -1;
        }

        private static int GetEndIndex(IList<string> rawData, IEnumerable<string> marks)
        {
            foreach (var mark in marks)
            {
                var index = rawData.IndexOf(rawData.FirstOrDefault(x => x.StartsWith(mark)));
                if (index != -1)
                {
                    return --index;
                }
            }

            return -1;
        }

        private static int GetEndIndex(IList<string> rawData, int startIndex)
        {
            // Hoping that 30 would be ok
            for (var i = startIndex; i < 50; i++)
            {
                if (rawData[i].Length < 3 || string.IsNullOrWhiteSpace(rawData[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        private static int GetEndIndex(IList<string> rawData, IEnumerable<string> marks, int startIndex)
        {
            foreach (var mark in marks)
            {
                for (int i = startIndex; i < rawData.Count; i++)
                {
                    if (rawData[i].StartsWith(mark))
                    {
                        return i - 1;
                    }
                }
            }

            return -1;
        }

        private static string ConcatenateRowsAsText(IReadOnlyList<string> rawData, int startIndex, int endIndex)
        {
            var result = new StringBuilder();

            if (startIndex == endIndex)
            {
                return rawData[startIndex];
            }

            for (var i = startIndex; i <= endIndex; i++)
            {
                result.Append(rawData[i]);
            }

            return result.ToString();
        }

        private static List<string> ConcatenateRowsAsLines(IReadOnlyList<string> rawData, int startIndex, int endIndex)
        {
            var result = new List<string>();

            if (startIndex == endIndex)
            {
                result.Add(rawData[startIndex]);
                return result;
            }

            for (var i = startIndex; i <= endIndex; i++)
            {
                if (rawData[i].Length > 10)
                {
                    result.Add(rawData[i]);
                }
            }

            return result;
        }

        private List<string> GetAttended(string data, string[] splitters)
        {
            var result = new List<string>();

            var splitted = data.Split(splitters, StringSplitOptions.RemoveEmptyEntries);

            foreach (var s in splitted)
            {
                result.Add(Normalize(s));
            }

            return result;
        }

        private Dictionary<string, string> GetNotAttended(string data, string[] splitters)
        {
            var result = new Dictionary<string, string>();

            var splitted = data.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
            foreach (var s in splitted)
            {
                var internalSplit = s.Split(_notAttendedInternalSplitOptions, StringSplitOptions.RemoveEmptyEntries);

                if (internalSplit.Count() == 1)
                {
                    result.Add(NormalizeNotSplit(internalSplit[0]), "-");
                }
                else
                {
                    result.Add(NormalizeNotSplit(internalSplit[0]), internalSplit[1]);
                }
            }

            return result;
        }

        private Dictionary<string, string> GetNotAttended(IReadOnlyList<string> rawData, int startIndex, int endIndex)
        {
            var result = new Dictionary<string, string>();

            for (var i = startIndex; i <= endIndex; i++)
            {
                var internalSplit = rawData[i].Split(_notAttendedInternalSplitOptions, StringSplitOptions.RemoveEmptyEntries);

                var toBeAdded = NormalizeNotSplit(internalSplit[0]);
                if (toBeAdded.Length > 10)
                {
                    if (internalSplit.Count() == 1)
                    {
                        result.Add(NormalizeNotSplit(internalSplit[0]), "-");
                    }
                    else
                    {
                        result.Add(NormalizeNotSplit(internalSplit[0]), internalSplit[1]);
                    }
                }
                
            }

            return result;
        }

        private Dictionary<string, string> GetNotAttended(IReadOnlyList<string> data)
        {
            var result = new Dictionary<string, string>();

            foreach (var record in data)
            {
                var internalSplit = record.Split(_notAttendedInternalSplitOptions, StringSplitOptions.RemoveEmptyEntries);

                var toBeAdded = NormalizeNotSplit(internalSplit[0]);
                if (toBeAdded.Length > 10)
                {
                    if (internalSplit.Count() == 1)
                    {
                        result.Add(NormalizeNotSplit(internalSplit[0]), "-");
                    }
                    else
                    {
                        result.Add(NormalizeNotSplit(internalSplit[0]), internalSplit[1]);
                    }
                }
            }

            return result;
        }

        public string GetDate(List<string> rawData)
        {
            foreach (var item in rawData)
            {
                if (item.StartsWith("2013") || item.StartsWith("2014") || item.StartsWith("2015") || item.StartsWith("2016"))
                {
                    return CleanDate(item);
                }
            }

            return string.Empty;
        }

        private string CleanDate(string rawDate)
        {
            var dateSplitted = rawDate.Split(new[] { ".", "gada", " " }, StringSplitOptions.RemoveEmptyEntries);

            var year = dateSplitted[0];
            var date = dateSplitted[1];
            var month = dateSplitted[2];

            switch (month)
            {
                case "janvari":
                    month = "01";
                    break;
                case "februari":
                    month = "02";
                    break;
                case "marta":
                    month = "03";
                    break;
                case "aprili":
                    month = "04";
                    break;
                case "maija":
                    month = "05";
                    break;
                case "junijs":
                case "junija":
                    month = "06";
                    break;
                case "julija":
                    month = "07";
                    break;
                case "augusta":
                    month = "08";
                    break;
                case "septembri":
                    month = "09";
                    break;
                case "oktobri":
                    month = "10";
                    break;
                case "novembri":
                    month = "11";
                    break;
                case "decembri":
                    month = "12";
                    break;
                default:
                    break;
            }

            return date + "." + month + "." + year;

        }

        public string Normalize(string s)
        {
            var splitted = s.Split(new[] { "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
            var result = string.Empty;

            if (splitted.Count() > 1)
            {
                result = splitted[0].ToLower().Replace("š", "s");
            }
            else
            {
                result = s.ToLower().Replace("š", "s");
            }

            return GetCorrectName(result);
        }

        public string NormalizeNotSplit(string s)
        {
            return GetCorrectName(s.ToLower().Replace("š", "s"));
        }

        public string GetCorrectName(string s)
        {
            var correctName = s;

            foreach (var item in _deputati)
            {
                if (string.IsNullOrEmpty(item.Key) || string.IsNullOrEmpty(item.Value))
                {
                    throw new ApplicationException("Deputatu saraksts satur tuksus ierakstus!");
                }

                if (s.Contains(item.Key))
                {
                    correctName = correctName.Replace(item.Key, item.Value);
                }
            }

            return correctName;
        }
    }
}
