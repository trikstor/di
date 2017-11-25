using System.Collections.Generic;

namespace TextReader.Filrters
{
    public interface IFilter
    {
        Dictionary<string, int> FilterTags(Dictionary<string, int> tags);
    }
}
