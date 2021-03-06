﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PdfParsing.Logic
{
    public class Writer
    {
        private readonly Validator _validator;
        private readonly Cleaner _cleaner;

        public Writer(Validator validator, Cleaner cleaner)
        {
            _validator = validator;
            _cleaner = cleaner;
        }

        public void WriteToFile(string file, string filePath, List<string> attending, Dictionary<string, string> notAttending, string date)
        {
            //before your loop
            var goodCsv = new StringBuilder();
            var badCsv = new StringBuilder();
            var error = string.Empty;

            if (!_validator.IsValid(attending, notAttending, out error))
            {
                badCsv.Append(file);
                badCsv.Append(Environment.NewLine);
                badCsv.Append(error);
                badCsv.Append(Environment.NewLine);
                foreach (var deputats in attending)
                {
                    if (!string.IsNullOrEmpty(deputats))
                    {
                        badCsv.Append($"{deputats.Trim()},{date.Trim()},1,-");
                        badCsv.Append(Environment.NewLine);
                    }
                    
                }

                foreach (var deputats in notAttending)
                {
                    if (!string.IsNullOrEmpty(deputats.Key))
                    {
                        badCsv.Append($"{deputats.Key.Trim()},{date.Trim()},0,{deputats.Value.Trim()}");
                        badCsv.Append(Environment.NewLine);
                    }
                }
            }
            else
            {
                goodCsv.Append(file);
                goodCsv.Append(Environment.NewLine);
                foreach (var deputats in attending)
                {
                    if (_validator.IsValid(_cleaner.CleanAttending(deputats)))
                    {
                        if (!string.IsNullOrEmpty(deputats))
                        {
                            goodCsv.Append($"{deputats.Trim()},{date.Trim()},1,-");
                            goodCsv.Append(Environment.NewLine);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(deputats))
                        {
                            badCsv.Append($"{deputats.Trim()},{date.Trim()},1,-");
                            badCsv.Append(Environment.NewLine);
                        }
                    }

                }

                foreach (var deputats in notAttending)
                {
                    if (_validator.IsValidNotAttending(_cleaner.CleanNotAttending(deputats)))
                    {
                        if (!string.IsNullOrEmpty(deputats.Key))
                        {
                            goodCsv.Append($"{deputats.Key.Trim()},{date.Trim()},0,{deputats.Value.Trim()}");
                            goodCsv.Append(Environment.NewLine);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(deputats.Key))
                        {
                            badCsv.Append($"{deputats.Key.Trim()},{date.Trim()},0,{deputats.Value.Trim()}");
                            badCsv.Append(Environment.NewLine);
                        }
                    }

                }
            }

            CheckIfExists(filePath);

            //after your loop
            File.AppendAllText(filePath + "good.csv", goodCsv.ToString());
            File.AppendAllText(filePath + "bad.csv", badCsv.ToString());
        }

        public void WriteError(string file, string filePath, Exception ex)
        {
            var fileToWrite = filePath + "errors.txt";

            CheckIfExists(filePath);

            File.AppendAllText(fileToWrite, file);
            File.AppendAllText(fileToWrite, Environment.NewLine);
            File.AppendAllText(fileToWrite, ex.Message);
            File.AppendAllText(fileToWrite, Environment.NewLine);
            File.AppendAllText(fileToWrite, ex.StackTrace);
            File.AppendAllText(fileToWrite, Environment.NewLine);
            File.AppendAllText(fileToWrite, Environment.NewLine);
        }

        private void CheckIfExists(string filePath)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
        }
    }
}
