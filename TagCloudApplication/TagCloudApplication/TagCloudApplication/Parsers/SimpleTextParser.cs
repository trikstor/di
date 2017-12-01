﻿using System;
using System.Collections.Generic;
using System.IO;

namespace TagCloudApplication.Parsers
{
    public class SimpleTextParser : IParser
    {
        public string[] FileExtentions { get; } 
            = { ".txt" };

        public IEnumerable<string> Parse(StreamReader textReader)
        {
            string currStr;
            while ((currStr = textReader.ReadLine()) != null)
            {
                foreach (var word in currStr.Split(
                    new[] {' ', '.', ',', ':', ';', '!', '?', '\t', '–'},
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    yield return word;
                }
            }
        }
    }
}