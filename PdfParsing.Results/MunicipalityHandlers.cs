﻿using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfParsing.Logic;
using PdfParsing.Logic.Handlers;
using System.IO;
using System.Management.Instrumentation;

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

        [TestMethod]
        public void HandleVainode()
        {
            var handler = new VainodeHandler();
            var files = Directory.GetFiles(@"C:\Work_misc\Protokoli\Vainode", "*.pdf", SearchOption.AllDirectories);

            var prieksedetajs = "visvaldis jansons";
            var deputatuSkaits = 9;

            var attended = new List<string>();
            var notAttended = new Dictionary<string, string>();

            var writer = new Writer(new Validator(deputatuSkaits, prieksedetajs), new Cleaner());
            var baseFile = @"C:\Work_misc\Protokoli\Vainode\Results\";

            foreach (var file in files)
            {
                try
                {
                    var rawData = Reader.ReadTextFromPdf(file);

                    const bool attendedSplit = false;
                    const bool notAttendedSplit = false;
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

        [TestMethod]
        public void HandleTukums()
        {
            var handler = new TukumsHandler();
            var files = Directory.GetFiles(@"C:\Work_misc\Protokoli\Tukums", "*.pdf", SearchOption.AllDirectories);

            var prieksedetajs = "juris sulcs";
            var deputatuSkaits = 16;

            var attended = new List<string>();
            var notAttended = new Dictionary<string, string>();

            var writer = new Writer(new Validator(deputatuSkaits, prieksedetajs), new Cleaner());
            var baseFile = @"C:\Work_misc\Protokoli\Tukums\Results\";

            foreach (var file in files)
            {
                try
                {
                    var rawData = Reader.ReadTextFromPdf(file);

                    const bool attendedSplit = false;
                    const bool notAttendedSplit = false;
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

        [TestMethod]
        public void HandleTervete()
        {
            var handler = new TerveteHandler();
            var files = Directory.GetFiles(@"C:\Work_misc\Protokoli\Tervete", "*.pdf", SearchOption.AllDirectories);

            var prieksedetajs = "dace reinika";
            var deputatuSkaits = 8;

            var attended = new List<string>();
            var notAttended = new Dictionary<string, string>();

            var writer = new Writer(new Validator(deputatuSkaits, prieksedetajs), new Cleaner());
            var baseFile = @"C:\Work_misc\Protokoli\Tervete\Results\";

            foreach (var file in files)
            {
                try
                {
                    var rawData = Reader.ReadTextFromPdf(file);

                    const bool attendedSplit = false;
                    const bool notAttendedSplit = false;
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

        [TestMethod]
        public void HandleSkrunda()
        {
            var handler = new SkrundaHandler();
            var files = Directory.GetFiles(@"C:\Work_misc\Protokoli\Skrunda", "*.pdf", SearchOption.AllDirectories);

            var prieksedetajs = "loreta robezniece";
            var deputatuSkaits = 14;

            var attended = new List<string>();
            var notAttended = new Dictionary<string, string>();

            var writer = new Writer(new Validator(deputatuSkaits, prieksedetajs), new Cleaner());
            var baseFile = @"C:\Work_misc\Protokoli\Skrunda\Results\";

            foreach (var file in files)
            {
                try
                {
                    var rawData = Reader.ReadTextFromPdf(file);

                    const bool attendedSplit = true;
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
        public void HandleSkriveri()
        {
            var handler = new SkriveriHandler();
            var files = Directory.GetFiles(@"C:\Work_misc\Protokoli\Skriveri", "*.pdf", SearchOption.AllDirectories);

            var prieksedetajs = "andris zalitis";
            var deputatuSkaits = 8;

            var attended = new List<string>();
            var notAttended = new Dictionary<string, string>();

            var writer = new Writer(new Validator(deputatuSkaits, prieksedetajs), new Cleaner());
            var baseFile = @"C:\Work_misc\Protokoli\Skriveri\Results\";

            foreach (var file in files)
            {
                try
                {
                    var rawData = Reader.ReadTextFromPdf(file);

                    const bool attendedSplit = false;
                    const bool notAttendedSplit = true;
                    const bool attendedNextLine = true;
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
        public void HandleRucava()
        {
            var handler = new RucavaHandler();
            var files = Directory.GetFiles(@"C:\Work_misc\Protokoli\Rucava", "*.pdf", SearchOption.AllDirectories);

            var prieksedetajs = "irena susta";
            var deputatuSkaits = 8;

            var attended = new List<string>();
            var notAttended = new Dictionary<string, string>();

            var writer = new Writer(new Validator(deputatuSkaits, prieksedetajs), new Cleaner());
            var baseFile = @"C:\Work_misc\Protokoli\Rucava\Results\";

            foreach (var file in files)
            {
                try
                {
                    var rawData = Reader.ReadTextFromPdf(file);

                    const bool attendedSplit = false;
                    const bool notAttendedSplit = true;
                    const bool attendedNextLine = true;
                    const bool notAttendedNextLine = true;

                    handler.Handle(rawData, out attended, out notAttended, attendedSplit, notAttendedSplit, attendedNextLine, notAttendedNextLine);

                    var date = handler.GetDate(rawData);

                    writer.WriteToFile(
                        file,
                        baseFile,
                        handler.CleanAttended(attended, prieksedetajs),
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
        public void HandleRauna()
        {
            var handler = new RaunaHandler();
            var files = Directory.GetFiles(@"C:\Work_misc\Protokoli\Rauna", "*.pdf", SearchOption.AllDirectories);

            var prieksedetajs = "evija zurge";
            var deputatuSkaits = 8;

            var attended = new List<string>();
            var notAttended = new Dictionary<string, string>();

            var writer = new Writer(new Validator(deputatuSkaits, prieksedetajs), new Cleaner());
            var baseFile = @"C:\Work_misc\Protokoli\Rauna\Results\";

            foreach (var file in files)
            {
                try
                {
                    var rawData = Reader.ReadTextFromPdf(file);

                    const bool attendedSplit = true;
                    const bool notAttendedSplit = true;
                    const bool attendedNextLine = false;
                    const bool notAttendedNextLine = false;

                    handler.Handle(rawData, out attended, out notAttended, attendedSplit, notAttendedSplit, attendedNextLine, notAttendedNextLine);

                    var date = handler.GetDate(rawData);

                    writer.WriteToFile(
                        file,
                        baseFile,
                        handler.CleanAttended(attended, prieksedetajs),
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
        public void HandlePriekuli()
        {
            var handler = new PriekuliHandler();

            var baseAddress = @"C:\Work_misc\Protokoli\" + handler.Name;

            var files = Directory.GetFiles(baseAddress, "*.pdf", SearchOption.AllDirectories);

            var writer = new Writer(new Validator(handler.DeputatuSkaits, handler.Prieksedetajs), new Cleaner());

            var baseFile = baseAddress + @"\Results\";

            foreach (var file in files)
            {
                try
                {
                    var rawData = Reader.ReadTextFromPdf(file);

                    List<string> attended;
                    Dictionary<string, string> notAttended;
                    handler.Handle(rawData, 
                        out attended, 
                        out notAttended, 
                        handler.AttendedSplit, 
                        handler.NotAttendedSplit,
                        handler.AttendedNextLine,
                        handler.NotAttendedNextLine);

                    var date = handler.GetDate(rawData);

                    writer.WriteToFile(
                        file,
                        baseFile,
                        handler.CleanAttended(attended, handler.Prieksedetajs),
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
        public void HandlePlavinas()
        {
            var handler = new PlavinasHandler();

            var baseAddress = @"C:\Work_misc\Protokoli\" + handler.Name;

            var files = Directory.GetFiles(baseAddress, "*.pdf", SearchOption.AllDirectories);

            var writer = new Writer(new Validator(handler.DeputatuSkaits, handler.Prieksedetajs), new Cleaner());

            var baseFile = baseAddress + @"\Results\";

            foreach (var file in files)
            {
                try
                {
                    var rawData = Reader.ReadTextFromPdf(file);

                    List<string> attended;
                    Dictionary<string, string> notAttended;
                    handler.Handle(rawData,
                        out attended,
                        out notAttended,
                        handler.AttendedSplit,
                        handler.NotAttendedSplit,
                        handler.AttendedNextLine,
                        handler.NotAttendedNextLine);

                    var date = handler.GetDate(rawData);

                    writer.WriteToFile(
                        file,
                        baseFile,
                        handler.CleanAttended(attended, handler.Prieksedetajs),
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
