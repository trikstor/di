using System.Collections.Generic;
using System.IO;

namespace TextReader.Parsers
{
    public interface IParser
    {
        string[] FileExtentions { get; }
        IEnumerable<string> Parse(StreamReader textrReader);
    }
}