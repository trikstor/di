using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagCloudApplication.Layouter;

namespace TagCloudApplication
{
    public class TagsCreator : ITagsCreator
    {
        public ILayouter Layouter { get; }
        public TagsCreator(ILayouter layouter)
        {
            Layouter = layouter;
        }
        public IEnumerable<Tag> Create(Dictionary<string, int> tagsCollection, int minFontSize, int maxFontSize, string fontName)
        {
            var factor = GetTagCollectionFontFactor(GetMaxTagWeight(tagsCollection), maxFontSize);
            return tagsCollection
                .Select(tag =>
            {
                var font = new Font(fontName, GetTagFontSize(tag.Value, factor, minFontSize));
                return new Tag(tag.Key, font, Layouter.PutNextRectangle(GetRectangleSize(tag.Key, font)));
            });
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

            return (int)(fontSize >= minFontSize ? fontSize : minFontSize);
        }

        private Size GetRectangleSize(string word, Font font)
        {
            var size = TextRenderer.MeasureText(word, font);
            return new Size(size.Width + 1, size.Height);
        }
    }
}
