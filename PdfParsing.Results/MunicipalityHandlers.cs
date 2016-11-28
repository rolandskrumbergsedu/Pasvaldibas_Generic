using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfParsing.Logic;
using PdfParsing.Logic.Handlers;
using System.IO;

namespace PdfParsing.Results
{
    [TestClass]
    public class MunicipalityHandlers
    {
        [TestMethod]
        public void HandleSalacgriva()
        {
            var handler = new SalacgrivaHandler();
        }

        [TestMethod]
        public void HandleJelgavasPilseta()
        {
            var handler = new JelgavaPilsetaHandler();
            var files = Directory.GetFiles(@"C:\Work_misc\Protokoli\Jelgava", "*.pdf", SearchOption.AllDirectories);

            var attended = new List<string>();
            var notAttended = new Dictionary<string, string>();

            foreach (var file in files)
            {
                var rawData = Reader.ReadTextFromPdf(file);

                handler.Handle(rawData, out attended, out notAttended);
            }

            var date = handler.GetDate();
            Writer.WriteToFile(@"C:\Work_misc\Protokoli\Jelgava_results.csv", attended, notAttended, date);
        }
    }
}
