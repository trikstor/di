using System.Collections.Generic;
using System.Linq;

namespace TextReader.Filrters
{
    public class BoringWordsFilter : IFilter
    {
        private List<string> BoringWords { get; }
        
        public BoringWordsFilter(List<string> boringWords)
        {
            BoringWords = boringWords;
        }

        public Dictionary<string, int> FilterTags(Dictionary<string, int> tags)
        {
            return tags.Where(tag => !BoringWords.Contains(tag.Key))
                .ToDictionary(tag => tag.Key, tag => tag.Value);
        }
    }
}