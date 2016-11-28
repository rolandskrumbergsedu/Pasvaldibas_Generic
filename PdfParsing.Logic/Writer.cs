using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PdfParsing.Logic
{
    public static class Writer
    {
        public static void WriteToFile(string filePath, List<string> attending, Dictionary<string, string> notAttending, string date)
        {
            //before your loop
            var csv = new StringBuilder();

            foreach (var deputats in attending)
            {
                csv.Append($"{deputats.Trim()},{date.Trim()},1,-");
                csv.Append(Environment.NewLine);
            }

            foreach (var deputats in notAttending)
            {
                csv.Append($"{deputats.Key.Trim()},{date.Trim()},0,{deputats.Value.Trim()}");
                csv.Append(Environment.NewLine);
            }

            //after your loop
            File.AppendAllText(filePath, csv.ToString());
        }
    }
}
