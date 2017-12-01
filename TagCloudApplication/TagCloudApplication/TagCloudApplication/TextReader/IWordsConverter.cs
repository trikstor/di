using System.Collections.Generic;

namespace TagCloudApplication.TextReader
{
    public interface IWordsConverter
    {
        IEnumerable<string> NormalizeWords(IEnumerable<string> words);
    }
}
