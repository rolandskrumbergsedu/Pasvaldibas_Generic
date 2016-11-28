using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfParsing.Logic
{
    public static class Writer
    {
        public static void WriteToFile(List<string> attending, Dictionary<string, string> notAttending, string date)
        {
            const string filePath = @"C:\Work_misc\Protokoli\Result.csv";

            //before your loop
            var csv = new StringBuilder();

            foreach (var deputats in attending)
            {
                var vards = deputats.Replace("š", "s").Replace("Š", "S");
                var dat = date.Replace("Salacgriva         ", string.Empty);
                csv.Append($"{vards.Trim()},{dat.Trim()},1,-");
                csv.Append(Environment.NewLine);
            }

            foreach (var deputats in notAttending)
            {
                var vards = deputats.Key.Replace("š", "s").Replace("Š", "S");
                var dat = date.Replace("Salacgriva         ", string.Empty);
                var iemesls = deputats.Value;
                csv.Append($"{vards.Trim()},{dat.Trim()},0,{iemesls.Trim()}");
                csv.Append(Environment.NewLine);
            }

            //after your loop
            File.AppendAllText(filePath, csv.ToString());
        }
    }
}
