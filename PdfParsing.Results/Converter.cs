using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfParsing.Logic;

namespace PdfParsing.Results
{
    /// <summary>
    /// Summary description for Converter
    /// </summary>
    [TestClass]
    public class Converter
    {
        [TestMethod]
        public void ConvertFiles()
        {
            PdfConverter.Convert(@"C:\Work_misc\Protokoli\Aizpute");
        }
    }
}
