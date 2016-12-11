using System;
using System.Collections.Generic;

namespace PdfParsing.Logic
{
    public class Validator
    {
        private int _deputatuSkaits;
        private string _prieksedetajs;

        public Validator(int deputatuSkaits, string prieksedetajs)
        {
            _deputatuSkaits = deputatuSkaits;
            _prieksedetajs = prieksedetajs;
        }

        public bool IsValid(string p)
        {
            return true;
        }

        public bool IsValidNotAttending(KeyValuePair<string, string> keyValuePair)
        {
            return true;
        }

        public bool IsValid(List<string> attending, Dictionary<string, string> notAttending, out string error)
        {
            foreach (var item in attending)
            {
                if (item.ToLower().Contains(_prieksedetajs))
                {
                    error = "Attending prieksedetajs";
                    return false;
                }
            }

            foreach (var item in notAttending)
            {
                if (item.Key.ToLower().Contains(_prieksedetajs))
                {
                    error = "Not attending prieksedetajs";
                    return false;
                }
            }

            if ((attending.Count + notAttending.Count) != _deputatuSkaits)
            {
                error = "Wrong count";
                return false;
            }
            error = string.Empty;
            return true;
        }
    }
}