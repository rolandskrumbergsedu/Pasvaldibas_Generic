using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace PdfParsing
{
    class Program
    {
        public const string Src = @"C:\Work_misc\Protokoli\2016_10_19_domes_sedes_protokols_Salacgriva.pdf";

        static void Main(string[] args)
        {
            var files = Directory.GetFiles(@"C:\Work_misc\Protokoli\Salacgriva_old", "*.pdf", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var resultList = ReadTextFromPdf(file);

                HandleSalacgriva_Old(resultList);
            }
        }

        public static List<string> ReadTextFromPdf(string file)
        {
            var strText = string.Empty;
            var reader = new PdfReader(file);
            var resultList = new List<string>();

            for (var page = 1; page <= reader.NumberOfPages; page++)
            {
                ITextExtractionStrategy its = new LocationTextExtractionStrategy();
                var s = PdfTextExtractor.GetTextFromPage(reader, page, its);

                s = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(s)));
                strText = strText + s;
                resultList.AddRange(strText.Split('\n').ToList());
            }

            reader.Close();

            return resultList;
        }

        public static void HandleSalacgriva(List<string> rawData)
        {
            var indexOfDeputati = rawData.IndexOf(rawData.FirstOrDefault(x => x.StartsWith("Deputati:")));
            if (indexOfDeputati == -1)
            {
                indexOfDeputati = rawData.IndexOf(rawData.FirstOrDefault(x => x.StartsWith(" Deputati:")));
            }

            var indexOfPasvaldibas =
                rawData.IndexOf(rawData.FirstOrDefault(x => x.StartsWith("Pašvaldibas administracijas darbinieki:")));
            if (indexOfPasvaldibas == -1)
            {
                var j = indexOfDeputati;
                do
                {
                    j++;
                } while (rawData[j] != " ");
                indexOfPasvaldibas = j;
            }

            var visiDeputati = string.Empty;

            for (var i = indexOfDeputati; i < indexOfPasvaldibas; i++)
            {
                visiDeputati += rawData[i];
            }

            var visiDeputatiClean = visiDeputati.Replace("Deputati: ", string.Empty);

            var deputatiPiedalas = visiDeputatiClean.Split(new[] {", "}, StringSplitOptions.RemoveEmptyEntries);

            var deputatiNepiedalasResult = new Dictionary<string, string>();
            var indexOfNepiedalas = rawData.IndexOf(rawData.FirstOrDefault(x => x.StartsWith("Nepiedalas")));
            if (indexOfNepiedalas != -1)
            {
                var j = indexOfNepiedalas - 1;
                do
                {
                    j++;
                } while (rawData[j] != " ");

                var nepiedalasDeputati = string.Empty;
                for (var i = indexOfNepiedalas; i < j; i++)
                {
                    nepiedalasDeputati += rawData[i];
                }
                var nepiedalasDeputatiClean = nepiedalasDeputati
                    .Replace("Nepiedalas deputati - ", string.Empty)
                    .Replace("Nepiedalas deputati- ", string.Empty)
                    .Replace("Nepiedalas- ", string.Empty);

                var deputatiNepiedalas = nepiedalasDeputatiClean.Split(new[] { ", ", "; " }, StringSplitOptions.RemoveEmptyEntries);

                deputatiNepiedalasResult = deputatiNepiedalas.Select(deputats => deputats.Split(new[] { "(", ")" }, StringSplitOptions.RemoveEmptyEntries)).ToDictionary(splitted => splitted[0], splitted => splitted[1]);
            }

            var datums = GetDate_Salacgriva(rawData);

            WriteToFile(deputatiPiedalas.ToList(), deputatiNepiedalasResult, datums);
        }

        public static void HandleSalacgriva_Old(List<string> rawData)
        {
            var indexOfDeputati = rawData.IndexOf(rawData.FirstOrDefault(x => x.StartsWith("Domes sastavs")));

            var sastavs = new List<string>();

            var j = indexOfDeputati;
            do
            {
                j++;
                sastavs.Add(rawData[j]);
            } while (rawData[j] != " ");

            var result = new Dictionary<string, Tuple<string, string>>();

            foreach (var deputats in sastavs)
            {
                if (deputats == " " || deputats.StartsWith("("))
                {
                    continue;
                }

                var row = deputats.Split(new[] {".", "  "}, StringSplitOptions.RemoveEmptyEntries);

                if (deputats.Contains("piedalas"))
                {
                    result.Add(row[1], new Tuple<string, string>("1", "-"));
                }
                else
                {
                    if (row[2].Contains("nepiedalas"))
                    {
                        var iemesli = row[2].Split(new[] { "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
                        result.Add(row[2], new Tuple<string, string>("0", iemesli[1]));
                    }
                    else
                    {
                        throw new Exception(":(");
                    }
                }

            }

            //var datums = GetDate_Salacgriva(rawData);

            WriteToFile(result, "");
        }

        public static string GetDate_Salacgriva(List<string> rawData)
        {
            var datums = string.Empty;
            for (var i = 9; i < 20; i++)
            {
                if (!rawData[i].Contains("Salacgriva")) continue;

                datums = rawData[i];
                break;
            }

            if (string.IsNullOrEmpty(datums))
            {
                throw new Exception("Oh shit!");
            }

            return datums;
        }

        public static void WriteToFile(Dictionary<string, Tuple<string, string>> results, string datums)
        {
            const string filePath = @"C:\Work_misc\Protokoli\Result.csv";

            //before your loop
            var csv = new StringBuilder();

            foreach (var row in results)
            {
                csv.Append($"{row.Key.Trim()},datums,{row.Value.Item1.Trim()},{row.Value.Item2.Trim()}");
            }

            //after your loop
            File.AppendAllText(filePath, csv.ToString());
        }

        public static void WriteToFile(List<string> piedalas, Dictionary<string, string> nepiedalas, string datums)
        {
            const string filePath = @"C:\Work_misc\Protokoli\Result.csv";

            //before your loop
            var csv = new StringBuilder();

            foreach (var deputats in piedalas)
            {
                var vards = deputats.Replace("š", "s").Replace("Š", "S");
                var dat = datums.Replace("Salacgriva         ", string.Empty);
                csv.Append($"{vards.Trim()},{dat.Trim()},1,-");
                csv.Append(Environment.NewLine);
            }

            foreach (var deputats in nepiedalas)
            {
                var vards = deputats.Key.Replace("š", "s").Replace("Š", "S");
                var dat = datums.Replace("Salacgriva         ", string.Empty);
                var iemesls = deputats.Value;
                csv.Append($"{vards.Trim()},{dat.Trim()},0,{iemesls.Trim()}");
                csv.Append(Environment.NewLine);
            }

            //after your loop
            File.AppendAllText(filePath, csv.ToString());
        }
    }
}
