using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace PdfParsing.Logic
{
    public static class Reader
    {
        /// <summary>
        /// Reads all the files from specific location
        /// </summary>
        /// <param name="location">Folder path where PDFs are located</param>
        /// <returns>Array of string file names</returns>
        public static string[] ReadFilesFromLocation(string location)
        {
            return Directory.GetFiles(location, "*.pdf", SearchOption.AllDirectories);
        }

        /// <summary>
        /// Reads PDF document and converts its content to string text
        /// </summary>
        /// <param name="file">Full path to file and file name</param>
        /// <returns>List of text lines, each line represents line in PDF document</returns>
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
    }
}
