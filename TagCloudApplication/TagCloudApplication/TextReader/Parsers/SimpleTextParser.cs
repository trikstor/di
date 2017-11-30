﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TextReader.Parsers
{
    public class SimpleTextParser : IParser
    {
        public string[] FileExtentions { get; } 
            = new [] { "txt" };

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