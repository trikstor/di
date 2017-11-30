using System.Collections.Generic;

namespace TextReader.Filrters
{
    public interface IFilter
    {
        bool FilterTag(string tag);
    }
}
