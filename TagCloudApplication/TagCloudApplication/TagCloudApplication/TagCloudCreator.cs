using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloudApplication
{
    public class TagCloudCreator : ITagCloudCreator
    {
        private string FontName { get; }
        private int MinFontSize { get; }
        private int MaxFontSize { get; }

        public TagCloudCreator(Config config)
        {
            MinFontSize = config.MinFontSize;
            MaxFontSize = config.MaxFontSize;
            FontName = config.FontName;
        }
        
        public Dictionary<string, Font> Create(Dictionary<string, int> tagsCollection)
        {
            var factor = GetTagCollectionFontFactor(GetMaxTagWeight(tagsCollection), MaxFontSize);
            var res = tagsCollection
                .OrderByDescending(pair => pair.Value)
                .ToDictionary(tag => tag.Key,
                    tag => new Font(FontName, GetTagFontSize(tag.Value, factor, MinFontSize)));
            return res;
        }

        private int GetMaxTagWeight(Dictionary<string, int> tagsCollection)
        {
            return tagsCollection.Max(tag => tag.Value);
        }
        
        private double GetTagCollectionFontFactor(int maxTagWeight, int maxFontSize) => 
            maxFontSize / maxTagWeight;

        private int GetTagFontSize(int tagWeight, double factor, int minFontSize)
        {
            var fontSize = tagWeight * factor;

            return (int) (fontSize >= minFontSize ? fontSize : minFontSize);
        }
    }
}
