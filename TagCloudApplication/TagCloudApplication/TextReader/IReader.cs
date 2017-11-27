using System.Collections.Generic;
using TextReader.Filrters;

namespace TextReader
{
    public interface IReader
    {
        Dictionary<string, int> Read(string path, List<IFilter> filters);
    }
}
