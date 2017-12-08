﻿using System.Collections.Generic;
using System.IO;

namespace TagCloudApplication.Parsers
{
    public interface IParser
    {
        string[] FileExtentions { get; }
        IEnumerable<string> Parse(StreamReader fileReader);
    }
}