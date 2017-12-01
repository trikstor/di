using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloudApplication.Layouter
{
    public class TagCloudCreator : ITagCloudCreator
    {
        private ILayouter Layouter { get; }
        private Size ImgSize { get; }

        public TagCloudCreator(ILayouter layouter, Config config)
        {
            Layouter = layouter;
            ImgSize = config.ImgSize;
        }
        public Dictionary<string, Rectangle> Create(Dictionary<string, int> tagsCollection)
        {
            return tagsCollection
                .OrderByDescending(pair => pair.Value)
                .ToDictionary(tag => tag.Key,
                    tag => Layouter.PutNextRectangle(GetRectangleSizeForWord(tag.Key.Length, tag.Value, ImgSize,
                        tagsCollection.Count)));
        }

        private Size GetRectangleSizeForWord(int wordLength, int tagWeight, Size imgSize, int rectQuant)
        {
            rectQuant *= 2;
            wordLength /= 2;
            wordLength = (wordLength == 0) ? 1 : wordLength;
            return new Size(wordLength * tagWeight * ((imgSize.Width + imgSize.Height) / rectQuant),
                tagWeight * ((imgSize.Width + imgSize.Height) / rectQuant));
        }
    }
}
