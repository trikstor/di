using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud
{
    public class TagCloudCreator : ITagCloudCreator
    {
        private CircularCloudLayouter Layouter { get; }
        private Size ImgSize { get; }

        public TagCloudCreator(CircularCloudLayouter layouter, Size imgSize)
        {
            Layouter = layouter;
            ImgSize = imgSize;
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
