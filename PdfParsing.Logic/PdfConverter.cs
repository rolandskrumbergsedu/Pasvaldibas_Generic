using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;

namespace PdfParsing.Logic
{
    public static class PdfConverter
    {
        public static void Convert(string basePath)
        {
            var files = Directory.GetFiles(basePath, "*.doc", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                ConvertDocFileToPdf(file);
            }

            files = Directory.GetFiles(basePath, "*.docx", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                ConvertDocxFileToPdf(file);
            }
        }

        public static void ConvertDocFileToPdf(string file)
        {
            var fileName = file.Replace(".doc", string.Empty);

            DoConversion(file, fileName);
        }

        public static void ConvertDocxFileToPdf(string file)
        {
            var fileName = file.Replace(".docx", string.Empty);

            DoConversion(file, fileName);
        }

        public static void DoConversion(string file, string outputFileName)
        {
            var appWord = new Application();
            if (appWord.Documents == null) return;
            
            var wordDocument = appWord.Documents.Open(file);
            var pdfDocName = outputFileName + ".pdf";
            if (wordDocument != null)
            {
                wordDocument.ExportAsFixedFormat(pdfDocName,
                    WdExportFormat.wdExportFormatPDF);
                wordDocument.Close();
            }
            appWord.Quit();
        }
    }
}
