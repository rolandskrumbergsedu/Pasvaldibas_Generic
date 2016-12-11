using System;
using System.Collections.Generic;

namespace PdfParsing.Logic
{
    public class Cleaner
    {
        public string CleanAttending(string deputats)
        {
            return deputats;
        }

        public KeyValuePair<string, string> CleanNotAttending(KeyValuePair<string, string> deputats)
        {
            return deputats;
        }
    }
}