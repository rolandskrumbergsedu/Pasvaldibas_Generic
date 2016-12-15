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
        public void HandleAdazi()
        {
            var handler = new AdaziHandler();
            var files = Directory.GetFiles(@"C:\Work_misc\Protokoli\Adazi", "*.pdf", SearchOption.AllDirectories);

            var prieksedetajs = "maris sprindžuks";
            var deputatuSkaits = 14;

            var attended = new List<string>();
            var notAttended = new Dictionary<string, string>();

            var writer = new Writer(new Validator(deputatuSkaits, prieksedetajs), new Cleaner());
            var baseFile = @"C:\Work_misc\Protokoli\Adazi\Results\";

            foreach (var file in files)
            {
                try
                {
                    var rawData = Reader.ReadTextFromPdf(file);

                    const bool attendedSplit = false;
                    const bool notAttendedSplit = true;
                    const bool attendedNextLine = false;
                    const bool notAttendedNextLine = true;

                    handler.Handle(rawData, out attended, out notAttended, attendedSplit, notAttendedSplit, attendedNextLine, notAttendedNextLine);

                    var date = handler.GetDate(rawData);
                    
                    writer.WriteToFile(
                        file,
                        baseFile,
                        attended,
                        handler.CleanNotAttended(notAttended),
                        date);
                }
                catch(Exception ex)
                {
                    writer.WriteError(file, baseFile, ex);
                }
                
            }
            
        }

        [TestMethod]
        public void HandleAglona()
        {
            var handler = new AglonaHandler();
            var files = Directory.GetFiles(@"C:\Work_misc\Protokoli\Aglona", "*.pdf", SearchOption.AllDirectories);

            var prieksedetajs = "helena streike";
            var deputatuSkaits = 8;

            var attended = new List<string>();
            var notAttended = new Dictionary<string, string>();

            var writer = new Writer(new Validator(deputatuSkaits, prieksedetajs), new Cleaner());
            var baseFile = @"C:\Work_misc\Protokoli\Aglona\Results\";

            foreach (var file in files)
            {
                try
                {
                    var rawData = Reader.ReadTextFromPdf(file);

                    const bool attendedSplit = false;
                    const bool notAttendedSplit = true;
                    const bool attendedNextLine = false;
                    const bool notAttendedNextLine = true;

                    handler.Handle(rawData, out attended, out notAttended, attendedSplit, notAttendedSplit, attendedNextLine, notAttendedNextLine);

                    var date = handler.GetDate(rawData);

                    writer.WriteToFile(
                        file,
                        baseFile,
                        handler.CleanAttended(attended),
                        handler.CleanNotAttended(notAttended),
                        date);
                }
                catch (Exception ex)
                {
                    writer.WriteError(file, baseFile, ex);
                }

            }

        }

        [TestMethod]
        public void HandleVilaka()
        {
            var handler = new VilakaHandler();
            var files = Directory.GetFiles(@"C:\Work_misc\Protokoli\Vilaka", "*.pdf", SearchOption.AllDirectories);

            var prieksedetajs = "vija gaiduka";
            var deputatuSkaits = 14;

            var attended = new List<string>();
            var notAttended = new Dictionary<string, string>();

            var writer = new Writer(new Validator(deputatuSkaits, prieksedetajs), new Cleaner());
            var baseFile = @"C:\Work_misc\Protokoli\Vilaka\Results\";

            foreach (var file in files)
            {
                try
                {
                    var rawData = Reader.ReadTextFromPdf(file);

                    const bool attendedSplit = false;
                    const bool notAttendedSplit = true;
                    const bool attendedNextLine = false;
                    const bool notAttendedNextLine = true;

                    handler.Handle(rawData, out attended, out notAttended, attendedSplit, notAttendedSplit, attendedNextLine, notAttendedNextLine);


                    var date = handler.GetDate(rawData);

                    writer.WriteToFile(
                        file,
                        baseFile,
                        handler.CleanAttended(attended),
                        handler.CleanNotAttended(notAttended),
                        date);
                }
                catch (Exception ex)
                {
                    writer.WriteError(file, baseFile, ex);
                }

            }
        }

        [TestMethod]
        public void HandleViesite()
        {
            var handler = new ViesiteHandler();
            var files = Directory.GetFiles(@"C:\Work_misc\Protokoli\Viesite", "*.pdf", SearchOption.AllDirectories);

            var prieksedetajs = "janis dimitrijevs";
            var deputatuSkaits = 8;

            var attended = new List<string>();
            var notAttended = new Dictionary<string, string>();

            var writer = new Writer(new Validator(deputatuSkaits, prieksedetajs), new Cleaner());
            var baseFile = @"C:\Work_misc\Protokoli\Viesite\Results\";

            foreach (var file in files)
            {
                try
                {
                    var rawData = Reader.ReadTextFromPdf(file);

                    const bool attendedSplit = false;
                    const bool notAttendedSplit = true;
                    const bool attendedNextLine = false;
                    const bool notAttendedNextLine = false;

                    handler.Handle(rawData, out attended, out notAttended, attendedSplit, notAttendedSplit, attendedNextLine, notAttendedNextLine);

                    var date = handler.GetDate(rawData);

                    writer.WriteToFile(
                        file,
                        baseFile,
                        handler.CleanAttended(attended),
                        handler.CleanNotAttended(notAttended),
                        date);
                }
                catch (Exception ex)
                {
                    writer.WriteError(file, baseFile, ex);
                }

            }

        }
    }
}
