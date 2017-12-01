using System.Collections.Generic;

namespace TagCloudApplication.Filrters
{
    public class BoringWordsFilter : IFilter
    {
        private List<string> BoringWords { get; }
        
        public BoringWordsFilter(List<string> boringWords)
        {
            BoringWords = boringWords;
        }

        public bool FilterTag(string tag)
        {
            return !BoringWords.Contains(tag) && tag.Length > 3;
        }
    }
}